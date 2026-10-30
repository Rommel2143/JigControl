using System.Text.Json;

namespace JIGControl.Scanner;

/// Per-PC settings in station.json next to the exe (copy station.example.json).
/// The section is locked here: operators cannot change it, and JIGs of other sections are rejected.
public sealed class StationConfig
{
    public string ConnectionString { get; set; } = "";
    public string Section { get; set; } = "";

    public const string FileName = "station.json";

    /// Returns the config, or an error message telling the installer what to fix.
    public static (StationConfig? Config, string? Error) Load()
    {
        var path = Path.Combine(AppContext.BaseDirectory, FileName);
        if (!File.Exists(path))
            return (null, $"{FileName} was not found next to the app.\n\nCopy station.example.json to {FileName} in\n{AppContext.BaseDirectory}\nand set the connection string and this station's section.");

        StationConfig? cfg;
        try
        {
            cfg = JsonSerializer.Deserialize<StationConfig>(File.ReadAllText(path), new JsonSerializerOptions { ReadCommentHandling = JsonCommentHandling.Skip });
        }
        catch (JsonException ex)
        {
            return (null, $"{FileName} is not valid JSON: {ex.Message}");
        }

        if (cfg is null || string.IsNullOrWhiteSpace(cfg.ConnectionString))
            return (null, $"Set \"ConnectionString\" in {FileName}.");
        if (!Lookups.Sections.Contains(cfg.Section))
            return (null, $"\"Section\" in {FileName} must be one of:\n{string.Join(", ", Lookups.Sections)}");
        return (cfg, null);
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
