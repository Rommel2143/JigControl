using System.Text.Json;

namespace JIGControl.Scanner;

/// Per-PC settings, set after installation from the Settings button (admin sign-in required).
/// Saved in %ProgramData%\JIGControl\Scanner\station.json so it is writable when the app is installed
/// under Program Files; a station.json next to the exe is still read (development, older installs).
/// The section is locked here: operators cannot change it, and JIGs of other sections are rejected.
public sealed class StationConfig
{
    /// Base URL of JIGControl.Web, e.g. http://server:5183
    public string ApiUrl { get; set; } = "";
    public string Section { get; set; } = "";

    public const string FileName = "station.json";

    public static string SavePath { get; } = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "JIGControl", "Scanner", FileName);

    private static string LocalPath => Path.Combine(AppContext.BaseDirectory, FileName);

    private static readonly JsonSerializerOptions Json = new() { ReadCommentHandling = JsonCommentHandling.Skip, WriteIndented = true };

    /// Returns the saved settings (possibly empty or invalid) and what is wrong with them, if anything.
    public static (StationConfig Config, string? Error) Load()
    {
        var path = File.Exists(SavePath) ? SavePath : File.Exists(LocalPath) ? LocalPath : null;
        if (path is null)
            return (new StationConfig(), "This station is not set up yet. Enter the web app address and this station's section.");

        StationConfig? cfg;
        try
        {
            cfg = JsonSerializer.Deserialize<StationConfig>(File.ReadAllText(path), Json);
        }
        catch (Exception ex) when (ex is JsonException or IOException or UnauthorizedAccessException)
        {
            return (new StationConfig(), $"{path} could not be read: {ex.Message}");
        }
        cfg ??= new StationConfig();
        return (cfg, cfg.Validate());
    }

    /// Null when the settings are usable, otherwise a message for the settings screen.
    public string? Validate()
    {
        if (!Uri.TryCreate(ApiUrl, UriKind.Absolute, out var url) || url.Scheme is not ("http" or "https"))
            return "Enter the web app address, e.g. http://server:5183";
        if (!Lookups.Sections.Contains(Section))
            return "Choose this station's section.";
        return null;
    }

    public void Save()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(SavePath)!);
        File.WriteAllText(SavePath, JsonSerializer.Serialize(this, Json));
    }
}

/// Values copied from the WinForms combo boxes (same list as JIGControl.Web Common/Lookups.cs).
public static class Lookups
{
    public static readonly string[] Sections =
    [
        "ASSY-ADF", "ASSY-OTHER", "ASSY-SCANNER", "MOLDING", "MOTOR ASSY", "PAINTING",
        "QC F2", "QC F1", "RETAINER", "RUBBER", "SHAFT", "TUBEPUMP"
    ];

    public static readonly string[] JigStatuses = ["Available", "In-Use", "Under Repair", "For Modification", "Other"];
}
