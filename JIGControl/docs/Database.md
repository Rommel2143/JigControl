# Database
Back: [[INDEX]] · Code: [[modules/Data-Access]]

MySQL server `PTI-027s`, default DB `trcsystem` (connection string hard-coded in `Database/conString.cs`).

| Table | Purpose | Used by |
|---|---|---|
| `jig_masterlist` (jig_id, control_no, partcode, partname, section, status, timestamp, updatestamp, imageUrl*) | master list of JIGs | AddJIG, ManageJIG, Model/JIG, JIGQueries |
| `jig_inventory` (record_id, QRcode, jig_id, user_id, inventory_status, inventory_remarks, timestamp, scan_month, scan_year) | monthly scan records; UNIQUE (QRcode + scan_month/scan_year) → MySQL error 1062 = "already scanned this month" | JIGQueries, JIGInventory, reports |
| `trc_user.jig_users` (user_id, username, password, firstname, lastname, admin) | accounts (separate schema) | User, AddUser, manageOtherUser |

QR code format: `TRCJIG|<jig_id>` (validated in `JIGQueries.InsertJIG` and web `ScanJigHandler`).

## Month logic (shared by both apps)
- Every per-month query filters `jig_inventory.scan_month` / `scan_year` (never `MONTH(timestamp)`): `JIGInventory`, `ReportDataset`, `Dashboard`, web `InventoryQueries`, `Reports`.
- A JIG counts toward a month only if `jig_masterlist.timestamp` (creation) is on/before that month — "missing" = existed by the 1st, no scan row that month (`ListMissingHandler`).

\*`imageUrl` is added by the web app migration (`JIGControl.Web/Database/001_jig_image_url.sql`): full share path of the JIG photo. See [[Web-App]].
