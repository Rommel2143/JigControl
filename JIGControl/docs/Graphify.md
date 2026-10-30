# Graphify knowledge graph
Back: [[INDEX]]

Outputs in `graphify-out/`: `graph.html` (interactive), `graph.json`, `GRAPH_REPORT.md`. 990 nodes · 1895 edges · 69 communities (refreshed 2026-10-30 from commit `4c70094`). Code is AST-extracted (free); `docs/` is semantically extracted, so the graph links vault notes to the code they describe and WinForms parts to their web counterparts (`semantically_similar_to`).

Hyperedges: *Monthly QR scan pipeline (both apps)*, *Web JIG photo storage*, *Shared per-month inventory logic*.

`.graphifyignore` excludes the UI icon PNGs in `SystemFramework/Resources/`.

```bash
graphify query "how does a JIG scan get saved?"
graphify path "JIGInventory" "conString"
graphify explain "ReportDataset"
graphify update .      # after code changes (code-only = no LLM cost)
graphify explain "JIGControl.Web/Features/Scan/ScanJig.cs::ScanJigHandler"   # use path::symbol when a name is ambiguous
```
