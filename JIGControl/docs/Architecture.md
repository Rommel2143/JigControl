# Architecture
Back: [[INDEX]]

Three front ends share one MySQL database ([[Database]]): the WinForms desktop app (`SystemFramework/`, below), the Blazor web app ([[Web-App]]) and the section scanning station ([[Scanner]]).

## WinForms

```
Program.Main ──> Login (dialog) ──OK──> MainFrame ──hosts──> subFrame (nav shell)
                                                         ├─ Dashboard (+ DashboardCards)
                                                         ├─ JIG.ManageJIG / AddJIG
                                                         ├─ JIG.JIGInventory  (scan + reports menu)
                                                         └─ Account.accountSettings
```
- **Form-in-panel navigation**: `MainFrame.displayForm` and `subFrame.displayForm` embed child Forms (`TopLevel=false`, docked fill) into a panel. `subFrame` sets `lblTittle` to the child's `Text`.
- **No service layer**: forms call static helpers ([[modules/Data-Access]]) or write SQL inline.
- **Session state**: static class `User` (UserID, UserName, isAdmin, userSection from `Settings.Default.Section`).
- **Graph god nodes**: `ReportDataset` (49 edges, generated typed DataSet), `JIGInventory` (33), `subFrame` (24), `AddJIG`, `ManageJIG`, `manageOtherUser`.

## Web (JIGControl.Web)
```
Program.cs ──> cookie auth ──> Routes (AuthorizeRouteView) ──> MainLayout ──> Features/<Slice>/*Page.razor
                                                                              └─ @inject <X>Handler ──> IDbConnectionFactory ──> MySQL
```
- **Vertical slices**: one folder per feature; handlers auto-registered by `SliceRegistration.AddFeatureSlices`.
- **Minimal-API endpoints** for things Blazor pages can't do: `MapAuthEndpoints`, `MapJigPhotoEndpoints`, `MapReportEndpoints`.
