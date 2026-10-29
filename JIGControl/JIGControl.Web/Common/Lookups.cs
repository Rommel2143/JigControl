namespace JIGControl.Web.Common;

/// Values copied from the WinForms combo boxes so both apps write the same data.
public static class Lookups
{
    public static readonly string[] Sections =
    [
        "ASSY-ADF", "ASSY-OTHER", "ASSY-SCANNER", "MOLDING", "MOTOR ASSY", "PAINTING",
        "QC F2", "QC F1", "RETAINER", "RUBBER", "SHAFT", "TUBEPUMP"
    ];

    public static readonly string[] JigStatuses = ["Available", "In-Use", "Under Repair", "For Modification", "Other"];

    public static readonly string[] Months =
        System.Globalization.CultureInfo.InvariantCulture.DateTimeFormat.MonthNames.Take(12).ToArray();
}
