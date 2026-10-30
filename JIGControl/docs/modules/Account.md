# Account module
Back: [[INDEX]] · `SystemFramework/Account/`, `User/User.cs`

- `User` (static): `getUser`, `ChangePassword`, `ResetPassword`, `UpdateCredential` against `trc_user.jig_users` ([[Database]]).
- Forms: `accountSettings` (hub), `changePassword`, `changeCredentials`, `setSection` (writes `Settings.Default.Section`), `AddUser` (admin=0), `manageOtherUser` (grid + reset-password icon column).
- Risks: [[Risks]] #2.
- Web counterpart: `Features/Auth`, `Features/Account`, `Features/Users` ([[Web-App]]); `/users` is admin-only.
