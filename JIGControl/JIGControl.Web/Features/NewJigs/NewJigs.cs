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
public sealed record NewJigYear(int StartTotal, int[] Added, int[] Totals);

/// jig_masterlist.timestamp is the date a JIG was added (the same column the missing-JIG logic uses).
/// A null section means all sections. Deleted JIGs are gone from the table, so they are not counted.
public sealed class NewJigCountsHandler(IDbConnectionFactory db)
{
    public async Task<NewJigYear> Handle(string? section, int year, CancellationToken ct = default)
    {
        var from = new DateTime(year, 1, 1);
        using var con = await db.OpenAsync(ct);
        var start = await con.ExecuteScalarAsync<long>(
            "SELECT COUNT(*) FROM jig_masterlist WHERE timestamp < @from AND (@section IS NULL OR section = @section)",
            new { section, from });
        var rows = await con.QueryAsync<(int Month, long Total)>("""
            SELECT MONTH(timestamp) AS month, COUNT(*) AS total
            FROM jig_masterlist
            WHERE timestamp >= @from AND timestamp < @to AND (@section IS NULL OR section = @section)
            GROUP BY MONTH(timestamp)
            """, new { section, from, to = from.AddYears(1) });

        var added = new int[12];
        foreach (var r in rows) added[r.Month - 1] = (int)r.Total;
        var totals = new int[12];
        var running = (int)start;
        for (var m = 0; m < 12; m++) totals[m] = running += added[m];
        return new NewJigYear((int)start, added, totals);
    }
}

public sealed class ListNewJigsHandler(IDbConnectionFactory db)
{
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
