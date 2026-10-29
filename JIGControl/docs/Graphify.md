# Graphify knowledge graph
Back: [[INDEX]]

Outputs in `graphify-out/`: `graph.html` (interactive), `graph.json`, `GRAPH_REPORT.md`. 452 nodes · 1126 edges · 28 communities (AST-only, no LLM tokens).

```bash
graphify query "how does a JIG scan get saved?"
graphify path "JIGInventory" "conString"
graphify explain "ReportDataset"
graphify update .      # after code changes
```
