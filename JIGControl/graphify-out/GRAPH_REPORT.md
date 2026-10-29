# Graph Report - JIGControl  (2026-10-29)

## Corpus Check
- 104 files · ~29,451 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 30 file(s) not represented in the graph (top: .resx 18, .rdlc 3, .css 2)

## Summary
- 908 nodes · 1791 edges · 62 communities (43 shown, 19 thin omitted)
- Extraction: 93% EXTRACTED · 7% INFERRED · 0% AMBIGUOUS · INFERRED: 132 edges (avg confidence: 0.9)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `ea75b50b`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- Namespaces & NuGet Deps
- Missing-JIG Typed Rows
- ReportDataset Core
- JIG Data Typed Row
- Row Change Events
- Manage Other Users
- JIG Summary Table
- JIG Inventory Screen
- subFrame Navigation Shell
- JIG Data Table
- Missing/Print Reports
- Resources & Settings
- Add JIG Form
- Add User & Credentials
- Missing-JIG Table
- Manage JIG Form
- Account Settings & Section
- JIG Model & Key Handlers
- Dashboard & App Root
- JigListPage.razor
- .OpenAsync
- JigEditPage.razor
- Dashboard Cards
- dbQueries Helpers
- Form
- DB Connection & JIG Inserts
- JIGControl.Web
- INDEX.md
- Reports.cs
- InventoryPage.razor
- ScanPage.razor
- UsersPage.razor
- AccountPage.razor
- http
- ReportsPage.razor
- MainLayout.razor
- JigItem
- JigPhotos.cs
- Components/_Imports.razor
- Features/_Imports.razor
- JIGControl.Web.Common
- DashboardPage.razor
- LoginUser
- JigPhoto
- ReconnectModal.razor.js
- ScannedRow
- Routes.razor
- dapper
- ClaimsPrincipal
- Account module
- App.razor
- MissingRow
- UserRow
- printData
- Reporting
- FluentBadge
- EmptyLayout.razor
- 001_jig_photos.sql

## God Nodes (most connected - your core abstractions)
1. `ReportDataset` - 52 edges
2. `JIGInventory` - 37 edges
3. `JIG_DataDataTable` - 29 edges
4. `JIG_DataRow` - 28 edges
5. `subFrame` - 28 edges
6. `JIG_MissingDataTable` - 26 edges
7. `JIG_SummaryDataTable` - 25 edges
8. `AddJIG` - 24 edges
9. `IDbConnectionFactory` - 22 edges
10. `ManageJIG` - 22 edges

## Surprising Connections (you probably didn't know these)
- `Auto-update` --references--> `subFrame`  [INFERRED]
  docs/Flows.md → SystemFramework/subFrame.Designer.cs
- `Account module` --references--> `AddUser`  [INFERRED]
  docs/modules/Account.md → SystemFramework/Account/AddUser.Designer.cs
- `Account module` --references--> `accountSettings`  [INFERRED]
  docs/modules/Account.md → SystemFramework/Account/accountSettings.Designer.cs
- `Account module` --references--> `changeCredentials`  [INFERRED]
  docs/modules/Account.md → SystemFramework/Account/changeCredentials.Designer.cs
- `Account module` --references--> `changePassword`  [INFERRED]
  docs/modules/Account.md → SystemFramework/Account/changePassword.Designer.cs

## Import Cycles
- None detected.

## Communities (62 total, 19 thin omitted)

### Community 0 - "Namespaces & NuGet Deps"
Cohesion: 0.12
Nodes (11): QCInventoryF2.Database, QCInventoryF2.Model, QCInventoryF2.usercontrol, QCInventoryF2.JIG.Reporting, QCInventoryF2.JIG, QCInventoryF2.Reports, QCInventoryF2, QCInventoryF2.Account (+3 more)

### Community 1 - "Missing-JIG Typed Rows"
Cohesion: 0.10
Nodes (11): JIG_MissingRow, control_no, jig_id, partcode, partname, section, JIG_SummaryRow, percentage (+3 more)

### Community 2 - "ReportDataset Core"
Cohesion: 0.09
Nodes (7): ReportDataset, JIG_Data, JIG_Missing, JIG_Summary, Relations, SchemaSerializationMode, Tables

### Community 3 - "JIG Data Typed Row"
Cohesion: 0.11
Nodes (9): JIG_DataRow, control_no, inventory_status, jig_id, partcode, partname, section, timestamp (+1 more)

### Community 4 - "Row Change Events"
Cohesion: 0.13
Nodes (9): JIG_DataRowChangeEvent, Action, Row, JIG_MissingRowChangeEvent, Action, Row, JIG_SummaryRowChangeEvent, Action (+1 more)

### Community 6 - "JIG Summary Table"
Cohesion: 0.16
Nodes (7): Reporting module, JIG_SummaryDataTable, Count, percentageColumn, sectionColumn, total_jigColumn, total_missingColumn

### Community 9 - "JIG Data Table"
Cohesion: 0.14
Nodes (10): JIG_DataDataTable, control_noColumn, Count, inventory_statusColumn, jig_idColumn, partcodeColumn, partnameColumn, sectionColumn (+2 more)

### Community 11 - "Resources & Settings"
Cohesion: 0.13
Nodes (9): QCInventoryF2.Properties, Resources, Culture, deleteKey, ResourceManager, trash_bin, Settings, Default (+1 more)

### Community 13 - "Add User & Credentials"
Cohesion: 0.12
Nodes (4): AddUser, changeCredentials, changePassword, Login

### Community 14 - "Missing-JIG Table"
Cohesion: 0.13
Nodes (7): JIG_MissingDataTable, control_noColumn, Count, jig_idColumn, partcodeColumn, partnameColumn, sectionColumn

### Community 17 - "JIG Model & Key Handlers"
Cohesion: 0.17
Nodes (7): JIG, ControlNo, JigID, Partcode, Partname, Section, Status

### Community 19 - "JigListPage.razor"
Cohesion: 0.06
Nodes (29): DataAnnotationsValidator, EditForm, FluentSearch, InputSelect, InputText, OnInitialized, OnInitialized, PageTitle (+21 more)

### Community 20 - ".OpenAsync"
Cohesion: 0.14
Nodes (13): JIGControl.Web.Features.Account, JIGControl.Web.Features.Users, IDbConnectionFactory, MySqlConnectionFactory, ChangePasswordHandler, UpdateProfileHandler, CreateJigHandler, GetJigHandler (+5 more)

### Community 21 - "JigEditPage.razor"
Cohesion: 0.07
Nodes (21): ChildContent, FluentInputFile, FluentInputFileEventArgs, FluentProgress, DeleteAsync, DeletePhotoAsync, OnParametersSetAsync, OnPhotosUploadedAsync (+13 more)

### Community 23 - "dbQueries Helpers"
Cohesion: 0.43
Nodes (3): Data access, Risks & Findings, dbQueries

### Community 24 - "Form"
Cohesion: 0.21
Nodes (3): Login, MainFrame, Program

### Community 25 - "DB Connection & JIG Inserts"
Cohesion: 0.32
Nodes (4): Database, JIG module, conString, JIGQueries

### Community 27 - "JIGControl.Web"
Cohesion: 0.20
Nodes (8): net10.0, JIGControl.Web, ClosedXML (0.105.1), Dapper (2.1.89), Microsoft.FluentUI.AspNetCore.Components (4.14.5), Microsoft.FluentUI.AspNetCore.Components.Icons (4.14.5), MySqlConnector (2.6.2), Microsoft.NET.Sdk.Web

### Community 28 - "INDEX.md"
Cohesion: 0.12
Nodes (11): Graphify knowledge graph, JIGControl — Vault Index, Modules, Stack, Start here, Carried-over risks, JIG photos, JIGControl.Web (Blazor) (+3 more)

### Community 29 - "Reports.cs"
Cohesion: 0.11
Nodes (16): JIGControl.Web.Features.Reports, MonthCard, Percentage, ScanMonth, ScanYear, TotalMaster, TotalScanned, MonthCardsHandler (+8 more)

### Community 30 - "InventoryPage.razor"
Cohesion: 0.10
Nodes (18): FluentTab, FluentTabs, ExportUrl, LoadAsync, OnInitializedAsync, AuthenticationState, FluentAnchor, FluentDataGrid (+10 more)

### Community 31 - "ScanPage.razor"
Cohesion: 0.10
Nodes (18): LoadAsync, OnAfterRenderAsync, OnInitializedAsync, OnKeyDownAsync, AuthenticationState, FluentButton, FluentCard, FluentDataGrid (+10 more)

### Community 32 - "UsersPage.razor"
Cohesion: 0.11
Nodes (14): AddAsync, OnInitializedAsync, FluentButton, FluentCard, FluentDataGrid, FluentProgressRing, FluentStack, FluentTextField (+6 more)

### Community 33 - "AccountPage.razor"
Cohesion: 0.12
Nodes (12): ChangePasswordAsync, OnInitializedAsync, AntiforgeryToken, AuthenticationState, FluentButton, FluentCard, FluentMessageBar, FluentStack (+4 more)

### Community 34 - "http"
Cohesion: 0.13
Nodes (15): ASPNETCORE_ENVIRONMENT, applicationUrl, commandName, dotnetRunMessages, environmentVariables, launchBrowser, applicationUrl, commandName (+7 more)

### Community 35 - "ReportsPage.razor"
Cohesion: 0.13
Nodes (13): LoadAsync, OnInitializedAsync, FluentAnchor, FluentCard, FluentDataGrid, FluentDivider, FluentNumberField, FluentProgressRing (+5 more)

### Community 36 - "MainLayout.razor"
Cohesion: 0.14
Nodes (13): AuthorizeView, FluentDesignTheme, FluentDialogProvider, FluentMessageBarProvider, FluentNavLink, FluentNavMenu, FluentPersona, FluentToastProvider (+5 more)

### Community 37 - "JigItem"
Cohesion: 0.15
Nodes (11): JIGControl.Web.Features.Jigs, JigItem, ControlNo, JigId, Partcode, Partname, PhotoId, Section (+3 more)

### Community 38 - "JigPhotos.cs"
Cohesion: 0.20
Nodes (6): PhotoStorage, Root, DeleteJigHandler, DeleteJigPhotoHandler, JigPhotoEndpoints, UploadJigPhotoHandler

### Community 39 - "Components/_Imports.razor"
Cohesion: 0.14
Nodes (13): JIGControl.Web, JIGControl.Web.Components, JIGControl.Web.Components.Layout, Microsoft.AspNetCore.Authorization, Microsoft.AspNetCore.Components.Authorization, Microsoft.AspNetCore.Components.Forms, Microsoft.AspNetCore.Components.Routing, Microsoft.AspNetCore.Components.Web (+5 more)

### Community 40 - "Features/_Imports.razor"
Cohesion: 0.14
Nodes (13): JIGControl.Web, JIGControl.Web.Components, JIGControl.Web.Components.Layout, Microsoft.AspNetCore.Authorization, Microsoft.AspNetCore.Components.Authorization, Microsoft.AspNetCore.Components.Forms, Microsoft.AspNetCore.Components.Routing, Microsoft.AspNetCore.Components.Web (+5 more)

### Community 41 - "JIGControl.Web.Common"
Cohesion: 0.19
Nodes (6): JIGControl.Web.Features.Auth, JIGControl.Web.Common, AppClaims, Lookups, Microsoft.AspNetCore.Authentication, Microsoft.AspNetCore.Authentication.Cookies

### Community 42 - "DashboardPage.razor"
Cohesion: 0.18
Nodes (11): JIGControl.Web.Features.Inventory, OnInitializedAsync, AuthenticationState, FluentCard, FluentIcon, FluentProgressRing, PageTitle, ListMissingHandler (+3 more)

### Community 43 - "LoginUser"
Cohesion: 0.17
Nodes (8): AuthEndpoints, LoginHandler, LoginUser, Admin, Firstname, Lastname, UserId, Username

### Community 44 - "JigPhoto"
Cohesion: 0.22
Nodes (8): JigPhoto, ContentType, FileName, JigId, OriginalName, PhotoId, SizeBytes, UploadedAt

### Community 45 - "ReconnectModal.razor.js"
Cohesion: 0.32
Nodes (6): handleReconnectStateChanged(), reconnectModal, resumeButton, retry(), retryButton, retryWhenDocumentBecomesVisible()

### Community 46 - "ScannedRow"
Cohesion: 0.25
Nodes (8): ScannedRow, ControlNo, InventoryStatus, Partname, RecordId, Section, Timestamp, User

### Community 48 - "Routes.razor"
Cohesion: 0.29
Nodes (6): AuthorizeRouteView, FocusOnNavigate, Found, NotAuthorized, RedirectToLogin, Router

### Community 49 - "dapper"
Cohesion: 0.33
Nodes (3): JIGControl.Web.Features.Scan, ScanJigHandler, ScanResult

### Community 52 - "App.razor"
Cohesion: 0.33
Nodes (5): HeadOutlet, ImportMap, ReconnectModal, ResourcePreloader, Routes

### Community 53 - "MissingRow"
Cohesion: 0.33
Nodes (6): MissingRow, ControlNo, JigId, Partcode, Partname, Section

### Community 54 - "UserRow"
Cohesion: 0.33
Nodes (6): UserRow, Admin, Firstname, Lastname, UserId, Username

### Community 56 - "Reporting"
Cohesion: 0.50
Nodes (4): Auto-update, Flows, Monthly JIG scan, Reporting

## Knowledge Gaps
- **273 isolated node(s):** `Root`, `ResourcePreloader`, `ImportMap`, `HeadOutlet`, `Routes` (+268 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 425 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **19 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `ReportDataset` connect `ReportDataset Core` to `Namespaces & NuGet Deps`, `Missing-JIG Typed Rows`, `JIG Data Typed Row`, `Row Change Events`, `JIG Summary Table`, `subFrame Navigation Shell`, `JIG Data Table`, `Missing/Print Reports`, `Missing-JIG Table`, `.GetSchemaSerializable`, `printData`, `Reporting`?**
  _High betweenness centrality (0.218) - this node is a cross-community bridge._
- **Are the 6 inferred relationships involving `ReportDataset` (e.g. with `Architecture` and `Reporting`) actually correct?**
  _`ReportDataset` has 6 INFERRED edges - model-reasoned connections that need verification._
- **What connects `Root`, `ResourcePreloader`, `ImportMap` to the rest of the system?**
  _273 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `Namespaces & NuGet Deps` be split into smaller, more focused modules?**
  _Cohesion score 0.12251984126984126 - nodes in this community are weakly interconnected._
- **Why does `IDbConnectionFactory` connect `.OpenAsync` to `JigItem`, `JigPhotos.cs`, `DashboardPage.razor`, `LoginUser`, `dapper`, `Reports.cs`?**
  _High betweenness centrality (0.195) - this node is a cross-community bridge._
- **Are the 5 inferred relationships involving `JIGInventory` (e.g. with `Architecture` and `Monthly JIG scan`) actually correct?**
  _`JIGInventory` has 5 INFERRED edges - model-reasoned connections that need verification._
- **Should `Missing-JIG Typed Rows` be split into smaller, more focused modules?**
  _Cohesion score 0.09747899159663866 - nodes in this community are weakly interconnected._