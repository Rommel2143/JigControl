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
    NewJigs    JIGs added per month (count cards + list with date added), own section or all
    Reports    section coverage, Excel export (/reports/export?kind=scanned|missing|summary|new, `&all=true` for every section on `new`)
    Users      admin: list, add, reset password
    Account    section switch, name, change password
```
- Slices are wired automatically: every class ending in `Handler` under `Features/` is registered scoped (`SliceRegistration`).
- Dapper maps `snake_case` → PascalCase (`DefaultTypeMap.MatchNamesWithUnderscores`).
- User section lives in the auth cookie (claim `section`), so changing it is a form post that re-issues the cookie.

## Routes
| Route | Slice | Access |
|---|---|---|
| `/login` | Auth (static SSR, `ExcludeFromInteractiveRouting`) | anonymous |
| `/` | Dashboard (`MonthCardsHandler`) | signed in |
| `/jigs`, `/jigs/new`, `/jigs/{id}` | Jigs | signed in; Delete / Remove photo admin-only ([[Risks]] #6) |
| `/scan` | Scan (`ScanJigHandler`) | signed in |
| `/new-jigs` | NewJigs (`NewJigCountsHandler`, `ListNewJigsHandler`) | signed in |
| `/inventory` | Inventory (`ListScannedHandler`, `ListMissingHandler`) | signed in |
| `/reports`, `/reports/export` | Reports (`SectionSummaryHandler`, ClosedXML) | signed in |
| `/users` | Users | **Admin role** |
| `/account`, POST `/account/section` | Account / Auth | signed in |
| GET `/jig-photos/{jigId}` | Jigs (`JigPhotoEndpoints`) | signed in |
| POST `/logout` | Auth | signed in |

## WinForms ↔ Web map
| WinForms | Web slice / handler |
|---|---|
| `Login` + `User.getUser` | `LoginHandler` → `AuthEndpoints.BuildPrincipal` (claims: id, name, given name, `section`, Admin role) |
| `User.userSection` / `Settings.Default.Section` | `section` claim in cookie `jigcontrol.auth` (12 h sliding) |
| `JIGQueries.InsertJIG` | `ScanJigHandler` (duplicate → `MySqlErrorCode.DuplicateKeyEntry`) |
| `AddJIG` / `ManageJIG` | `ListJigsHandler`, `GetJigHandler`, `Create/Update/DeleteJigHandler` |
| `JIGInventory` grids | `ListScannedHandler`, `ListMissingHandler` |
| `printGraph` / `Dashboard` cards | `SectionSummaryHandler` / `MonthCardsHandler` |
| `AddUser`, `manageOtherUser` | `AddUserHandler`, `ListUsersHandler`, `ResetPasswordHandler` |
| `changePassword`, `changeCredentials` | `ChangePasswordHandler`, `UpdateProfileHandler` |
| `conString` (hard-coded) | `MySqlConnectionFactory` reading `ConnectionStrings:JigDb` (user-secrets) |
| `Lookups` values | copied from WinForms combo boxes (sections, statuses) |

## JIG photo
One photo per JIG. Column **`jig_masterlist.imageUrl`** (nullable, see `Database/001_jig_image_url.sql`) holds the full path of the file on the share `\\ptif1-ds\SystemServer\Backup\BACKUP UPLOADS\JIGINVENTORY\{jig_id}\{guid}.ext` (`PhotoStorage:Path`). Uploading again replaces the photo and deletes the old file. Browsers cannot open UNC paths, so the app streams it via `/jig-photos/{jig_id}` and only serves/deletes files inside the share root. The account running the web app needs write access to the share.

## Setup
```bash
mysql ... trcsystem < JIGControl.Web/Database/001_jig_image_url.sql
dotnet user-secrets --project JIGControl.Web set "ConnectionStrings:JigDb" "server=...;user id=...;password=...;database=trcsystem"
dotnet run --project JIGControl.Web   # or the `jigcontrol-web` preview in .claude/launch.json (port 5183)
```

## Carried-over risks
Plain-text passwords and reset-to-empty are kept for compatibility with WinForms ([[Risks]] #2). All web SQL is parameterized (fixes [[Risks]] #3 in the web app only). Web-only issues: [[Risks]] #6–#8 (#6, #7 fixed).
