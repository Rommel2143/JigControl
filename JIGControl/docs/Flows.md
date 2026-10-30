# Flows
Back: [[INDEX]]

## Login
`Program.Main` → `Login.btnLogin_Click` → `User.getUser(username,password)` (plain-text compare) → `DialogResult.OK` → `MainFrame` → `subFrame` → Dashboard auto-clicked.

## Monthly JIG scan
`JIGInventory` text box KeyDown (scanner sends Enter) → `JIGQueries.InsertJIG(qr, status)` → validate format → check `jig_masterlist` → `InserttoDB` into `jig_inventory` with `User.UserID`. Duplicate in same month blocked by DB unique index. See [[modules/JIG]].

## Reporting
`JIGInventory` context menu → `printData` / `MissingJigReport` / `printGraph` — RDLC ReportViewer bound to `ReportDataset` tables (`JIG_Data`, `JIG_Missing`, `JIG_Summary`). Grid → Excel via `Reports.ExportData.ToExcel`. See [[modules/Reporting]].

## Auto-update
`subFrame` "Check updates" → if `\ptif1-ds\SystemServer\JIGControl\setup.exe` exists, launch it and exit.

## Web (JIGControl.Web)
- **Login**: `/login` (static SSR form) → `LoginHandler` → `AuthEndpoints.BuildPrincipal` → cookie `jigcontrol.auth`; the chosen section becomes the `section` claim.
- **Scan**: `/scan` → `ScanJigHandler.Handle(qr, status, User.UserId())` — same validation as WinForms; duplicate-month scan caught as `MySqlErrorCode.DuplicateKeyEntry`.
- **Photo**: `JigEditPage` upload → `UploadJigPhotoHandler` writes `{Root}/{jig_id}/{guid}.ext`, updates `imageUrl`, deletes the old file; the `<img>` src is `/jig-photos/{jig_id}`.
- **Missing JIGs**: `/missing` → pick month/year → `MissingBySectionHandler` (expected / scanned / missing / coverage per section) → click a section or *All sections* → `MissingJigsHandler` list with each JIG's last scan before that month (or *Never*). Defaults to the user's section.
- **Dashboard**: own-section KPIs (expected, scanned + coverage, missing + days left, new this month), scan results by status, master list by JIG status, recent scans, coverage bars for all sections, month cards.
- **New JIGs per month (admin)**: `/new-jigs` → `NewJigDashboardHandler` builds a section × month grid (each cell `+added` and the month-end total, e.g. Feb 659 → Mar 680 = +21) with an *All sections* row → click a cell → `ListNewJigsHandler` lists those JIGs by date added.
- **Export**: `/reports/export?kind=scanned|missing|summary` → ClosedXML workbook.
See [[Web-App]].
