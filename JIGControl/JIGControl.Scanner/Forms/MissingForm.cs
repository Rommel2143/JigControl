namespace JIGControl.Scanner.Forms;

/// JIGs of the station's section not yet scanned this month, so the operator knows what to look for.
public sealed class MissingForm : Form
{
    public MissingForm(string section, DateTime month, List<MissingRow> rows)
    {
        Text = $"Missing JIGs · {section} · {month:MMMM yyyy}";
        Font = Theme.Base;
        StartPosition = FormStartPosition.CenterParent;
        Size = new Size(900, 600);
        MinimizeBox = false;

        var search = new TextBox { Dock = DockStyle.Top, PlaceholderText = "Filter by control no, part code or part name", Font = new Font("Segoe UI", 11f) };
        var list = new ListView { Dock = DockStyle.Fill, View = View.Details, FullRowSelect = true };
        list.Columns.Add("Control No", 150);
        list.Columns.Add("Part Code", 150);
        list.Columns.Add("Part Name", 280);
        list.Columns.Add("JIG Status", 120);
        list.Columns.Add("Last scanned", 120);
        var count = new Label { Dock = DockStyle.Bottom, Height = 28, TextAlign = ContentAlignment.MiddleLeft, ForeColor = Theme.Muted };

        void Fill()
        {
            var f = search.Text.Trim();
            var shown = rows.Where(r => f.Length == 0
                || r.ControlNo.Contains(f, StringComparison.OrdinalIgnoreCase)
                || r.Partcode.Contains(f, StringComparison.OrdinalIgnoreCase)
                || r.Partname.Contains(f, StringComparison.OrdinalIgnoreCase)).ToList();
            list.BeginUpdate();
            list.Items.Clear();
            foreach (var r in shown)
            {
                var item = new ListViewItem([r.ControlNo, r.Partcode, r.Partname, r.Status ?? "-", r.LastScanned?.ToString("yyyy-MM-dd") ?? "Never"]);
                if (r.LastScanned is null) item.ForeColor = Theme.ErrorText;
                list.Items.Add(item);
            }
            list.EndUpdate();
            count.Text = $"  {shown.Count} of {rows.Count} missing";
        }

        search.TextChanged += (_, _) => Fill();
        var body = new Panel { Dock = DockStyle.Fill, Padding = new Padding(12) };
        body.Controls.Add(list);
        body.Controls.Add(new Panel { Dock = DockStyle.Top, Height = 8 });
        body.Controls.Add(search);
        body.Controls.Add(count);
        Controls.Add(body);
        Fill();
    }
}
