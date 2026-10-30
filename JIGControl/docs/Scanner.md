# JIGControl.Scanner (scanning station)
Back: [[INDEX]] · Rules: [[Database]] · Web counterpart: [[Web-App]] `/scan`

Small **.NET 10 WinForms** app for the PCs with barcode scanners. One station = one section. Same MySQL database and `jig_users` accounts as the other apps.

## Setup (per PC)
1. Publish: `dotnet publish JIGControl.Scanner -c Release -r win-x64 --self-contained` (or install the .NET 10 Desktop Runtime and copy the normal build).
2. Copy `station.example.json` to `station.json` next to `JIGControl.Scanner.exe` and set:
   - `ConnectionString` — same DB as the web app (`trcsystem`).
   - `Section` — this station's section; must be one of the section list. Operators cannot change it.
3. `station.json` is git-ignored (it holds the DB password).

## Screens
- **LoginForm** — username/password against `trc_user.jig_users` (plain text, same as [[Risks]] #2); shows the station section.
- **ScanForm** — month progress for the section (scanned / expected, %, missing), status picker (`Lookups.JigStatuses`), QR box (scanner sends Enter), green/red result banner with sound, latest 30 scans of the section this month, *Sign out* returns to login.
- **MissingForm** — the section's unscanned JIGs this month with last scan date (*Never* in red) and a filter box.

## Scan rules (`ScannerDb.ScanAsync`)
Same as web `ScanJigHandler` / WinForms `JIGQueries.InsertJIG` — QR `TRCJIG|<jig_id>`, JIG must exist, one scan per month (duplicate key → "already scanned") — **plus**: the JIG's `section` must equal the station's section, otherwise it is rejected and nothing is written.

Progress and missing use the shared month rule (expected = existed by the 1st), so the numbers match the web Missing page and Dashboard.

## Code
`StationConfig.cs` (config + section/status lists copied from web `Lookups`), `ScannerDb.cs` (all SQL, Dapper + MySqlConnector), `Forms/` (UI built in code, no designer files), `Program.cs` (login → scan loop).
