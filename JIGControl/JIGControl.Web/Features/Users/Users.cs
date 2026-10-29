using Dapper;
using JIGControl.Web.Common;

namespace JIGControl.Web.Features.Users;

public sealed class UserRow
{
    public int UserId { get; set; }
    public string Username { get; set; } = "";
    public string Firstname { get; set; } = "";
    public string Lastname { get; set; } = "";
    public bool Admin { get; set; }
}

public sealed class ListUsersHandler(IDbConnectionFactory db)
{
    public async Task<List<UserRow>> Handle(CancellationToken ct = default)
    {
        using var con = await db.OpenAsync(ct);
        var rows = await con.QueryAsync<UserRow>(
            "SELECT user_id, username, firstname, lastname, admin FROM trc_user.jig_users ORDER BY username");
        return rows.AsList();
    }
}

public sealed class AddUserHandler(IDbConnectionFactory db)
{
    /// Returns an error message, or null on success. New users are never admins (same as WinForms).
    public async Task<string?> Handle(string username, string password, string firstname, string lastname, CancellationToken ct = default)
    {
        using var con = await db.OpenAsync(ct);
        var exists = await con.ExecuteScalarAsync<long>(
            "SELECT COUNT(*) FROM trc_user.jig_users WHERE username = @username", new { username });
        if (exists > 0) return "That username is already taken.";

        await con.ExecuteAsync("""
            INSERT INTO trc_user.jig_users (username, password, firstname, lastname, admin)
            VALUES (@username, @password, @firstname, @lastname, 0)
            """, new { username, password, firstname, lastname });
        return null;
    }
}

/// Clears the password (WinForms behavior): the user then signs in with an empty password and sets a new one.
public sealed class ResetPasswordHandler(IDbConnectionFactory db)
{
    public async Task Handle(int userId, CancellationToken ct = default)
    {
        using var con = await db.OpenAsync(ct);
        await con.ExecuteAsync("UPDATE trc_user.jig_users SET password = '' WHERE user_id = @userId", new { userId });
    }
}
