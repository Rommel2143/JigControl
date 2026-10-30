namespace JIGControl.Web.Common;

/// Values copied from the WinForms combo boxes so both apps write the same data.
public static class Lookups
{
    public static readonly string[] Sections =
    [
        "ASSY-ADF", "ASSY-OTHER", "ASSY-SCANNER", "MOLDING", "MOTOR ASSY", "PAINTING",
        "QC F2", "QC F1", "RETAINER", "RUBBER", "SHAFT", "TUBEPUMP"
    ];

    /// Section dropdowns on list pages; AllSections maps to a null (no) filter.
    public const string AllSections = "All sections";
    public static readonly string[] SectionFilter = [AllSections, .. Sections];
    public static string? SectionOrNull(string? s) => Sections.Contains(s) ? s : null;

    public static readonly string[] JigStatuses = ["Available", "In-Use", "Under Repair", "For Modification", "Other"];

    public static readonly string[] Months =
        System.Globalization.CultureInfo.InvariantCulture.DateTimeFormat.MonthNames.Take(12).ToArray();
}
