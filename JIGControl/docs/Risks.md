# Risks & Findings
Back: [[INDEX]]

1. **Hard-coded DB credentials** in `Database/conString.cs` (committed to git).
2. **Plain-text passwords** in `jig_users`; `ResetPassword` sets password to `''`.
3. **SQL injection**: string-interpolated queries — `Model/JIG.getDetails` (`jig_id =" + JigID`), AddJIG (`section = '{cmbsection.Text}'`), JIGInventory (`section = '{User.userSection}'`, and the search box at `JIGInventory.cs:126`: `LIKE '%" + search + "%'`), `UPDATE jig_masterlist SET status = '` ...
4. Inconsistent error handling in `dbQueries` (some swallow into MessageBox, some rethrow, `LoadComboBox` reader not in `using`).
5. Update check runs an exe from a network share with no signature check.

## Web app (JIGControl.Web)
6. ~~**JIG delete not admin-gated**~~ — **fixed 2026-10-30**: `JigEditPage` shows Delete / Remove photo only to admins and re-checks `isAdmin` in `DeleteAsync` / `RemovePhotoAsync` (matches WinForms `AddJIG.cs:40`). Non-admins can still add/edit JIGs and upload or replace a photo.
7. ~~**Antiforgery not enforced on minimal-API form posts**~~ — **fixed 2026-10-30**: `/logout` and `/account/section` now call `IAntiforgery.IsRequestValidAsync` and return 400 without a valid token (`AuthEndpoints.cs`). Any new hand-read form endpoint must do the same.
8. Photo upload trusts the browser's `contentType` + extension only (no magic-byte check); files are served back only from inside `PhotoStorage.Root` (`IsInside`), which limits the impact.

9. **Two definitions of "expected this month"** — **fixed in the web app 2026-10-30**: every web count (Reports `SectionSummaryHandler`, Dashboard `MonthCardsHandler`, Inventory, Missing) now uses *existed by the 1st*; month cards count only scans of those JIGs. **Still open in WinForms**: `printGraph` (`ReportDataset` JIG_Summary) counts JIGs that existed by the *end* of the month, so desktop and web coverage can differ for months where JIGs were added. See [[Database]] month logic.
See [[Web-App]].
