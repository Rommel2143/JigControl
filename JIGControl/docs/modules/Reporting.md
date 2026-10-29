# Reporting module
Back: [[INDEX]] · `SystemFramework/JIG/Reporting/`, `Reports/ExportData.cs`

- `ReportDataset` — generated typed DataSet (most-connected graph node): `JIG_DataDataTable`, `JIG_MissingDataTable`, `JIG_SummaryDataTable`.
- Viewers: `printData`, `MissingJigReport`, `printGraph` (each has month combo → reload).
- `ExportData.ToExcel(Guna2DataGridView)` — ClosedXML export of visible columns.
