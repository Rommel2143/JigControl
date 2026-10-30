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

        var db = new ScannerDb(config);
        // Sign out returns to the login screen; closing the login screen exits.
        while (true)
        {
            using var login = new LoginForm(db, config.Section);
            if (login.ShowDialog() != DialogResult.OK || login.Operator is null) return;

            using var scan = new ScanForm(db, config.Section, login.Operator);
            Application.Run(scan);
            if (!scan.SignedOut) return;
        }
    }
}
