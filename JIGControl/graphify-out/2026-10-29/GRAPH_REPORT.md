# Graph Report - JIGControl  (2026-10-29)

## Corpus Check
- Corpus is ~22,074 words - fits in a single context window. You may not need a graph.

## Summary
- 452 nodes · 1126 edges · 28 communities (11 shown, 17 thin omitted)
- Extraction: 97% EXTRACTED · 3% INFERRED · 0% AMBIGUOUS · INFERRED: 39 edges (avg confidence: 0.85)
- Token cost: 0 input · 0 output

## Community Hubs (Navigation)
- Namespaces & NuGet Deps
- Missing-JIG Typed Rows
- ReportDataset Core
- JIG Data Typed Row
- Row Change Events
- Manage Other Users
- JIG Summary Table
- JIG Inventory Screen
- subFrame Navigation Shell
- JIG Data Table
- Missing/Print Reports
- Resources & Settings
- Add JIG Form
- Add User & Credentials
- Missing-JIG Table
- Manage JIG Form
- Account Settings & Section
- JIG Model & Key Handlers
- Dashboard & App Root
- Login & Program Entry
- Graph Report Print
- Account Namespace
- Dashboard Cards
- dbQueries Helpers
- MainFrame
- DB Connection & JIG Inserts

## God Nodes (most connected - your core abstractions)
1. `ReportDataset` - 49 edges
2. `JIGInventory` - 33 edges
3. `JIG_DataDataTable` - 28 edges
4. `JIG_DataRow` - 28 edges
5. `JIG_MissingDataTable` - 25 edges
6. `JIG_SummaryDataTable` - 24 edges
7. `subFrame` - 24 edges
8. `AddJIG` - 22 edges
9. `ManageJIG` - 20 edges
10. `manageOtherUser` - 19 edges

## Surprising Connections (you probably didn't know these)
- `MissingJigReport` --references--> `ReportDataset`  [EXTRACTED]
  SystemFramework/JIG/Reporting/MissingJigReport.Designer.cs → SystemFramework/JIG/Reporting/ReportDataset.Designer.cs
- `printData` --references--> `ReportDataset`  [EXTRACTED]
  SystemFramework/JIG/Reporting/printData.Designer.cs → SystemFramework/JIG/Reporting/ReportDataset.Designer.cs
- `printGraph` --references--> `ReportDataset`  [EXTRACTED]
  SystemFramework/JIG/Reporting/printGraph.Designer.cs → SystemFramework/JIG/Reporting/ReportDataset.Designer.cs

## Import Cycles
- None detected.

## Communities (28 total, 17 thin omitted)

### Community 0 - "Namespaces & NuGet Deps"
Cohesion: 0.14
Nodes (6): QCInventoryF2.Database, QCInventoryF2.Model, QCInventoryF2.usercontrol, QCInventoryF2.JIG.Reporting, QCInventoryF2.JIG, QCInventoryF2.Reports

### Community 1 - "Missing-JIG Typed Rows"
Cohesion: 0.09
Nodes (11): JIG_MissingRow, control_no, jig_id, partcode, partname, section, JIG_SummaryRow, percentage (+3 more)

### Community 2 - "ReportDataset Core"
Cohesion: 0.09
Nodes (7): ReportDataset, JIG_Data, JIG_Missing, JIG_Summary, Relations, SchemaSerializationMode, Tables

### Community 3 - "JIG Data Typed Row"
Cohesion: 0.11
Nodes (9): JIG_DataRow, control_no, inventory_status, jig_id, partcode, partname, section, timestamp (+1 more)

### Community 4 - "Row Change Events"
Cohesion: 0.13
Nodes (9): JIG_DataRowChangeEvent, Action, Row, JIG_MissingRowChangeEvent, Action, Row, JIG_SummaryRowChangeEvent, Action (+1 more)

### Community 6 - "JIG Summary Table"
Cohesion: 0.14
Nodes (6): JIG_SummaryDataTable, Count, percentageColumn, sectionColumn, total_jigColumn, total_missingColumn

### Community 9 - "JIG Data Table"
Cohesion: 0.14
Nodes (10): JIG_DataDataTable, control_noColumn, Count, inventory_statusColumn, jig_idColumn, partcodeColumn, partnameColumn, sectionColumn (+2 more)

### Community 11 - "Resources & Settings"
Cohesion: 0.13
Nodes (9): QCInventoryF2.Properties, Resources, Culture, deleteKey, ResourceManager, trash_bin, Settings, Default (+1 more)

### Community 13 - "Add User & Credentials"
Cohesion: 0.17
Nodes (3): AddUser, changeCredentials, changePassword

### Community 14 - "Missing-JIG Table"
Cohesion: 0.16
Nodes (7): JIG_MissingDataTable, control_noColumn, Count, jig_idColumn, partcodeColumn, partnameColumn, sectionColumn

### Community 17 - "JIG Model & Key Handlers"
Cohesion: 0.17
Nodes (7): JIG, ControlNo, JigID, Partcode, Partname, Section, Status

## Knowledge Gaps
- **61 isolated node(s):** `JIG_Data`, `JIG_Missing`, `JIG_Summary`, `SchemaSerializationMode`, `Tables` (+56 more)
  These have ≤1 connection - possible missing edges. (Counts symbols only; 132 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **17 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `ReportDataset` connect `ReportDataset Core` to `Namespaces & NuGet Deps`, `Missing-JIG Typed Rows`, `JIG Data Typed Row`, `Row Change Events`, `JIG Summary Table`, `JIG Data Table`, `Missing/Print Reports`, `Missing-JIG Table`, `Graph Report Print`?**
  _High betweenness centrality (0.399) - this node is a cross-community bridge._
- **Are the 3 inferred relationships involving `ReportDataset` (e.g. with `.MissingJigReport_Load()` and `.printData_Load()`) actually correct?**
  _`ReportDataset` has 3 INFERRED edges - model-reasoned connections that need verification._
- **What connects `JIG_Data`, `JIG_Missing`, `JIG_Summary` to the rest of the system?**
  _61 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `Namespaces & NuGet Deps` be split into smaller, more focused modules?**
  _Cohesion score 0.1397459165154265 - nodes in this community are weakly interconnected._
- **Why does `JIGInventory` connect `JIG Inventory Screen` to `Namespaces & NuGet Deps`, `subFrame Navigation Shell`, `Missing/Print Reports`, `Add User & Credentials`, `Manage JIG Form`, `Account Settings & Section`, `JIG Model & Key Handlers`, `Graph Report Print`, `dbQueries Helpers`, `MainFrame`?**
  _High betweenness centrality (0.085) - this node is a cross-community bridge._
- **Should `Missing-JIG Typed Rows` be split into smaller, more focused modules?**
  _Cohesion score 0.09365079365079365 - nodes in this community are weakly interconnected._
- **Why does `MissingJigReport` connect `Missing/Print Reports` to `Namespaces & NuGet Deps`, `ReportDataset Core`, `Account Settings & Section`, `dbQueries Helpers`, `MainFrame`?**
  _High betweenness centrality (0.076) - this node is a cross-community bridge._