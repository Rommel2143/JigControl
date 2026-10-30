using Dapper;
using MySqlConnector;

namespace JIGControl.Scanner;

public sealed class Operator
{
    public int UserId { get; set; }
    public string Username { get; set; } = "";
    public string Firstname { get; set; } = "";
    public string Lastname { get; set; } = "";
    public string DisplayName => $"{Firstname} {Lastname}".Trim() is { Length: > 0 } n ? n : Username;
}

public sealed record ScanResult(bool Ok, string Message);

public sealed record Progress(int Expected, int Scanned)
{
    public int Missing => Expected - Scanned;
    public double Percentage => Expected == 0 ? 0 : Math.Round(Scanned * 100.0 / Expected, 1);
}

public sealed class ScanRow
{
    public string ControlNo { get; set; } = "";
    public string Partname { get; set; } = "";
    public string InventoryStatus { get; set; } = "";
    public string User { get; set; } = "";
    public DateTime Timestamp { get; set; }
}

public sealed class MissingRow
{
    public string ControlNo { get; set; } = "";
    public string Partcode { get; set; } = "";
    public string Partname { get; set; } = "";
    public string? Status { get; set; }
    public DateTime? LastScanned { get; set; }
}

/// All database access for the scanner. Rules match JIGControl.Web (ScanJigHandler, MissingJigsHandler):
/// QR = "TRCJIG|<jig_id>", one scan per JIG per month (DB unique index), expected = JIGs that existed by the 1st.
/// The scanner adds one rule: the JIG must belong to the station's section.
public sealed class ScannerDb(StationConfig config)
{
    static ScannerDb() => DefaultTypeMap.MatchNamesWithUnderscores = true; // jig_id -> JigId

    private async Task<MySqlConnection> OpenAsync()
    {
        var con = new MySqlConnection(config.ConnectionString);
        await con.OpenAsync();
        return con;
    }

    /// Passwords are stored as plain text by the existing apps; kept compatible here.
    public async Task<Operator?> LoginAsync(string username, string password)
    {
        await using var con = await OpenAsync();
        return await con.QuerySingleOrDefaultAsync<Operator>(
            "SELECT user_id, username, firstname, lastname FROM trc_user.jig_users WHERE username = @username AND password = @password",
            new { username, password });
    }

    public async Task<ScanResult> ScanAsync(string qrCode, string status, int userId)
    {
        qrCode = qrCode.Trim();
        var parts = qrCode.Split('|');
        if (parts.Length != 2 || !qrCode.Contains("TRCJIG") || !int.TryParse(parts[1], out var jigId))
            return new(false, "Invalid JIG QR code format.");

        await using var con = await OpenAsync();
        var jig = await con.QuerySingleOrDefaultAsync<(string ControlNo, string Section)?>(
            "SELECT control_no, section FROM jig_masterlist WHERE jig_id = @jigId", new { jigId });
        if (jig is null)
            return new(false, "This JIG QR code does not exist in the master list.");
        if (!string.Equals(jig.Value.Section, config.Section, StringComparison.OrdinalIgnoreCase))
            return new(false, $"{jig.Value.ControlNo} belongs to {jig.Value.Section}, not {config.Section}. Not scanned.");

        try
        {
            await con.ExecuteAsync("""
                INSERT INTO jig_inventory (QRcode, jig_id, user_id, inventory_status, inventory_remarks)
                VALUES (@qrCode, @jigId, @userId, @status, '')
                """, new { qrCode, jigId, userId, status });
            return new(true, $"{jig.Value.ControlNo} scanned as {status}.");
        }
        catch (MySqlException ex) when (ex.ErrorCode == MySqlErrorCode.DuplicateKeyEntry)
        {
            return new(false, $"{jig.Value.ControlNo} was already scanned this month.");
        }
    }

    public async Task<Progress> ProgressAsync(DateTime month)
    {
        var from = new DateTime(month.Year, month.Month, 1);
        await using var con = await OpenAsync();
        var r = await con.QuerySingleAsync<(long Expected, long Scanned)>("""
            SELECT COUNT(*) AS expected,
                   COALESCE(SUM(CASE WHEN s.jig_id IS NULL THEN 0 ELSE 1 END), 0) AS scanned
            FROM jig_masterlist jm
            LEFT JOIN (SELECT DISTINCT jig_id FROM jig_inventory WHERE scan_month = @m AND scan_year = @y) s ON s.jig_id = jm.jig_id
            WHERE jm.section = @section AND DATE(jm.timestamp) <= @from
            """, new { section = config.Section, m = month.Month, y = month.Year, from });
        return new((int)r.Expected, (int)r.Scanned);
    }

    public async Task<List<ScanRow>> RecentScansAsync(DateTime month, int limit = 30)
    {
        await using var con = await OpenAsync();
        var rows = await con.QueryAsync<ScanRow>("""
            SELECT jm.control_no, jm.partname, ji.inventory_status,
                   CONCAT(ju.firstname, ' ', ju.lastname) AS user, ji.timestamp
            FROM jig_inventory ji
            JOIN jig_masterlist jm ON jm.jig_id = ji.jig_id
            JOIN trc_user.jig_users ju ON ju.user_id = ji.user_id
            WHERE ji.scan_month = @m AND ji.scan_year = @y AND jm.section = @section
            ORDER BY ji.record_id DESC
            LIMIT @limit
            """, new { section = config.Section, m = month.Month, y = month.Year, limit });
        return rows.AsList();
    }

    public async Task<List<MissingRow>> MissingAsync(DateTime month)
    {
        var from = new DateTime(month.Year, month.Month, 1);
        await using var con = await OpenAsync();
        var rows = await con.QueryAsync<MissingRow>("""
            SELECT jm.control_no, jm.partcode, jm.partname, jm.status,
                   (SELECT MAX(x.timestamp) FROM jig_inventory x WHERE x.jig_id = jm.jig_id AND x.timestamp < @from) AS last_scanned
            FROM jig_masterlist jm
            LEFT JOIN jig_inventory ji ON ji.jig_id = jm.jig_id AND ji.scan_month = @m AND ji.scan_year = @y
            WHERE jm.section = @section AND ji.jig_id IS NULL AND DATE(jm.timestamp) <= @from
            ORDER BY jm.control_no
            """, new { section = config.Section, m = month.Month, y = month.Year, from });
        return rows.AsList();
    }
}
