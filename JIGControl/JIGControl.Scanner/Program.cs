using JIGControl.Scanner.Forms;

namespace JIGControl.Scanner;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();

        var (config, error) = StationConfig.Load();
        if (config is null)
        {
            MessageBox.Show(error, "JIG Scanner setup", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        var api = new ScannerApi(config);
        // Sign out returns to the login screen; closing the login screen exits.
        while (true)
        {
            using var login = new LoginForm(api, config.Section);
            if (login.ShowDialog() != DialogResult.OK || login.Operator is null) return;

            using var scan = new ScanForm(api, config.Section, login.Operator);
            Application.Run(scan);
            api.SignOut();
            if (!scan.SignedOut) return;
        }
    }
}
