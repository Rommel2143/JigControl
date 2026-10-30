namespace JIGControl.Scanner.Forms;

/// Station setup after installation: web app address and the section this station scans.
/// Saving needs an administrator's sign-in, checked against the entered server, so operators cannot
/// move a station to another section and a wrong address is caught before it is saved.
public sealed class SettingsForm : Form
{
    private readonly TextBox _apiUrl = new() { Dock = DockStyle.Top, Font = new Font("Segoe UI", 12f) };
    private readonly ComboBox _section = new() { Dock = DockStyle.Top, Font = new Font("Segoe UI", 12f), DropDownStyle = ComboBoxStyle.DropDownList };
    private readonly TextBox _username = new() { Dock = DockStyle.Top, Font = new Font("Segoe UI", 12f) };
    private readonly TextBox _password = new() { Dock = DockStyle.Top, Font = new Font("Segoe UI", 12f), UseSystemPasswordChar = true };
    private readonly Label _error = new() { Dock = DockStyle.Top, ForeColor = Theme.ErrorText, AutoSize = false, Height = 44 };
    private readonly Button _save = Theme.PrimaryButton("Save settings");

    public StationConfig? Config { get; private set; }

    public SettingsForm(StationConfig current, string? message = null)
    {
        Text = "JIG Scanner settings";
        Font = Theme.Base;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        StartPosition = FormStartPosition.CenterScreen;
        ClientSize = new Size(420, 560);
        BackColor = Color.White;

        var header = new Panel { Dock = DockStyle.Top, Height = 70, BackColor = Theme.Brand };
        header.Controls.Add(new Label
        {
            Text = "Station settings",
            ForeColor = Color.White,
            Font = Theme.Title,
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter,
        });

        _apiUrl.Text = current.ApiUrl;
        _section.Items.AddRange(Lookups.Sections);
        _section.SelectedItem = Lookups.Sections.Contains(current.Section) ? current.Section : null;
        _error.Text = message ?? "";
        _error.ForeColor = message is null ? Theme.ErrorText : Theme.Muted;

        _save.Dock = DockStyle.Top;
        _save.Click += async (_, _) => await SaveAsync();
        AcceptButton = _save;

        // Docked Top controls stack in reverse order of adding.
        var body = new Panel { Dock = DockStyle.Fill, Padding = new Padding(32, 16, 32, 16) };
        body.Controls.AddRange(
        [
            _save,
            _error,
            _password,
            Caption("Administrator password"),
            Gap(),
            _username,
            Caption("Administrator username"),
            Gap(16),
            _section,
            Caption("Section of this station"),
            Gap(),
            _apiUrl,
            Caption("Web app address (e.g. http://server:5183)"),
        ]);

        Controls.Add(body);
        Controls.Add(header);
        Shown += (_, _) => (_apiUrl.Text.Length == 0 ? _apiUrl : _username).Focus();
    }

    private static Label Caption(string text) => new() { Text = text, Dock = DockStyle.Top, Height = 24, ForeColor = Theme.Muted };

    private static Panel Gap(int height = 8) => new() { Dock = DockStyle.Top, Height = height };

    private async Task SaveAsync()
    {
        _error.ForeColor = Theme.ErrorText;
        var config = new StationConfig { ApiUrl = _apiUrl.Text.Trim().TrimEnd('/'), Section = (string?)_section.SelectedItem ?? "" };
        _error.Text = config.Validate() ?? (_username.Text.Trim().Length == 0 ? "Enter an administrator's username and password." : "");
        if (_error.Text.Length > 0) return;

        _save.Enabled = false;
        UseWaitCursor = true;
        try
        {
            // Signing in against the entered address checks the address, the section and the admin role at once.
            var op = await new ScannerApi(config).LoginAsync(_username.Text.Trim(), _password.Text);
            if (op is null || !op.IsAdmin)
            {
                _error.Text = op is null ? "Wrong username or password." : "Only administrators can change station settings.";
                _password.SelectAll();
                _password.Focus();
                return;
            }
            config.Save();
            Config = config;
            DialogResult = DialogResult.OK;
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            _error.Text = $"Cannot save {StationConfig.SavePath}: {ex.Message}";
        }
        catch (Exception ex)
        {
            _error.Text = "Cannot reach the server: " + ex.Message;
        }
        finally
        {
            _save.Enabled = true;
            UseWaitCursor = false;
        }
    }
}
