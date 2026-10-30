using Dapper;
using JIGControl.Web.Common;

namespace JIGControl.Web.Features.NewJigs;

public sealed class NewJigRow
{
    public int JigId { get; set; }
    public string ControlNo { get; set; } = "";
    public string Partcode { get; set; } = "";
    public string Partname { get; set; } = "";
    public string Section { get; set; } = "";
    public string? Status { get; set; }
    public DateTime DateAdded { get; set; }
}

/// Index 0 = January. Totals[m] is the master-list size at the end of that month, so
/// Totals[m] - Totals[m - 1] == Added[m] (e.g. Feb 659 + 21 added = Mar 680).
/// StartTotal is the size before January 1 of the year.
public sealed record NewJigYear(int StartTotal, int[] Added, int[] Totals)
{
    public static NewJigYear From(int start, int[] added)
    {
        var totals = new int[12];
        var running = start;
        for (var m = 0; m < 12; m++) totals[m] = running += added[m];
        return new NewJigYear(start, added, totals);
    }
}

public sealed record SectionNewJigs(string Section, NewJigYear Year);

/// One row per section plus the all-sections total.
public sealed record NewJigDashboard(NewJigYear All, List<SectionNewJigs> Sections);

/// jig_masterlist.timestamp is the date a JIG was added (the same column the missing-JIG logic uses).
/// Deleted JIGs are gone from the table, so they are not counted.
public sealed class NewJigDashboardHandler(IDbConnectionFactory db)
{
    public async Task<NewJigDashboard> Handle(int year, CancellationToken ct = default)
    {
        var from = new DateTime(year, 1, 1);
        using var con = await db.OpenAsync(ct);
        var starts = (await con.QueryAsync<(string Section, long Total)>(
            "SELECT section, COUNT(*) AS total FROM jig_masterlist WHERE timestamp < @from GROUP BY section",
            new { from })).ToDictionary(r => r.Section ?? "", r => (int)r.Total);
        var months = await con.QueryAsync<(string Section, int Month, long Total)>("""
            SELECT section, MONTH(timestamp) AS month, COUNT(*) AS total
            FROM jig_masterlist
            WHERE timestamp >= @from AND timestamp < @to
            GROUP BY section, MONTH(timestamp)
            """, new { from, to = from.AddYears(1) });

        var added = new Dictionary<string, int[]>();
        foreach (var r in months)
        {
            var key = r.Section ?? "";
            if (!added.TryGetValue(key, out var a)) added[key] = a = new int[12];
            a[r.Month - 1] = (int)r.Total;
        }

        // Known sections first (in the WinForms order, even when empty), then any other value found in the table.
        var names = Lookups.Sections
            .Concat(starts.Keys.Concat(added.Keys).Where(s => !Lookups.Sections.Contains(s)).Distinct().Order());
        var sections = names
            .Select(s => new SectionNewJigs(s, NewJigYear.From(starts.GetValueOrDefault(s), added.GetValueOrDefault(s) ?? new int[12])))
            .ToList();

        var allAdded = new int[12];
        foreach (var s in sections) for (var m = 0; m < 12; m++) allAdded[m] += s.Year.Added[m];
        return new NewJigDashboard(NewJigYear.From(sections.Sum(s => s.Year.StartTotal), allAdded), sections);
    }
}

public sealed class ListNewJigsHandler(IDbConnectionFactory db)
{
    /// A null section means all sections.
    public async Task<List<NewJigRow>> Handle(string? section, int year, int month, CancellationToken ct = default)
    {
        using var con = await db.OpenAsync(ct);
        var from = new DateTime(year, month, 1);
        var rows = await con.QueryAsync<NewJigRow>("""
            SELECT jig_id, control_no, partcode, partname, section, status, timestamp AS date_added
            FROM jig_masterlist
            WHERE timestamp >= @from AND timestamp < @to AND (@section IS NULL OR section = @section)
            ORDER BY timestamp DESC, control_no
            """, new { section, from, to = from.AddMonths(1) });
        return rows.AsList();
    }
}
