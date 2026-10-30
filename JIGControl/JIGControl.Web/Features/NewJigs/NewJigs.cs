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

/// jig_masterlist.timestamp is the date a JIG was added (the same column the missing-JIG logic uses).
/// A null section means all sections.
public sealed class NewJigCountsHandler(IDbConnectionFactory db)
{
    /// Number of JIGs added in each month of the year; index 0 = January.
    public async Task<int[]> Handle(string? section, int year, CancellationToken ct = default)
    {
        using var con = await db.OpenAsync(ct);
        var rows = await con.QueryAsync<(int Month, long Total)>("""
            SELECT MONTH(timestamp) AS month, COUNT(*) AS total
            FROM jig_masterlist
            WHERE timestamp >= @from AND timestamp < @to AND (@section IS NULL OR section = @section)
            GROUP BY MONTH(timestamp)
            """, new { section, from = new DateTime(year, 1, 1), to = new DateTime(year + 1, 1, 1) });
        var counts = new int[12];
        foreach (var r in rows) counts[r.Month - 1] = (int)r.Total;
        return counts;
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
