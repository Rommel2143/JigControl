# Graph Report - JIGControl  (2026-10-30)

## Corpus Check
- 123 files · ~39,070 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 32 file(s) not represented in the graph (top: .resx 18, (none) 3, .rdlc 3)

## Summary
- 1233 nodes · 2324 edges · 78 communities (62 shown, 16 thin omitted)
- Extraction: 90% EXTRACTED · 10% INFERRED · 0% AMBIGUOUS · INFERRED: 222 edges (avg confidence: 0.89)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `f96264f5`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- WinForms Namespaces & Deps
- ScanPage.razor
- JIG_SummaryRow
- Add User & Account Forms
- DashboardPage.razor
- JIG Data Typed Row
- Web JIG Edit & Photo UI
- EventArgs
- Reports.cs
- Row Change Events
- Web Inventory Page
- AdminDashboardPage.razor
- Web Users Admin Page
- JIG Missing Table
- DashboardCards
- JIG Inventory Screen
- PhotoStorage
- subFrame
- JIG Data Table
- Web Account Page
- Add JIG Form
- DB Connection (Both Apps)
- Launch Settings
- Resources & Settings
- ManageJIG
- jig_masterlist & Web Risks
- JIG Summary Table
- Web Reports Page
- Web Main Layout
- Shared Month Logic
- Components/_Imports.razor
- NewJigsPage.razor
- NewJigs.cs
- MissingPage.razor
- Manage Other Users
- Web Login Page
- Vault Architecture Docs
- Features/_Imports.razor
- MissingJigRow
- Stack & Excel Export
- JIGControl.Web (Blazor)
- Web Project & Packages
- DateTime
- JIGControl.Web.Common
- Users & Passwords
- JigListPage.razor
- ScannerApi
- Reconnect Modal JS
- .MapApiEndpoints
- ReportDataset
- Web Routing
- Claims Helpers
- Login Flows (Both Apps)
- Web App Root
- ScannedRow
- JigItem
- UserRow Model
- Vault Index
- Minor Cluster 61
- JIG module
- Minor Cluster 63
- Minor Cluster 64
- ScanJig.cs
- dbQueries
- Form
- NewJigRow
- ScanForm
- Missing.cs
- IDbConnectionFactory
- subFrame (nav shell)
- NavigationManager

## God Nodes (most connected - your core abstractions)
1. `ReportDataset` - 52 edges
2. `JIGInventory` - 37 edges
3. `JIG_DataDataTable` - 29 edges
4. `JIG_DataRow` - 28 edges
5. `subFrame` - 28 edges
6. `IDbConnectionFactory` - 27 edges
7. `JIG_MissingDataTable` - 26 edges
8. `JIG_SummaryDataTable` - 25 edges
9. `AddJIG` - 24 edges
10. `JIGControl.Web.Common` - 23 edges

## Surprising Connections (you probably didn't know these)
- `Code` --references--> `SessionExpiredException`  [INFERRED]
  docs/Scanner.md → JIGControl.Scanner/ScannerApi.cs
- `Architecture` --references--> `manageOtherUser`  [INFERRED]
  docs/Architecture.md → SystemFramework/Account/manageOtherUser.Designer.cs
- `Account module` --references--> `manageOtherUser`  [INFERRED]
  docs/modules/Account.md → SystemFramework/Account/manageOtherUser.Designer.cs
- `Account module` --references--> `setSection`  [INFERRED]
  docs/modules/Account.md → SystemFramework/Account/setSection.Designer.cs
- `Architecture` --references--> `AddJIG`  [INFERRED]
  docs/Architecture.md → SystemFramework/JIG/AddJIG.Designer.cs

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Shared per-month inventory logic** — docs_database_month_logic, docs_modules_jig_jiginventory, docs_modules_reporting_reportdataset, docs_modules_dashboard_dashboard, docs_web_app_listmissinghandler, docs_database_missing_jig_definition [EXTRACTED 1.00]
- **Web JIG photo storage** — docs_web_app_uploadjigphotohandler, docs_web_app_photostorage, docs_web_app_jigphotoendpoints, docs_database_imageurl_column, docs_risks_photo_upload_content_trust [INFERRED 0.85]
- **Monthly QR scan pipeline (both apps)** — docs_modules_jig_jiginventory, docs_modules_jig_jigqueries_insertjig, docs_web_app_scanjighandler, docs_database_qr_code_format, docs_database_unique_monthly_scan_constraint, docs_database_jig_inventory [INFERRED 0.95]

## Communities (78 total, 16 thin omitted)

### Community 0 - "WinForms Namespaces & Deps"
Cohesion: 0.13
Nodes (11): QCInventoryF2.Database, QCInventoryF2.Model, QCInventoryF2.usercontrol, QCInventoryF2.JIG.Reporting, QCInventoryF2.JIG, QCInventoryF2.Reports, QCInventoryF2, QCInventoryF2.Account (+3 more)

### Community 1 - "ScanPage.razor"
Cohesion: 0.07
Nodes (24): MissingRow, ControlNo, JigId, Partcode, Partname, Section, LoadAsync, OnAfterRenderAsync (+16 more)

### Community 2 - "JIG_SummaryRow"
Cohesion: 0.10
Nodes (11): JIG_MissingRow, control_no, jig_id, partcode, partname, section, JIG_SummaryRow, percentage (+3 more)

### Community 4 - "Add User & Account Forms"
Cohesion: 0.11
Nodes (6): Account module, accountSettings, AddUser, changeCredentials, changePassword, User

### Community 5 - "DashboardPage.razor"
Cohesion: 0.11
Nodes (17): JIGControl.Web.Features.Inventory, OnInitializedAsync, Pct, AuthenticationState, FluentAnchor, FluentCard, FluentIcon, FluentProgressRing (+9 more)

### Community 6 - "JIG Data Typed Row"
Cohesion: 0.11
Nodes (9): JIG_DataRow, control_no, inventory_status, jig_id, partcode, partname, section, timestamp (+1 more)

### Community 7 - "Web JIG Edit & Photo UI"
Cohesion: 0.07
Nodes (20): ChildContent, FluentInputFile, FluentInputFileEventArgs, FluentProgress, DeleteAsync, OnParametersSetAsync, OnPhotoUploadedAsync, AuthenticationState (+12 more)

### Community 8 - "EventArgs"
Cohesion: 0.09
Nodes (5): Reporting, MissingJigReport, printData, printGraph, ExportData

### Community 9 - "Reports.cs"
Cohesion: 0.11
Nodes (16): JIGControl.Web.Features.NewJigs, MonthCard, Percentage, ScanMonth, ScanYear, TotalMaster, TotalScanned, MonthCardsHandler (+8 more)

### Community 10 - "Row Change Events"
Cohesion: 0.13
Nodes (9): JIG_DataRowChangeEvent, Action, Row, JIG_MissingRowChangeEvent, Action, Row, JIG_SummaryRowChangeEvent, Action (+1 more)

### Community 11 - "Web Inventory Page"
Cohesion: 0.10
Nodes (18): FluentTab, FluentTabs, ExportUrl, LoadAsync, OnInitializedAsync, AuthenticationState, FluentAnchor, FluentDataGrid (+10 more)

### Community 12 - "AdminDashboardPage.razor"
Cohesion: 0.10
Nodes (18): Bar, LabelBars, LoadAsync, MissingUrl, OnInitializedAsync, Pct, FluentAnchor, FluentCard (+10 more)

### Community 13 - "Web Users Admin Page"
Cohesion: 0.11
Nodes (14): AddAsync, OnInitializedAsync, FluentButton, FluentCard, FluentDataGrid, FluentProgressRing, FluentStack, FluentTextField (+6 more)

### Community 14 - "JIG Missing Table"
Cohesion: 0.13
Nodes (7): JIG_MissingDataTable, control_noColumn, Count, jig_idColumn, partcodeColumn, partnameColumn, sectionColumn

### Community 15 - "DashboardCards"
Cohesion: 0.20
Nodes (4): Dashboard, DashboardCards (LoadSummary), MonthCardsHandler, DashboardCards

### Community 17 - "PhotoStorage"
Cohesion: 0.22
Nodes (5): JIGControl.Web.Features.Jigs, PhotoStorage, Root, RemoveJigPhotoHandler, UploadJigPhotoHandler

### Community 19 - "JIG Data Table"
Cohesion: 0.14
Nodes (10): JIG_DataDataTable, control_noColumn, Count, inventory_statusColumn, jig_idColumn, partcodeColumn, partnameColumn, sectionColumn (+2 more)

### Community 20 - "Web Account Page"
Cohesion: 0.12
Nodes (12): ChangePasswordAsync, OnInitializedAsync, AntiforgeryToken, AuthenticationState, FluentButton, FluentCard, FluentMessageBar, FluentStack (+4 more)

### Community 22 - "DB Connection (Both Apps)"
Cohesion: 0.14
Nodes (14): IDbConnectionFactory, MainLayout, Database/conString.cs (hard-coded connection string), MySQL server PTI-027s, DB trcsystem, Common/Db.cs (IDbConnectionFactory + Dapper), conString.ConnectionString / GetConnection(), dbQueries (LoadGrid, LoadComboBox, GetDataTable, ExecuteScalar), Risk #1: Hard-coded DB credentials (+6 more)

### Community 23 - "Launch Settings"
Cohesion: 0.13
Nodes (15): ASPNETCORE_ENVIRONMENT, applicationUrl, commandName, dotnetRunMessages, environmentVariables, launchBrowser, applicationUrl, commandName (+7 more)

### Community 24 - "Resources & Settings"
Cohesion: 0.13
Nodes (9): QCInventoryF2.Properties, Resources, Culture, deleteKey, ResourceManager, trash_bin, Settings, Default (+1 more)

### Community 26 - "jig_masterlist & Web Risks"
Cohesion: 0.14
Nodes (13): jig_masterlist.imageUrl (001_jig_image_url.sql), jig_masterlist table, AddJIG, ManageJIG, Model.JIG (getDetails), Risk #7: Antiforgery not enforced on minimal-API form posts, Risk #6: Web JIG delete not admin-gated, Risk #8: Photo upload trusts browser contentType/extension (+5 more)

### Community 27 - "JIG Summary Table"
Cohesion: 0.16
Nodes (7): Reporting module, JIG_SummaryDataTable, Count, percentageColumn, sectionColumn, total_jigColumn, total_missingColumn

### Community 28 - "Web Reports Page"
Cohesion: 0.13
Nodes (13): LoadAsync, OnInitializedAsync, FluentAnchor, FluentCard, FluentDataGrid, FluentDivider, FluentNumberField, FluentProgressRing (+5 more)

### Community 29 - "Web Main Layout"
Cohesion: 0.14
Nodes (13): AuthorizeView, FluentDesignTheme, FluentDialogProvider, FluentMessageBarProvider, FluentNavLink, FluentNavMenu, FluentPersona, FluentToastProvider (+5 more)

### Community 30 - "Shared Month Logic"
Cohesion: 0.14
Nodes (16): jig_inventory table, Missing JIG = existed by the 1st, no scan row that month, QR code format TRCJIG|<jig_id>, InserttoDB, JIGQueries.InsertJIG, Web scan flow (/scan), Microsoft.Reporting.WinForms (RDLC), JIGInventory (+8 more)

### Community 31 - "Components/_Imports.razor"
Cohesion: 0.14
Nodes (13): JIGControl.Web, JIGControl.Web.Components, JIGControl.Web.Components.Layout, Microsoft.AspNetCore.Authorization, Microsoft.AspNetCore.Components.Authorization, Microsoft.AspNetCore.Components.Forms, Microsoft.AspNetCore.Components.Routing, Microsoft.AspNetCore.Components.Web (+5 more)

### Community 32 - "NewJigsPage.razor"
Cohesion: 0.10
Nodes (17): LoadYearAsync, OnInitializedAsync, FluentAnchor, FluentCard, FluentDataGrid, FluentDivider, FluentNumberField, FluentPaginator (+9 more)

### Community 33 - "NewJigs.cs"
Cohesion: 0.46
Nodes (5): ListNewJigsHandler, NewJigDashboard, NewJigDashboardHandler, NewJigYear, SectionNewJigs

### Community 34 - "MissingPage.razor"
Cohesion: 0.08
Nodes (22): Bar, LoadAsync, OnInitializedAsync, AuthenticationState, FluentAnchor, FluentCard, FluentDataGrid, FluentDivider (+14 more)

### Community 36 - "Web Login Page"
Cohesion: 0.17
Nodes (10): DataAnnotationsValidator, EditForm, InputSelect, InputText, OnInitialized, PageTitle, SignInAsync, route:/login (+2 more)

### Community 37 - "Vault Architecture Docs"
Cohesion: 0.24
Nodes (6): Architecture, Database, Flows, Monthly JIG scan, Web JIG photo upload flow, Graphify knowledge graph

### Community 38 - "Features/_Imports.razor"
Cohesion: 0.14
Nodes (13): JIGControl.Web, JIGControl.Web.Components, JIGControl.Web.Components.Layout, Microsoft.AspNetCore.Authorization, Microsoft.AspNetCore.Components.Authorization, Microsoft.AspNetCore.Components.Forms, Microsoft.AspNetCore.Components.Routing, Microsoft.AspNetCore.Components.Web (+5 more)

### Community 39 - "MissingJigRow"
Cohesion: 0.20
Nodes (10): MissingJigRow, ControlNo, DateAdded, JigId, LastScanned, LastScanStatus, Partcode, Partname (+2 more)

### Community 40 - "Stack & Excel Export"
Cohesion: 0.15
Nodes (13): Reporting flow (RDLC ReportViewer), Reports.ExportData.ToExcel, Web Excel export (/reports/export), Blazor Server web app (JIGControl.Web, .NET 10), ClosedXML (Excel export), Dapper 2.1.89, Fluent UI Blazor 4.14.5, Guna.UI2 2.0.4.7 (+5 more)

### Community 41 - "JIGControl.Web (Blazor)"
Cohesion: 0.22
Nodes (6): Carried-over risks, JIG photos, JIGControl.Web (Blazor), Layout, Setup, SliceRegistration

### Community 42 - "Web Project & Packages"
Cohesion: 0.15
Nodes (11): net10.0, net10.0-windows, JIGControl.Scanner, JIGControl.Web, ClosedXML (0.105.1), Dapper (2.1.89), Microsoft.FluentUI.AspNetCore.Components (4.14.5), Microsoft.FluentUI.AspNetCore.Components.Icons (4.14.5) (+3 more)

### Community 43 - "DateTime"
Cohesion: 0.21
Nodes (13): JIGControl.Web.Features.Admin, MissingRow, ControlNo, LastScanned, Partcode, Partname, Status, AdminActivity (+5 more)

### Community 44 - "JIGControl.Web.Common"
Cohesion: 0.10
Nodes (14): JIGControl.Web.Features.Auth, JIGControl.Web.Features.Reports, JIGControl.Web.Features.Api, JIGControl.Web.Common, AppClaims, Lookups, ApiError, ApiLoginRequest (+6 more)

### Community 45 - "Users & Passwords"
Cohesion: 0.25
Nodes (9): User static session class (UserID, UserName, isAdmin, userSection), trc_user.jig_users table, accountSettings / changePassword / changeCredentials / setSection, AddUser, manageOtherUser, User (getUser, ChangePassword, ResetPassword, UpdateCredential), Risk #2: Plain-text passwords; ResetPassword sets empty, ChangePasswordHandler / UpdateProfileHandler (+1 more)

### Community 46 - "JigListPage.razor"
Cohesion: 0.10
Nodes (18): LoadAsync, OnInitializedAsync, OpenRow, AuthenticationState, FluentButton, FluentDataGrid, FluentDivider, FluentIcon (+10 more)

### Community 47 - "ScannerApi"
Cohesion: 0.08
Nodes (17): ApiError, Operator, DisplayName, IsAdmin, UserId, Username, Progress, ScannerApi (+9 more)

### Community 48 - "Reconnect Modal JS"
Cohesion: 0.32
Nodes (6): handleReconnectStateChanged(), reconnectModal, resumeButton, retry(), retryButton, retryWhenDocumentBecomesVisible()

### Community 49 - ".MapApiEndpoints"
Cohesion: 0.13
Nodes (9): ApiEndpoints, AuthEndpoints, LoginUser, Admin, Firstname, Lastname, UserId, Username (+1 more)

### Community 50 - "ReportDataset"
Cohesion: 0.09
Nodes (7): ReportDataset, JIG_Data, JIG_Missing, JIG_Summary, Relations, SchemaSerializationMode, Tables

### Community 51 - "Web Routing"
Cohesion: 0.29
Nodes (6): AuthorizeRouteView, FocusOnNavigate, Found, NotAuthorized, RedirectToLogin, Router

### Community 53 - "Login Flows (Both Apps)"
Cohesion: 0.33
Nodes (5): Login flow (WinForms), User.getUser (plain-text compare), Web login flow (/login -> cookie jigcontrol.auth), AuthEndpoints.BuildPrincipal, LoginHandler

### Community 54 - "Web App Root"
Cohesion: 0.33
Nodes (5): HeadOutlet, ImportMap, ReconnectModal, ResourcePreloader, Routes

### Community 55 - "ScannedRow"
Cohesion: 0.25
Nodes (8): ScannedRow, ControlNo, InventoryStatus, Partname, RecordId, Section, Timestamp, User

### Community 56 - "JigItem"
Cohesion: 0.20
Nodes (10): JigItem, ControlNo, ImageUrl, JigId, Partcode, Partname, PhotoSrc, Section (+2 more)

### Community 58 - "UserRow Model"
Cohesion: 0.33
Nodes (6): UserRow, Admin, Firstname, Lastname, UserId, Username

### Community 59 - "Vault Index"
Cohesion: 0.50
Nodes (4): JIGControl — Vault Index, Modules, Stack, Start here

### Community 61 - "Minor Cluster 61"
Cohesion: 0.67
Nodes (3): Graph god nodes (ReportDataset, JIGInventory, subFrame, AddJIG, ManageJIG, manageOtherUser), graphify query/path/explain/update commands, graphify-out outputs (graph.html, graph.json, GRAPH_REPORT.md)

### Community 62 - "JIG module"
Cohesion: 0.15
Nodes (9): JIG module, JIGQueries, JIG, ControlNo, JigID, Partcode, Partname, Section (+1 more)

### Community 69 - "ScanJig.cs"
Cohesion: 0.22
Nodes (5): JIGControl.Web.Features.Scan, Scan rules (server side, `ScanJigHandler` via `POST /api/scans`), MySqlConnectionFactory, ScanJigHandler, ScanResult

### Community 70 - "dbQueries"
Cohesion: 0.21
Nodes (4): Data access, Risks & Findings, conString, dbQueries

### Community 71 - "Form"
Cohesion: 0.09
Nodes (5): Login, Dashboard, Login, MainFrame, Program

### Community 72 - "NewJigRow"
Cohesion: 0.25
Nodes (8): NewJigRow, ControlNo, DateAdded, JigId, Partcode, Partname, Section, Status

### Community 73 - "ScanForm"
Cohesion: 0.05
Nodes (23): JIGControl.Scanner, JIGControl.Scanner.Forms, Code, JIGControl.Scanner (scanning station), Screens, Setup (per PC), LoginForm, NewConfig (+15 more)

### Community 74 - "Missing.cs"
Cohesion: 0.29
Nodes (6): JIGControl.Web.Features.Missing, MissingBySectionHandler, MissingJigsHandler, SectionMissing, Percentage, Scanned

### Community 75 - "IDbConnectionFactory"
Cohesion: 0.13
Nodes (16): JIGControl.Web.Features.Dashboard, JIGControl.Web.Features.Account, JIGControl.Web.Features.Users, IDbConnectionFactory, ChangePasswordHandler, UpdateProfileHandler, JigStatusCountsHandler, StatusCount (+8 more)

### Community 76 - "subFrame (nav shell)"
Cohesion: 0.33
Nodes (6): Login (dialog), MainFrame, Program.Main, subFrame (nav shell), Auto-update, Risk #5: Update exe run from network share without signature check

## Knowledge Gaps
- **376 isolated node(s):** `Operator`, `NewConfig`, `SignedOut`, `Config`, `net10.0-windows` (+371 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 581 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **16 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `ReportDataset` connect `ReportDataset` to `WinForms Namespaces & Deps`, `JIG_SummaryRow`, `Vault Architecture Docs`, `JIG Data Typed Row`, `EventArgs`, `Row Change Events`, `JIG Missing Table`, `JIG Data Table`, `.GetSchemaSerializable`, `JIG Summary Table`?**
  _High betweenness centrality (0.156) - this node is a cross-community bridge._
- **Are the 6 inferred relationships involving `ReportDataset` (e.g. with `Architecture` and `Reporting`) actually correct?**
  _`ReportDataset` has 6 INFERRED edges - model-reasoned connections that need verification._
- **What connects `Operator`, `NewConfig`, `SignedOut` to the rest of the system?**
  _376 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `WinForms Namespaces & Deps` be split into smaller, more focused modules?**
  _Cohesion score 0.12956107879428874 - nodes in this community are weakly interconnected._
- **Why does `JIGControl.Web.Common` connect `JIGControl.Web.Common` to `NewJigs.cs`, `ScanJig.cs`, `Features/_Imports.razor`, `DashboardPage.razor`, `JIGControl.Web (Blazor)`, `Missing.cs`, `IDbConnectionFactory`, `DateTime`, `Reports.cs`, `PhotoStorage`, `Components/_Imports.razor`?**
  _High betweenness centrality (0.155) - this node is a cross-community bridge._
- **Should `ScanPage.razor` be split into smaller, more focused modules?**
  _Cohesion score 0.07384615384615385 - nodes in this community are weakly interconnected._
- **Why does `Dashboard` connect `DashboardCards` to `subFrame`, `subFrame (nav shell)`, `Vault Architecture Docs`, `Shared Month Logic`?**
  _High betweenness centrality (0.119) - this node is a cross-community bridge._