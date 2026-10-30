using Dapper;
using JIGControl.Web.Common;

namespace JIGControl.Web.Features.Jigs;

public sealed class ListJigsHandler(IDbConnectionFactory db)
{
    public async Task<List<JigItem>> Handle(string? section, CancellationToken ct = default)
    {
        using var con = await db.OpenAsync(ct);
        var rows = await con.QueryAsync<JigItem>("""
            SELECT jm.jig_id, jm.control_no, jm.partcode, jm.partname, jm.section, jm.status, jm.updatestamp, jm.imageUrl
            FROM jig_masterlist jm
            WHERE (@section IS NULL OR jm.section = @section)
            ORDER BY jm.section, jm.control_no
            """, new { section });
        return rows.AsList();
    }
}
