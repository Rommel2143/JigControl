# JIGControl — Vault Index

System for tracking QC **JIGs** (fixtures): master list, monthly QR-scan inventory, missing-JIG reports, user accounts. Two front ends on one MySQL DB:
- **Desktop** — C# WinForms, .NET Framework 4.8, namespace `QCInventoryF2` (`SystemFramework/`)
- **Web** — Blazor Server, .NET 10, namespace `JIGControl.Web` (`JIGControl.Web/`)
- **Scanner** — WinForms, .NET 10, namespace `JIGControl.Scanner` (`JIGControl.Scanner/`), one section per station

## Start here
- [[Architecture]] — layers, startup, navigation shell
- [[Database]] — MySQL schema & every query location
- [[Flows]] — login, monthly scan, reporting, auto-update
- [[Risks]] — security & code-quality issues found (WinForms #1–5, web #6–9; #6–7 fixed)
- [[Graphify]] — how to query the knowledge graph
- [[Web-App]] — Blazor .NET 10 web version (VSA, Dapper, Fluent UI, JIG photos)
- [[Scanner]] — .NET 10 WinForms scanning station, locked to one section per PC

## Modules
- [[modules/Account]] · [[modules/JIG]] · [[modules/Reporting]] · [[modules/Dashboard]] · [[modules/Data-Access]]

## Stack
**Desktop:** WinForms + **Guna.UI2** 2.0.4.7 · **MySql.Data** 9.7 · **ClosedXML** 0.105 (Excel export) · **Microsoft.Reporting.WinForms** (RDLC) · solution `JIGControl.slnx` → project `SystemFramework/JIGControl.csproj`.
**Web:** Blazor Server .NET 10 · Fluent UI Blazor 4.14.5 · Dapper 2.1.89 · MySqlConnector 2.6.2 · ClosedXML 0.105.1 · project `JIGControl.Web/JIGControl.Web.csproj`.

_Last refreshed 2026-10-30 (graph built from commit `4c70094`)._
