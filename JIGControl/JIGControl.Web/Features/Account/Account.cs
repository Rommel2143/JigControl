using Dapper;
using JIGControl.Web.Common;

namespace JIGControl.Web.Features.Account;

public sealed class ChangePasswordHandler(IDbConnectionFactory db)
{
    /// True when the current password matched and the new one was saved.
    public async Task<bool> Handle(int userId, string currentPassword, string newPassword, CancellationToken ct = default)
    {
        using var con = await db.OpenAsync(ct);
        var rows = await con.ExecuteAsync("""
            UPDATE trc_user.jig_users SET password = @newPassword
            WHERE user_id = @userId AND password = @currentPassword
            """, new { userId, currentPassword, newPassword });
        return rows > 0;
    }
}

public sealed class UpdateProfileHandler(IDbConnectionFactory db)
{
    public async Task Handle(int userId, string firstname, string lastname, CancellationToken ct = default)
    {
        using var con = await db.OpenAsync(ct);
        await con.ExecuteAsync(
            "UPDATE trc_user.jig_users SET firstname = @firstname, lastname = @lastname WHERE user_id = @userId",
            new { userId, firstname, lastname });
    }
}
