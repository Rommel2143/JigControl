# JIG module
Back: [[INDEX]] · `SystemFramework/JIG/`, `Model/JIG.cs`

- `JIGQueries.InsertJIG` / `InserttoDB` — scan pipeline ([[Flows]]).
- `AddJIG` — insert into `jig_masterlist`, filter by section.
- `ManageJIG` — edit/delete/status of master list; `reloadJIG`, search box.
- `JIGInventory` — scan screen, month/status filters, `LoadCount`, menu to [[modules/Reporting]].
- `Model.JIG` — POCO + `getDetails()` (injectable SQL, [[Risks]] #3).
