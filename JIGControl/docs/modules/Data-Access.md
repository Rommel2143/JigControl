# Data access
Back: [[INDEX]] · `SystemFramework/Database/`

- `conString.ConnectionString` / `GetConnection()` — single MySQL connection string.
- `dbQueries`: `LoadGrid` (2 overloads, one parameterized), `LoadComboBox`, `GetDataTable`, `ExecuteScalar(query, Dictionary params)`.
- Many forms bypass these and open `MySqlConnection` directly. See [[Database]], [[Risks]].
- Web counterpart: `Common/Db.cs` — `IDbConnectionFactory` / `MySqlConnectionFactory` + Dapper, connection string from config ([[Web-App]]).
