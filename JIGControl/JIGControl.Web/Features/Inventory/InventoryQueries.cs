using Dapper;
using JIGControl.Web.Common;

namespace JIGControl.Web.Features.Inventory;

public sealed class ScannedRow
{
    public int RecordId { get; set; }
    public string ControlNo { get; set; } = "";
    public string Partname { get; set; } = "";
    public string Section { get; set; } = "";
    public string InventoryStatus { get; set; } = "";
    public string User { get; set; } = "";
    public DateTime Timestamp { get; set; }
}

public sealed class MissingRow
{
    public int JigId { get; set; }
    public string ControlNo { get; set; } = "";
    public string Partcode { get; set; } = "";
    public string Partname { get; set; } = "";
    public string Section { get; set; } = "";
}

public sealed class ListScannedHandler(IDbConnectionFactory db)
{
    public async Task<List<ScannedRow>> Handle(string section, int year, int month, CancellationToken ct = default)
    {
        using var con = await db.OpenAsync(ct);
        var rows = await con.QueryAsync<ScannedRow>("""
            SELECT ji.record_id, jm.control_no, jm.partname, jm.section, ji.inventory_status,
                   CONCAT(UPPER(LEFT(ju.firstname,1)), '. ', UPPER(LEFT(ju.lastname,1)), LOWER(SUBSTRING(ju.lastname,2))) AS user,
                   ji.timestamp
            FROM jig_inventory ji
            JOIN jig_masterlist jm ON ji.jig_id = jm.jig_id
            JOIN trc_user.jig_users ju ON ju.user_id = ji.user_id
            WHERE ji.scan_month = @month AND ji.scan_year = @year AND jm.section = @section
            ORDER BY ji.record_id DESC
            """, new { section, year, month });
        return rows.AsList();
    }
}

/// A JIG is missing for a month when it existed by the 1st of that month and has no scan in it.
public sealed class ListMissingHandler(IDbConnectionFactory db)
{
    public async Task<List<MissingRow>> Handle(string section, int year, int month, CancellationToken ct = default)
    {
        using var con = await db.OpenAsync(ct);
        var rows = await con.QueryAsync<MissingRow>("""
            SELECT jm.jig_id, jm.control_no, jm.partcode, jm.partname, jm.section
            FROM jig_masterlist jm
            LEFT JOIN jig_inventory ji ON ji.jig_id = jm.jig_id AND ji.scan_month = @month AND ji.scan_year = @year
            WHERE jm.section = @section AND ji.jig_id IS NULL AND DATE(jm.timestamp) <= @fromDate
            ORDER BY jm.control_no
            """, new { section, year, month, fromDate = new DateTime(year, month, 1) });
        return rows.AsList();
    }
}
