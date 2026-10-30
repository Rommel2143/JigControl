# Graph Report - JIGControl  (2026-10-30)

## Corpus Check
- 104 files · ~29,464 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 990 nodes · 1895 edges · 69 communities (51 shown, 18 thin omitted)
- Extraction: 90% EXTRACTED · 10% INFERRED · 0% AMBIGUOUS · INFERRED: 188 edges (avg confidence: 0.89)
- Token cost: 0 input · 0 output

## Community Hubs (Navigation)
- WinForms Namespaces & Deps
- Web Dashboard Page
- Missing-JIG Typed Rows
- Web Slice Namespaces
- Add User & Account Forms
- Report Viewers (RDLC)
- JIG Data Typed Row
- Web JIG Edit & Photo UI
- ReportDataset Core
- Row Change Events
- Web Inventory Page
- Web JIG List Page
- Web Users Admin Page
- JIG Missing Table
- WinForms Startup & Nav
- JIG Inventory Screen
- Photo Storage & Endpoints
- subFrame Navigation Shell
- JIG Data Table
- Web Account Page
- Add JIG Form
- DB Connection (Both Apps)
- Launch Settings
- Resources & Settings
- Manage JIG Form
- jig_masterlist & Web Risks
- JIG Summary Table
- Web Reports Page
- Web Main Layout
- Shared Month Logic
- Component Imports
- Feature Imports
- Account Settings & Section
- JIG Model & Scan Queries
- Manage Other Users
- Web Login Page
- Vault Architecture Docs
- Login & MainFrame
- Monthly Scan Pipeline
- Stack & Excel Export
- WinForms Data Access
- Web Project & Packages
- JigItem Model
- Web Scan Handler
- Users & Passwords
- Web Slice Registration
- Dashboard Cards
- Reconnect Modal JS
- Auth Claims Builder
- Web Routing
- Claims Helpers
- Login Flows (Both Apps)
- Web App Root
- Auth Imports
- MonthCard Model
- SectionSummary Model
- UserRow Model
- Vault Index
- Minor Cluster 61
- Minor Cluster 62
- Minor Cluster 63
- Minor Cluster 64

## God Nodes (most connected - your core abstractions)
1. `ReportDataset` - 52 edges
2. `JIGInventory` - 37 edges
3. `JIG_DataDataTable` - 29 edges
4. `JIG_DataRow` - 28 edges
5. `subFrame` - 28 edges
6. `JIG_MissingDataTable` - 26 edges
7. `JIG_SummaryDataTable` - 25 edges
8. `AddJIG` - 24 edges
9. `ManageJIG` - 22 edges
10. `manageOtherUser` - 21 edges

## Surprising Connections (you probably didn't know these)
- `Reporting module` --references--> `MissingJigReport`  [INFERRED]
  docs/modules/Reporting.md → SystemFramework/JIG/Reporting/MissingJigReport.Designer.cs
- `Reporting module` --references--> `printGraph`  [INFERRED]
  docs/modules/Reporting.md → SystemFramework/JIG/Reporting/printGraph.Designer.cs
- `Architecture` --references--> `AddJIG`  [INFERRED]
  docs/Architecture.md → SystemFramework/JIG/AddJIG.Designer.cs
- `JIG module` --references--> `AddJIG`  [INFERRED]
  docs/modules/JIG.md → SystemFramework/JIG/AddJIG.Designer.cs
- `Reporting module` --references--> `JIG_MissingDataTable`  [INFERRED]
  docs/modules/Reporting.md → SystemFramework/JIG/Reporting/ReportDataset.Designer.cs

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Monthly QR scan pipeline (both apps)** — docs_modules_jig_jiginventory, docs_modules_jig_jigqueries_insertjig, docs_web_app_scanjighandler, docs_database_qr_code_format, docs_database_unique_monthly_scan_constraint, docs_database_jig_inventory [INFERRED 0.95]
- **Web JIG photo storage** — docs_web_app_uploadjigphotohandler, docs_web_app_photostorage, docs_web_app_jigphotoendpoints, docs_database_imageurl_column, docs_risks_photo_upload_content_trust [INFERRED 0.85]
- **Shared per-month inventory logic** — docs_database_month_logic, docs_modules_jig_jiginventory, docs_modules_reporting_reportdataset, docs_modules_dashboard_dashboard, docs_web_app_listmissinghandler, docs_database_missing_jig_definition [EXTRACTED 1.00]

## Communities (69 total, 18 thin omitted)

### Community 0 - "WinForms Namespaces & Deps"
Cohesion: 0.14
Nodes (11): QCInventoryF2.Database, QCInventoryF2.Model, QCInventoryF2.usercontrol, QCInventoryF2.JIG.Reporting, QCInventoryF2.JIG, QCInventoryF2.Reports, QCInventoryF2, QCInventoryF2.Account (+3 more)

### Community 1 - "Web Dashboard Page"
Cohesion: 0.04
Nodes (43): JIGControl.Web.Features.Inventory, OnInitializedAsync, AuthenticationState, FluentCard, FluentIcon, FluentProgressRing, PageTitle, ListMissingHandler (+35 more)

### Community 2 - "Missing-JIG Typed Rows"
Cohesion: 0.10
Nodes (11): JIG_MissingRow, control_no, jig_id, partcode, partname, section, JIG_SummaryRow, percentage (+3 more)

### Community 3 - "Web Slice Namespaces"
Cohesion: 0.10
Nodes (21): JIGControl.Web.Features.Auth, JIGControl.Web.Features.Reports, JIGControl.Web.Features.Jigs, JIGControl.Web.Features.Account, JIGControl.Web.Features.Users, JIGControl.Web.Common, IDbConnectionFactory, Lookups (+13 more)

### Community 4 - "Add User & Account Forms"
Cohesion: 0.09
Nodes (6): Account module, AddUser, changeCredentials, changePassword, Login, User

### Community 5 - "Report Viewers (RDLC)"
Cohesion: 0.09
Nodes (5): Reporting, MissingJigReport, printData, printGraph, ExportData

### Community 6 - "JIG Data Typed Row"
Cohesion: 0.11
Nodes (9): JIG_DataRow, control_no, inventory_status, jig_id, partcode, partname, section, timestamp (+1 more)

### Community 7 - "Web JIG Edit & Photo UI"
Cohesion: 0.07
Nodes (20): ChildContent, FluentInputFile, FluentInputFileEventArgs, FluentProgress, DeleteAsync, OnParametersSetAsync, OnPhotoUploadedAsync, AuthenticationState (+12 more)

### Community 8 - "ReportDataset Core"
Cohesion: 0.09
Nodes (7): ReportDataset, JIG_Data, JIG_Missing, JIG_Summary, Relations, SchemaSerializationMode, Tables

### Community 10 - "Row Change Events"
Cohesion: 0.13
Nodes (9): JIG_DataRowChangeEvent, Action, Row, JIG_MissingRowChangeEvent, Action, Row, JIG_SummaryRowChangeEvent, Action (+1 more)

### Community 11 - "Web Inventory Page"
Cohesion: 0.10
Nodes (18): FluentTab, FluentTabs, ExportUrl, LoadAsync, OnInitializedAsync, AuthenticationState, FluentAnchor, FluentDataGrid (+10 more)

### Community 12 - "Web JIG List Page"
Cohesion: 0.10
Nodes (18): FluentSearch, LoadAsync, OnInitializedAsync, OpenRow, AuthenticationState, FluentButton, FluentDataGrid, FluentDivider (+10 more)

### Community 13 - "Web Users Admin Page"
Cohesion: 0.11
Nodes (14): AddAsync, OnInitializedAsync, FluentButton, FluentCard, FluentDataGrid, FluentProgressRing, FluentStack, FluentTextField (+6 more)

### Community 14 - "JIG Missing Table"
Cohesion: 0.13
Nodes (7): JIG_MissingDataTable, control_noColumn, Count, jig_idColumn, partcodeColumn, partnameColumn, sectionColumn

### Community 15 - "WinForms Startup & Nav"
Cohesion: 0.12
Nodes (10): Login (dialog), MainFrame, Program.Main, subFrame (nav shell), Auto-update, Dashboard, DashboardCards (LoadSummary), Risk #5: Update exe run from network share without signature check (+2 more)

### Community 17 - "Photo Storage & Endpoints"
Cohesion: 0.14
Nodes (6): PhotoStorage, Root, DeleteJigHandler, JigPhotoEndpoints, RemoveJigPhotoHandler, UploadJigPhotoHandler

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
Cohesion: 0.16
Nodes (13): Missing JIG = existed by the 1st, no scan row that month, Guna.UI2 2.0.4.7, Microsoft.Reporting.WinForms (RDLC), MySql.Data 9.7, WinForms desktop app (QCInventoryF2, .NET Framework 4.8), JIGInventory, JIGQueries.InsertJIG / InserttoDB, Features/Reports (SectionSummaryHandler + Excel export endpoint) (+5 more)

### Community 31 - "Component Imports"
Cohesion: 0.14
Nodes (13): JIGControl.Web, JIGControl.Web.Components, JIGControl.Web.Components.Layout, Microsoft.AspNetCore.Authorization, Microsoft.AspNetCore.Components.Authorization, Microsoft.AspNetCore.Components.Forms, Microsoft.AspNetCore.Components.Routing, Microsoft.AspNetCore.Components.Web (+5 more)

### Community 32 - "Feature Imports"
Cohesion: 0.14
Nodes (13): JIGControl.Web, JIGControl.Web.Components, JIGControl.Web.Components.Layout, Microsoft.AspNetCore.Authorization, Microsoft.AspNetCore.Components.Authorization, Microsoft.AspNetCore.Components.Forms, Microsoft.AspNetCore.Components.Routing, Microsoft.AspNetCore.Components.Web (+5 more)

### Community 34 - "JIG Model & Scan Queries"
Cohesion: 0.19
Nodes (9): JIG module, JIGQueries, JIG, ControlNo, JigID, Partcode, Partname, Section (+1 more)

### Community 36 - "Web Login Page"
Cohesion: 0.17
Nodes (10): DataAnnotationsValidator, EditForm, InputSelect, InputText, OnInitialized, PageTitle, SignInAsync, route:/login (+2 more)

### Community 37 - "Vault Architecture Docs"
Cohesion: 0.30
Nodes (4): Architecture, Database, Web JIG photo upload flow, Graphify knowledge graph

### Community 38 - "Login & MainFrame"
Cohesion: 0.21
Nodes (3): Login, MainFrame, Program

### Community 39 - "Monthly Scan Pipeline"
Cohesion: 0.27
Nodes (8): jig_inventory table, QR code format TRCJIG|<jig_id>, Flows, InserttoDB, JIGQueries.InsertJIG, Monthly JIG scan, Web scan flow (/scan), ScanJigHandler

### Community 40 - "Stack & Excel Export"
Cohesion: 0.18
Nodes (10): Reporting flow (RDLC ReportViewer), Reports.ExportData.ToExcel, Web Excel export (/reports/export), Blazor Server web app (JIGControl.Web, .NET 10), ClosedXML (Excel export), Dapper 2.1.89, Fluent UI Blazor 4.14.5, JIGControl system (QC JIG tracking) (+2 more)

### Community 41 - "WinForms Data Access"
Cohesion: 0.27
Nodes (4): Data access, Risks & Findings, conString, dbQueries

### Community 42 - "Web Project & Packages"
Cohesion: 0.20
Nodes (9): net10.0, JIGControl.Web, ClosedXML (0.105.1), Dapper (2.1.89), Microsoft.FluentUI.AspNetCore.Components (4.14.5), Microsoft.FluentUI.AspNetCore.Components.Icons (4.14.5), MySqlConnector (2.6.2), Microsoft.NET.Sdk.Web (+1 more)

### Community 43 - "JigItem Model"
Cohesion: 0.20
Nodes (10): JigItem, ControlNo, ImageUrl, JigId, Partcode, Partname, PhotoSrc, Section (+2 more)

### Community 44 - "Web Scan Handler"
Cohesion: 0.25
Nodes (4): JIGControl.Web.Features.Scan, MySqlConnectionFactory, ScanJigHandler, ScanResult

### Community 45 - "Users & Passwords"
Cohesion: 0.25
Nodes (9): User static session class (UserID, UserName, isAdmin, userSection), trc_user.jig_users table, accountSettings / changePassword / changeCredentials / setSection, AddUser, manageOtherUser, User (getUser, ChangePassword, ResetPassword, UpdateCredential), Risk #2: Plain-text passwords; ResetPassword sets empty, ChangePasswordHandler / UpdateProfileHandler (+1 more)

### Community 46 - "Web Slice Registration"
Cohesion: 0.22
Nodes (6): Carried-over risks, JIG photos, JIGControl.Web (Blazor), Layout, Setup, SliceRegistration

### Community 48 - "Reconnect Modal JS"
Cohesion: 0.32
Nodes (6): handleReconnectStateChanged(), reconnectModal, resumeButton, retry(), retryButton, retryWhenDocumentBecomesVisible()

### Community 49 - "Auth Claims Builder"
Cohesion: 0.25
Nodes (7): AuthEndpoints, LoginUser, Admin, Firstname, Lastname, UserId, Username

### Community 51 - "Web Routing"
Cohesion: 0.29
Nodes (6): AuthorizeRouteView, FocusOnNavigate, Found, NotAuthorized, RedirectToLogin, Router

### Community 53 - "Login Flows (Both Apps)"
Cohesion: 0.33
Nodes (5): Login flow (WinForms), User.getUser (plain-text compare), Web login flow (/login -> cookie jigcontrol.auth), AuthEndpoints.BuildPrincipal, LoginHandler

### Community 54 - "Web App Root"
Cohesion: 0.33
Nodes (5): HeadOutlet, ImportMap, ReconnectModal, ResourcePreloader, Routes

### Community 55 - "Auth Imports"
Cohesion: 0.33
Nodes (3): AppClaims, Microsoft.AspNetCore.Authentication, Microsoft.AspNetCore.Authentication.Cookies

### Community 56 - "MonthCard Model"
Cohesion: 0.33
Nodes (6): MonthCard, Percentage, ScanMonth, ScanYear, TotalMaster, TotalScanned

### Community 57 - "SectionSummary Model"
Cohesion: 0.33
Nodes (6): SectionSummary, Percentage, Scanned, Section, TotalJig, TotalMissing

### Community 58 - "UserRow Model"
Cohesion: 0.33
Nodes (6): UserRow, Admin, Firstname, Lastname, UserId, Username

### Community 59 - "Vault Index"
Cohesion: 0.50
Nodes (4): JIGControl — Vault Index, Modules, Stack, Start here

### Community 61 - "Minor Cluster 61"
Cohesion: 0.67
Nodes (3): Graph god nodes (ReportDataset, JIGInventory, subFrame, AddJIG, ManageJIG, manageOtherUser), graphify query/path/explain/update commands, graphify-out outputs (graph.html, graph.json, GRAPH_REPORT.md)

## Knowledge Gaps
- **287 isolated node(s):** `control_no`, `jig_id`, `partcode`, `partname`, `section` (+282 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 445 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **18 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `ReportDataset` connect `ReportDataset Core` to `WinForms Namespaces & Deps`, `Missing-JIG Typed Rows`, `Report Viewers (RDLC)`, `Vault Architecture Docs`, `JIG Data Typed Row`, `Row Change Events`, `JIG Missing Table`, `Typed Schema Serialization`, `JIG Data Table`, `JIG Summary Table`?**
  _High betweenness centrality (0.210) - this node is a cross-community bridge._
- **Are the 6 inferred relationships involving `ReportDataset` (e.g. with `Architecture` and `Reporting`) actually correct?**
  _`ReportDataset` has 6 INFERRED edges - model-reasoned connections that need verification._
- **What connects `control_no`, `jig_id`, `partcode` to the rest of the system?**
  _287 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `WinForms Namespaces & Deps` be split into smaller, more focused modules?**
  _Cohesion score 0.14143775569842199 - nodes in this community are weakly interconnected._
- **Why does `Dashboard` connect `WinForms Startup & Nav` to `subFrame Navigation Shell`, `Shared Month Logic`?**
  _High betweenness centrality (0.152) - this node is a cross-community bridge._
- **Are the 5 inferred relationships involving `JIGInventory` (e.g. with `Architecture` and `Monthly JIG scan`) actually correct?**
  _`JIGInventory` has 5 INFERRED edges - model-reasoned connections that need verification._
- **Should `Web Dashboard Page` be split into smaller, more focused modules?**
  _Cohesion score 0.044326241134751775 - nodes in this community are weakly interconnected._