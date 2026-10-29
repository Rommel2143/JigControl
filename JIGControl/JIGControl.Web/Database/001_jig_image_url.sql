-- Run once against the `trcsystem` database. Adds one photo per JIG for JIGControl.Web.
-- imageUrl holds the full path of the photo on \ptif1-ds\SystemServer\Backup\BACKUP UPLOADS\JIGINVENTORY (NULL = no photo).
-- Nullable and additive: the WinForms app keeps working (its SELECT * grids just show one more column).
ALTER TABLE jig_masterlist ADD COLUMN imageUrl VARCHAR(500) NULL;
