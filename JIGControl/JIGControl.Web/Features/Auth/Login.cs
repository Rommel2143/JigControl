using Dapper;
using JIGControl.Web.Common;

namespace JIGControl.Web.Features.Auth;

public sealed class LoginUser
{
    public int UserId { get; set; }
    public string Username { get; set; } = "";
    public string Firstname { get; set; } = "";
    public string Lastname { get; set; } = "";
    public bool Admin { get; set; }
}

public sealed class LoginHandler(IDbConnectionFactory db)
{
    // Passwords are stored as plain text by the WinForms app; kept compatible here.
    public async Task<LoginUser?> Handle(string username, string password, CancellationToken ct = default)
    {
        using var con = await db.OpenAsync(ct);
        return await con.QuerySingleOrDefaultAsync<LoginUser>(
            "SELECT user_id, username, firstname, lastname, admin FROM trc_user.jig_users WHERE username = @username AND password = @password",
            new { username, password });
    }
}
