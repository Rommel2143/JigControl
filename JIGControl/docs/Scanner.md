# JIGControl.Scanner (scanning station)
Back: [[INDEX]] · Rules: [[Database]] · Web counterpart: [[Web-App]] `/scan`

Small **.NET 10 WinForms** app for the PCs with barcode scanners. One station = one section. It talks only to the **web API** ([[Web-App]] `/api`) — no database connection or DB password on the station. Same `jig_users` accounts as the other apps.

## Setup (per PC)
1. Publish: `dotnet publish JIGControl.Scanner -c Release -r win-x64 --self-contained` (or install the .NET 10 Desktop Runtime and copy the normal build).
2. Start the app. On a new install it opens **SettingsForm** (also the *⚙ Settings* button on the login screen):
   - Web app address — the running JIGControl.Web, e.g. `http://server:5183` (must be reachable from the station).
   - Section — this station's section. Operators cannot change it.
   - Administrator username/password — saving signs in to the entered address (`POST /api/auth/login` + `/api/me`), so a wrong address or section fails and non-admins are refused.
3. Settings are saved to `%ProgramData%\JIGControl\Scanner\station.json` (writable even when installed in Program Files). A `station.json` next to the exe is still read when that file is absent (development; git-ignored, `station.example.json` shows the format).

## Screens
- **LoginForm** — `POST /api/auth/login` with username, password and the station section → 12 h bearer token locked to that section (passwords are still plain text in the DB, [[Risks]] #2). When the token expires the station shows a message and returns to login. *⚙ Settings* opens SettingsForm; after a save the login screen reopens for the new section.
- **ScanForm** — month progress for the section (scanned / expected, %, missing), status picker (`Lookups.JigStatuses`), QR box (scanner sends Enter), green/red result banner with sound, latest 30 scans of the section this month, *Sign out* returns to login.
- **MissingForm** — the section's unscanned JIGs this month with last scan date (*Never* in red) and a filter box.

## Scan rules (server side, `ScanJigHandler` via `POST /api/scans`)
Same as web `ScanJigHandler` / WinForms `JIGQueries.InsertJIG` — QR `TRCJIG|<jig_id>`, JIG must exist, one scan per month (duplicate key → "already scanned") — **plus**: the JIG's `section` must equal the token's station section, otherwise it is rejected and nothing is written.

Progress and missing use the shared month rule (expected = existed by the 1st), so the numbers match the web Missing page and Dashboard.

## Code
`StationConfig.cs` (config + section/status lists copied from web `Lookups`), `ScannerApi.cs` (HttpClient wrapper for `/api`, `SessionExpiredException` on 401), `Forms/` (UI built in code, no designer files), `Program.cs` (login → scan loop).
