# JIGControl — Vault Index

Desktop app (C# WinForms, .NET Framework 4.8, namespace `QCInventoryF2`) for tracking QC **JIGs** (fixtures): master list, monthly QR-scan inventory, missing-JIG reports, user accounts.

## Start here
- [[Architecture]] — layers, startup, navigation shell
- [[Database]] — MySQL schema & every query location
- [[Flows]] — login, monthly scan, reporting, auto-update
- [[Risks]] — security & code-quality issues found
- [[Graphify]] — how to query the knowledge graph
- [[Web-App]] — Blazor .NET 10 web version (VSA, Dapper, Fluent UI, JIG photos)

## Modules
- [[modules/Account]] · [[modules/JIG]] · [[modules/Reporting]] · [[modules/Dashboard]] · [[modules/Data-Access]]

## Stack
WinForms + **Guna.UI2** 2.0.4.7 · **MySql.Data** 9.7 · **ClosedXML** 0.105 (Excel export) · **Microsoft.Reporting.WinForms** (RDLC) · solution `JIGControl.slnx` → project `SystemFramework/JIGControl.csproj`.
