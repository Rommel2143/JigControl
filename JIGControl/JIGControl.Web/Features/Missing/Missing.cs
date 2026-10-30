using Dapper;
using JIGControl.Web.Common;

namespace JIGControl.Web.Features.Missing;

public sealed class MissingJigRow
{
    public int JigId { get; set; }
    public string ControlNo { get; set; } = "";
    public string Partcode { get; set; } = "";
    public string Partname { get; set; } = "";
    public string Section { get; set; } = "";
    public string? Status { get; set; }
    public DateTime DateAdded { get; set; }

    /// Most recent scan before this inventory month, or null if the JIG was never scanned.
    public DateTime? LastScanned { get; set; }
    public string? LastScanStatus { get; set; }
}

public sealed record SectionMissing(string Section, int Total, int Missing)
{
    public int Scanned => Total - Missing;
    public double Percentage => Total == 0 ? 0 : Math.Round(Scanned * 100.0 / Total, 1);
}

/// Unscanned JIGs for one inventory month. Uses the same rule as the Inventory page (ListMissingHandler):
/// a JIG is expected in a month when it existed by the 1st, and missing when it has no scan in that month.
public sealed class MissingJigsHandler(IDbConnectionFactory db)
{
    /// A null section means all sections.
    public async Task<List<MissingJigRow>> Handle(string? section, int year, int month, CancellationToken ct = default)
    {
        using var con = await db.OpenAsync(ct);
        var rows = await con.QueryAsync<MissingJigRow>("""
            SELECT jm.jig_id, jm.control_no, jm.partcode, jm.partname, jm.section, jm.status, jm.timestamp AS date_added,
                   last.last_scanned, last.last_scan_status
            FROM jig_masterlist jm
            LEFT JOIN jig_inventory ji ON ji.jig_id = jm.jig_id AND ji.scan_month = @month AND ji.scan_year = @year
            LEFT JOIN (
                SELECT x.jig_id, x.timestamp AS last_scanned, x.inventory_status AS last_scan_status
                FROM jig_inventory x
                JOIN (SELECT jig_id, MAX(record_id) AS record_id FROM jig_inventory WHERE timestamp < @fromDate GROUP BY jig_id) m
                  ON m.record_id = x.record_id
            ) last ON last.jig_id = jm.jig_id
            WHERE ji.jig_id IS NULL AND DATE(jm.timestamp) <= @fromDate AND (@section IS NULL OR jm.section = @section)
            ORDER BY jm.section, jm.control_no
            """, new { section, year, month, fromDate = new DateTime(year, month, 1) });
        return rows.AsList();
    }
}

/// Expected and missing JIGs per section for one month, with the same rule as MissingJigsHandler,
/// so the counts always match the list.
public sealed class MissingBySectionHandler(IDbConnectionFactory db)
{
    public async Task<List<SectionMissing>> Handle(int year, int month, CancellationToken ct = default)
    {
        using var con = await db.OpenAsync(ct);
        var rows = await con.QueryAsync<(string Section, long Total, long Missing)>("""
            SELECT jm.section,
                   COUNT(*) AS total,
                   SUM(CASE WHEN s.jig_id IS NULL THEN 1 ELSE 0 END) AS missing
            FROM jig_masterlist jm
            LEFT JOIN (SELECT DISTINCT jig_id FROM jig_inventory WHERE scan_month = @month AND scan_year = @year) s
                   ON s.jig_id = jm.jig_id
            WHERE DATE(jm.timestamp) <= @fromDate
            GROUP BY jm.section
            """, new { year, month, fromDate = new DateTime(year, month, 1) });
        var bySection = rows.ToDictionary(r => r.Section ?? "", r => new SectionMissing(r.Section ?? "", (int)r.Total, (int)r.Missing));

        // Known sections first (WinForms order, even when empty), then any other value found in the table.
        return Lookups.Sections
            .Concat(bySection.Keys.Where(s => !Lookups.Sections.Contains(s)).Order())
            .Select(s => bySection.GetValueOrDefault(s) ?? new SectionMissing(s, 0, 0))
            .ToList();
    }
}
