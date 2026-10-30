namespace JIGControl.Scanner.Forms;

/// Sign-in with the same jig_users accounts as the other apps. The section is fixed by the station settings;
/// the Settings button changes them (admin only) and closes this form with DialogResult.Retry.
public sealed class LoginForm : Form
{
    private readonly ScannerApi _api;
    private readonly TextBox _username = new() { Dock = DockStyle.Top, Font = new Font("Segoe UI", 12f) };
    private readonly TextBox _password = new() { Dock = DockStyle.Top, Font = new Font("Segoe UI", 12f), UseSystemPasswordChar = true };
    private readonly Label _error = new() { Dock = DockStyle.Top, ForeColor = Theme.ErrorText, AutoSize = false, Height = 40 };
    private readonly Button _signIn = Theme.PrimaryButton("Sign in");

    public Operator? Operator { get; private set; }

    /// Set when an administrator saved new settings; the caller restarts with them.
    public StationConfig? NewConfig { get; private set; }

    public LoginForm(ScannerApi api, StationConfig config)
    {
        _api = api;
        var section = config.Section;
        Text = "JIG Scanner";
        Font = Theme.Base;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        StartPosition = FormStartPosition.CenterScreen;
        ClientSize = new Size(380, 436);
        BackColor = Color.White;

        var header = new Panel { Dock = DockStyle.Top, Height = 90, BackColor = Theme.Brand };
        header.Controls.Add(new Label
        {
            Text = $"JIG Scanner\n{section}",
            ForeColor = Color.White,
            Font = Theme.Title,
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter,
        });

        _signIn.Dock = DockStyle.Top;
        _signIn.Click += async (_, _) => await SignInAsync();
        AcceptButton = _signIn;

        // Docked Top controls stack in reverse order of adding.
        var body = new Panel { Dock = DockStyle.Fill, Padding = new Padding(32, 20, 32, 20) };
        body.Controls.AddRange(
        [
            _signIn,
            _error,
            _password,
            Caption("Password"),
            new Panel { Dock = DockStyle.Top, Height = 8 },
            _username,
            Caption("Username"),
        ]);

        var settings = Theme.FlatButton("⚙ Settings", Theme.Muted);
        settings.Dock = DockStyle.Right;
        settings.Click += (_, _) =>
        {
            using var dlg = new SettingsForm(config);
            if (dlg.ShowDialog(this) != DialogResult.OK) return;
            NewConfig = dlg.Config;
            DialogResult = DialogResult.Retry;
        };
        var footer = new Panel { Dock = DockStyle.Bottom, Height = 36, Padding = new Padding(0, 0, 8, 4) };
        footer.Controls.Add(settings);

        Controls.Add(body);
        Controls.Add(footer);
        Controls.Add(header);
        Shown += (_, _) => _username.Focus();
    }

    private static Label Caption(string text) => new() { Text = text, Dock = DockStyle.Top, Height = 24, ForeColor = Theme.Muted };

    private async Task SignInAsync()
    {
        _error.Text = "";
        if (_username.Text.Trim().Length == 0)
        {
            _error.Text = "Enter your username.";
            _username.Focus();
            return;
        }

        _signIn.Enabled = false;
        UseWaitCursor = true;
        try
        {
            Operator = await _api.LoginAsync(_username.Text.Trim(), _password.Text);
            if (Operator is null)
            {
                _error.Text = "Wrong username or password.";
                _password.SelectAll();
                _password.Focus();
                return;
            }
            DialogResult = DialogResult.OK;
        }
        catch (Exception ex)
        {
            _error.Text = "Cannot reach the server: " + ex.Message;
        }
        finally
        {
            _signIn.Enabled = true;
            UseWaitCursor = false;
        }
    }
}
