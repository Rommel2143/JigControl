using Dapper;
using JIGControl.Web.Common;

namespace JIGControl.Web.Features.Jigs;

public sealed class GetJigHandler(IDbConnectionFactory db)
{
    public async Task<JigItem?> Handle(int jigId, CancellationToken ct = default)
    {
        using var con = await db.OpenAsync(ct);
        return await con.QuerySingleOrDefaultAsync<JigItem>(
            "SELECT jig_id, control_no, partcode, partname, section, status, updatestamp, imageUrl FROM jig_masterlist WHERE jig_id = @jigId",
            new { jigId });
    }
}

public sealed class CreateJigHandler(IDbConnectionFactory db)
{
    public async Task<int> Handle(string controlNo, string partcode, string partname, string section, CancellationToken ct = default)
    {
        using var con = await db.OpenAsync(ct);
        return await con.ExecuteScalarAsync<int>("""
            INSERT INTO jig_masterlist (control_no, partcode, partname, section) VALUES (@controlNo, @partcode, @partname, @section);
            SELECT LAST_INSERT_ID();
            """, new { controlNo, partcode, partname, section });
    }
}

public sealed class UpdateJigHandler(IDbConnectionFactory db)
{
    public async Task Handle(JigItem jig, CancellationToken ct = default)
    {
        using var con = await db.OpenAsync(ct);
        await con.ExecuteAsync("""
            UPDATE jig_masterlist
            SET control_no = @ControlNo, partcode = @Partcode, partname = @Partname, status = @Status
            WHERE jig_id = @JigId
            """, jig);
    }
}

public sealed class DeleteJigHandler(IDbConnectionFactory db, PhotoStorage storage)
{
    public async Task Handle(int jigId, CancellationToken ct = default)
    {
        using var con = await db.OpenAsync(ct);
        var files = await con.QueryAsync<string>("SELECT imageUrl FROM jig_masterlist WHERE jig_id = @jigId AND imageUrl IS NOT NULL", new { jigId });
        await con.ExecuteAsync("DELETE FROM jig_masterlist WHERE jig_id = @jigId", new { jigId });
        foreach (var f in files) storage.Delete(f);
    }
}
