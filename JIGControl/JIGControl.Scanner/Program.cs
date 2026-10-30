using JIGControl.Scanner.Forms;

namespace JIGControl.Scanner;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();

        // A new install (or broken settings) opens the settings screen first.
        var (config, error) = StationConfig.Load();
        if (error is not null)
        {
            using var setup = new SettingsForm(config, error);
            if (setup.ShowDialog() != DialogResult.OK || setup.Config is null) return;
            config = setup.Config;
        }

        // Sign out returns to the login screen; closing the login screen exits.
        while (true)
        {
            var api = new ScannerApi(config);
            using var login = new LoginForm(api, config);
            var result = login.ShowDialog();
            if (result == DialogResult.Retry && login.NewConfig is not null)
            {
                config = login.NewConfig;
                continue;
            }
            if (result != DialogResult.OK || login.Operator is null) return;

            using var scan = new ScanForm(api, config.Section, login.Operator);
            Application.Run(scan);
            api.SignOut();
            if (!scan.SignedOut) return;
        }
    }
}
