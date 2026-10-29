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
