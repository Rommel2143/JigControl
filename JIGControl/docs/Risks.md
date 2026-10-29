# Risks & Findings
Back: [[INDEX]]

1. **Hard-coded DB credentials** in `Database/conString.cs` (committed to git).
2. **Plain-text passwords** in `jig_users`; `ResetPassword` sets password to `''`.
3. **SQL injection**: string-interpolated queries — `Model/JIG.getDetails` (`jig_id =" + JigID`), AddJIG (`section = '{cmbsection.Text}'`), JIGInventory (`section = '{User.userSection}'`), `UPDATE jig_masterlist SET status = '` ...
4. Inconsistent error handling in `dbQueries` (some swallow into MessageBox, some rethrow, `LoadComboBox` reader not in `using`).
5. Update check runs an exe from a network share with no signature check.
