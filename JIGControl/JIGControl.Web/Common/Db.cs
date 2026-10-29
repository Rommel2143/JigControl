using System.Data;
using MySqlConnector;

namespace JIGControl.Web.Common;

public interface IDbConnectionFactory
{
    Task<IDbConnection> OpenAsync(CancellationToken ct = default);
}

public sealed class MySqlConnectionFactory(IConfiguration config) : IDbConnectionFactory
{
    private readonly string _cs = config.GetConnectionString("JigDb")
        ?? throw new InvalidOperationException("Connection string 'JigDb' is missing. Set it with: dotnet user-secrets set \"ConnectionStrings:JigDb\" \"...\"");

    public async Task<IDbConnection> OpenAsync(CancellationToken ct = default)
    {
        var con = new MySqlConnection(_cs);
        await con.OpenAsync(ct);
        return con;
    }
}
