# JIGControl.Web (Blazor)
Back: [[INDEX]]

Web version of the WinForms app. **Blazor Server, .NET 10, Vertical Slice Architecture, Dapper + MySqlConnector, Fluent UI Blazor 4.14 (Office look, `OfficeColor.Excel` theme)**. Same database as WinForms; see [[Database]].

## Layout
```
JIGControl.Web/
  Program.cs              DI, cookie auth, endpoint mapping
  Common/                 Db factory, claims helpers, Lookups, PhotoStorage, slice registration
  Components/             App (render-mode switch), Routes (AuthorizeRouteView), Layout (Office shell)
  Database/001_jig_image_url.sql   adds jig_masterlist.imageUrl
  Features/<Slice>/       one folder per feature: *Handler classes (Dapper SQL) + .razor page
    Auth       LoginPage (static SSR, writes cookie), /logout, /account/section
    Dashboard  KPIs + month cards
    Jigs       master list grid, add/edit/delete, **photo upload**, one per JIG (/jig-photos/{jig_id})
    Scan       QR scan → jig_inventory (same rules as JIGQueries.InsertJIG)
    Inventory  scanned / missing per month + section
    Reports    section coverage, Excel export (/reports/export?kind=scanned|missing|summary)
    Users      admin: list, add, reset password
    Account    section switch, name, change password
```
- Slices are wired automatically: every class ending in `Handler` under `Features/` is registered scoped (`SliceRegistration`).
- Dapper maps `snake_case` → PascalCase (`DefaultTypeMap.MatchNamesWithUnderscores`).
- User section lives in the auth cookie (claim `section`), so changing it is a form post that re-issues the cookie.

## JIG photo
One photo per JIG. Column **`jig_masterlist.imageUrl`** (nullable, see `Database/001_jig_image_url.sql`) holds the full path of the file on the share `\\ptif1-ds\SystemServer\Backup\BACKUP UPLOADS\JIGINVENTORY\{jig_id}\{guid}.ext` (`PhotoStorage:Path`). Uploading again replaces the photo and deletes the old file. Browsers cannot open UNC paths, so the app streams it via `/jig-photos/{jig_id}` and only serves/deletes files inside the share root. The account running the web app needs write access to the share.

## Setup
```bash
mysql ... trcsystem < JIGControl.Web/Database/001_jig_image_url.sql
dotnet user-secrets --project JIGControl.Web set "ConnectionStrings:JigDb" "server=...;user id=...;password=...;database=trcsystem"
dotnet run --project JIGControl.Web
```

## Carried-over risks
Plain-text passwords and reset-to-empty are kept for compatibility with WinForms ([[Risks]] #2). All web SQL is parameterized (fixes [[Risks]] #3 in the web app only).
