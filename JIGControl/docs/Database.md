# Database
Back: [[INDEX]] · Code: [[modules/Data-Access]]

MySQL server `PTI-027s`, default DB `trcsystem` (connection string hard-coded in `Database/conString.cs`).

| Table | Purpose | Used by |
|---|---|---|
| `jig_masterlist` (jig_id, control_no, partcode, partname, section, status, updatestamp, imageUrl*) | master list of JIGs | AddJIG, ManageJIG, Model/JIG, JIGQueries |
| `jig_inventory` (record_id, QRcode, jig_id, user_id, inventory_status, inventory_remarks, timestamp) | monthly scan records; UNIQUE (QRcode + month/year) → MySQL error 1062 = "already scanned this month" | JIGQueries, JIGInventory, reports |
| `trc_user.jig_users` (user_id, username, password, firstname, lastname, admin) | accounts (separate schema) | User, AddUser, manageOtherUser |

QR code format: `TRCJIG|<jig_id>` (validated in `JIGQueries.InsertJIG`).

\*`imageUrl` is added by the web app migration (`JIGControl.Web/Database/001_jig_image_url.sql`): full share path of the JIG photo. See [[Web-App]].
