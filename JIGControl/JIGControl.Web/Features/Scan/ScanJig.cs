using Dapper;
using JIGControl.Web.Common;
using MySqlConnector;

namespace JIGControl.Web.Features.Scan;

public sealed record ScanResult(bool Ok, string Message);

/// Same rules as the WinForms JIGQueries.InsertJIG: QR must be "TRCJIG|<jig_id>",
/// the JIG must exist, and the DB unique index blocks a second scan in the same month.
/// With requiredSection (scanning stations via the API), JIGs of other sections are rejected.
public sealed class ScanJigHandler(IDbConnectionFactory db)
{
    public async Task<ScanResult> Handle(string qrCode, string status, int userId, string? requiredSection = null, CancellationToken ct = default)
    {
        qrCode = qrCode.Trim();
        var parts = qrCode.Split('|');
        if (parts.Length != 2 || !qrCode.Contains("TRCJIG") || !int.TryParse(parts[1], out var jigId))
            return new(false, "Invalid JIG QR code format.");

        using var con = await db.OpenAsync(ct);
        var jig = await con.QuerySingleOrDefaultAsync<(string ControlNo, string Section)?>(
            "SELECT control_no, section FROM jig_masterlist WHERE jig_id = @jigId", new { jigId });
        if (jig is null)
            return new(false, "This JIG QR code does not exist in the master list.");
        var (controlNo, section) = jig.Value;
        if (requiredSection is not null && !string.Equals(section, requiredSection, StringComparison.OrdinalIgnoreCase))
            return new(false, $"{controlNo} belongs to {section}, not {requiredSection}. Not scanned.");

        try
        {
            await con.ExecuteAsync("""
                INSERT INTO jig_inventory (QRcode, jig_id, user_id, inventory_status, inventory_remarks)
                VALUES (@qrCode, @jigId, @userId, @status, '')
                """, new { qrCode, jigId, userId, status });
            return new(true, $"{controlNo} scanned as {status}.");
        }
        catch (MySqlException ex) when (ex.ErrorCode == MySqlErrorCode.DuplicateKeyEntry)
        {
            return new(false, $"{controlNo} was already scanned this month.");
        }
    }
}
