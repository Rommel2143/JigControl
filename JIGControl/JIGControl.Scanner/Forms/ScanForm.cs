using System.Media;

namespace JIGControl.Scanner.Forms;

/// Main station screen. The barcode scanner types the QR code and sends Enter, which scans it.
public sealed class ScanForm : Form
{
    private readonly ScannerDb _db;
    private readonly string _section;
    private readonly Operator _operator;

    private readonly ComboBox _status = new() { DropDownStyle = ComboBoxStyle.DropDownList, Font = Theme.Big, Width = 260 };
    private readonly TextBox _qr = new() { Font = Theme.Big, Dock = DockStyle.Fill };
    private readonly Label _result = new()
    {
        Dock = DockStyle.Top, Height = 72, Font = Theme.Big, TextAlign = ContentAlignment.MiddleCenter,
        Text = "Ready — scan a JIG QR code", ForeColor = Theme.Muted, BackColor = Color.WhiteSmoke,
    };
    private readonly Label _month = new() { AutoSize = true, ForeColor = Theme.Muted, Text = "Loading this month's inventory…" };
    private readonly Label _scanned = new() { AutoSize = true, Font = Theme.Huge, ForeColor = Theme.Brand, Text = "– / – scanned" };
    private readonly Label _missing = new() { AutoSize = true, Font = Theme.Big, ForeColor = Theme.ErrorText, Text = "– missing" };
    private readonly ProgressBar _bar = new() { Dock = DockStyle.Bottom, Height = 14 };
    private readonly ListView _recent = new()
    {
        Dock = DockStyle.Fill, View = View.Details, FullRowSelect = true, GridLines = false,
        HeaderStyle = ColumnHeaderStyle.Nonclickable, Font = Theme.Base,
    };
    private bool _busy;

    public bool SignedOut { get; private set; }

    public ScanForm(ScannerDb db, string section, Operator op)
    {
        _db = db;
        _section = section;
        _operator = op;

        Text = $"JIG Scanner · {section}";
        Font = Theme.Base;
        StartPosition = FormStartPosition.CenterScreen;
        MinimumSize = new Size(900, 640);
        Size = new Size(1100, 760);
        BackColor = Color.White;
        KeyPreview = true;

        _status.Items.AddRange(Lookups.JigStatuses);
        _status.SelectedIndex = 0;
        _status.SelectedIndexChanged += (_, _) => _qr.Focus();
        _qr.KeyDown += async (_, e) =>
        {
            if (e.KeyCode != Keys.Enter) return;
            e.SuppressKeyPress = true;
            await ScanAsync();
        };

        _recent.Columns.Add("Time", 120);
        _recent.Columns.Add("Control No", 160);
        _recent.Columns.Add("Part Name", 320);
        _recent.Columns.Add("Status", 140);
        _recent.Columns.Add("Scanned by", 200);

        Controls.Add(BuildRecent());
        Controls.Add(_result);
        Controls.Add(BuildInput());
        Controls.Add(BuildProgress());
        Controls.Add(BuildHeader());

        Shown += async (_, _) => { _qr.Focus(); await RefreshAsync(); };
        Activated += (_, _) => _qr.Focus();
    }

    private Control BuildHeader()
    {
        var header = new Panel { Dock = DockStyle.Top, Height = 56, BackColor = Theme.Brand, Padding = new Padding(16, 0, 8, 0) };
        var title = new Label
        {
            Text = $"JIG Scanner  ·  {_section}", ForeColor = Color.White, Font = Theme.Title,
            Dock = DockStyle.Left, AutoSize = false, Width = 500, TextAlign = ContentAlignment.MiddleLeft,
        };
        var right = new FlowLayoutPanel { Dock = DockStyle.Right, AutoSize = true, FlowDirection = FlowDirection.LeftToRight, WrapContents = false, Padding = new Padding(0, 12, 0, 0) };
        right.Controls.Add(new Label { Text = _operator.DisplayName, ForeColor = Color.White, AutoSize = true, Padding = new Padding(0, 6, 8, 0) });
        var signOut = Theme.FlatButton("Sign out", Color.White);
        signOut.BackColor = Theme.BrandDark;
        signOut.Click += (_, _) => { SignedOut = true; Close(); };
        right.Controls.Add(signOut);
        header.Controls.Add(title);
        header.Controls.Add(right);
        return header;
    }

    private Control BuildProgress()
    {
        var panel = new Panel { Dock = DockStyle.Top, Height = 120, Padding = new Padding(16, 8, 16, 12) };
        var left = new FlowLayoutPanel { Dock = DockStyle.Left, FlowDirection = FlowDirection.TopDown, AutoSize = true, WrapContents = false };
        left.Controls.Add(_month);
        left.Controls.Add(_scanned);
        var right = new FlowLayoutPanel { Dock = DockStyle.Right, FlowDirection = FlowDirection.TopDown, AutoSize = true, WrapContents = false };
        right.Controls.Add(_missing);
        var view = Theme.FlatButton("View missing JIGs →", Theme.ErrorText);
        view.Click += async (_, _) => await ShowMissingAsync();
        right.Controls.Add(view);
        panel.Controls.Add(left);
        panel.Controls.Add(right);
        panel.Controls.Add(_bar);
        return panel;
    }

    private Control BuildInput()
    {
        var grid = new TableLayoutPanel { Dock = DockStyle.Top, Height = 92, ColumnCount = 3, RowCount = 2, Padding = new Padding(16, 4, 16, 8) };
        grid.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        grid.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        grid.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        grid.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        grid.Controls.Add(new Label { Text = "Status", AutoSize = true, ForeColor = Theme.Muted }, 0, 0);
        grid.Controls.Add(new Label { Text = "JIG QR code (scan or type, then Enter)", AutoSize = true, ForeColor = Theme.Muted }, 1, 0);
        grid.Controls.Add(_status, 0, 1);
        grid.Controls.Add(_qr, 1, 1);
        var scan = Theme.PrimaryButton("Scan");
        scan.Width = 120;
        scan.Height = 42;
        scan.Click += async (_, _) => await ScanAsync();
        grid.Controls.Add(scan, 2, 1);
        _status.Margin = new Padding(0, 0, 12, 0);
        scan.Margin = new Padding(12, 0, 0, 0);
        return grid;
    }

    private Control BuildRecent()
    {
        var panel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(16, 8, 16, 16) };
        panel.Controls.Add(_recent);
        panel.Controls.Add(new Label { Text = "Latest scans this month in this section", Dock = DockStyle.Top, Height = 26, Font = new Font("Segoe UI Semibold", 10.5f) });
        return panel;
    }

    private async Task ScanAsync()
    {
        var qr = _qr.Text.Trim();
        if (qr.Length == 0 || _busy) return;
        _busy = true;
        try
        {
            var result = await _db.ScanAsync(qr, (string)_status.SelectedItem!, _operator.UserId);
            ShowResult(result.Ok, result.Message);
            if (result.Ok) await RefreshAsync();
        }
        catch (Exception ex)
        {
            ShowResult(false, "Database error: " + ex.Message);
        }
        finally
        {
            _busy = false;
            _qr.Clear();
            _qr.Focus();
        }
    }

    private void ShowResult(bool ok, string message)
    {
        _result.Text = (ok ? "✔  " : "✖  ") + message;
        _result.BackColor = ok ? Theme.OkBack : Theme.ErrorBack;
        _result.ForeColor = ok ? Theme.OkText : Theme.ErrorText;
        (ok ? SystemSounds.Asterisk : SystemSounds.Hand).Play();
    }

    private async Task RefreshAsync()
    {
        var now = DateTime.Now;
        try
        {
            var p = await _db.ProgressAsync(now);
            _month.Text = $"{now:MMMM yyyy} inventory · {_section}";
            _scanned.Text = $"{p.Scanned} / {p.Expected} scanned  ({p.Percentage}%)";
            _missing.Text = $"{p.Missing} missing";
            _bar.Maximum = Math.Max(1, p.Expected);
            _bar.Value = Math.Min(_bar.Maximum, p.Scanned);

            var rows = await _db.RecentScansAsync(now);
            _recent.BeginUpdate();
            _recent.Items.Clear();
            foreach (var r in rows)
                _recent.Items.Add(new ListViewItem([r.Timestamp.ToString("MMM d HH:mm"), r.ControlNo, r.Partname, r.InventoryStatus, r.User]));
            _recent.EndUpdate();
        }
        catch (Exception ex)
        {
            ShowResult(false, "Cannot load progress: " + ex.Message);
        }
    }

    private async Task ShowMissingAsync()
    {
        try
        {
            var rows = await _db.MissingAsync(DateTime.Now);
            using var dlg = new MissingForm(_section, DateTime.Now, rows);
            dlg.ShowDialog(this);
        }
        catch (Exception ex)
        {
            ShowResult(false, "Cannot load missing JIGs: " + ex.Message);
        }
        _qr.Focus();
    }
}
