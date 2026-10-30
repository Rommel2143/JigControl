using ClosedXML.Excel;
using Dapper;
using JIGControl.Web.Common;
using JIGControl.Web.Features.Inventory;
using JIGControl.Web.Features.NewJigs;

namespace JIGControl.Web.Features.Reports;

public sealed class SectionSummary
{
    public string Section { get; set; } = "";
    public long TotalJig { get; set; }
    public long TotalMissing { get; set; }
    public long Scanned => TotalJig - TotalMissing;
    public double Percentage => TotalJig == 0 ? 0 : Math.Round(Scanned * 100.0 / TotalJig, 1);
}

public sealed class MonthCard
{
    public int ScanYear { get; set; }
    public int ScanMonth { get; set; }
    public long TotalScanned { get; set; }
    public long TotalMaster { get; set; }
    public double Percentage => TotalMaster == 0 ? 0 : Math.Round(TotalScanned * 100.0 / TotalMaster, 1);
}

/// Scan coverage per section for one month (the WinForms printGraph report).
public sealed class SectionSummaryHandler(IDbConnectionFactory db)
{
    public async Task<List<SectionSummary>> Handle(int year, int month, CancellationToken ct = default)
    {
        using var con = await db.OpenAsync(ct);
        var rows = await con.QueryAsync<SectionSummary>("""
            SELECT jm.section,
                   COUNT(DISTINCT jm.jig_id) AS total_jig,
                   SUM(CASE WHEN ji.jig_id IS NULL THEN 1 ELSE 0 END) AS total_missing
            FROM jig_masterlist jm
            LEFT JOIN (SELECT DISTINCT jig_id FROM jig_inventory WHERE scan_month = @month AND scan_year = @year) ji
                   ON ji.jig_id = jm.jig_id
            WHERE DATE(jm.timestamp) < @endDate
            GROUP BY jm.section
            ORDER BY jm.section
            """, new { year, month, endDate = new DateTime(year, month, 1).AddMonths(1) });
        return rows.AsList();
    }
}

/// One card per scanned month: distinct JIGs scanned vs. JIGs that existed by month end (the WinForms Dashboard).
public sealed class MonthCardsHandler(IDbConnectionFactory db)
{
    public async Task<List<MonthCard>> Handle(int year, CancellationToken ct = default)
    {
        using var con = await db.OpenAsync(ct);
        var rows = await con.QueryAsync<MonthCard>("""
            SELECT s.scan_year, s.scan_month, s.total_scanned,
                   (SELECT COUNT(*) FROM jig_masterlist jm
                     WHERE jm.timestamp < DATE_ADD(MAKEDATE(s.scan_year, 1), INTERVAL s.scan_month MONTH)) AS total_master
            FROM (SELECT scan_year, scan_month, COUNT(DISTINCT jig_id) AS total_scanned
                  FROM jig_inventory WHERE scan_year = @year
                  GROUP BY scan_year, scan_month) s
            ORDER BY s.scan_month
            """, new { year });
        return rows.AsList();
    }
}

public static class ReportEndpoints
{
    public static void MapReportEndpoints(this WebApplication app)
    {
        app.MapGet("/reports/export", async (string kind, int year, int month, HttpContext ctx,
            ListScannedHandler scanned, ListMissingHandler missing, SectionSummaryHandler summary, ListNewJigsHandler newJigs) =>
        {
            if (month is < 1 or > 12) return Results.BadRequest();
            var section = ctx.User.Section();
            using var wb = new XLWorkbook();
            var stamp = $"{year}-{month:00}";

            switch (kind)
            {
                case "scanned":
                    wb.Worksheets.Add("Scanned").Cell(1, 1).InsertTable(await scanned.Handle(section, year, month));
                    break;
                case "missing":
                    wb.Worksheets.Add("Missing").Cell(1, 1).InsertTable(await missing.Handle(section, year, month));
                    break;
                case "summary":
                    var rows = (await summary.Handle(year, month))
                        .Select(r => new { r.Section, Total = r.TotalJig, r.Scanned, Missing = r.TotalMissing, Percent = r.Percentage });
                    wb.Worksheets.Add("Summary").Cell(1, 1).InsertTable(rows);
                    section = "all";
                    break;
                case "new":
                    // Admin dashboard export: ?section= (empty = all sections).
                    if (!ctx.User.IsAdmin()) return Results.Forbid();
                    var filter = ctx.Request.Query["section"].ToString();
                    section = filter == "" ? "all" : filter;
                    var added = (await newJigs.Handle(filter == "" ? null : filter, year, month))
                        .Select(r => new { r.DateAdded, r.ControlNo, r.Partcode, r.Partname, r.Section, r.Status });
                    wb.Worksheets.Add("New JIGs").Cell(1, 1).InsertTable(added);
                    break;
                default:
                    return Results.BadRequest();
            }
            foreach (var ws in wb.Worksheets) { ws.Columns().AdjustToContents(); ws.SheetView.FreezeRows(1); }

            var ms = new MemoryStream();
            wb.SaveAs(ms);
            ms.Position = 0;
            return Results.File(ms, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"JIG_{kind}_{section}_{stamp}.xlsx");
        }).RequireAuthorization();
    }
}
