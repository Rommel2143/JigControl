using Dapper;
using JIGControl.Web.Common;

namespace JIGControl.Web.Features.Dashboard;

public sealed record StatusCount(string Status, int Count);

/// Master-list JIGs of a section grouped by jig_masterlist.status (Available, In-Use, Under Repair, ...).
public sealed class JigStatusCountsHandler(IDbConnectionFactory db)
{
    public async Task<List<StatusCount>> Handle(string section, CancellationToken ct = default)
    {
        using var con = await db.OpenAsync(ct);
        var rows = await con.QueryAsync<(string? Status, long Count)>(
            "SELECT status, COUNT(*) AS count FROM jig_masterlist WHERE section = @section GROUP BY status ORDER BY count DESC",
            new { section });
        return rows.Select(r => new StatusCount(string.IsNullOrWhiteSpace(r.Status) ? "No status" : r.Status, (int)r.Count)).ToList();
    }
}
