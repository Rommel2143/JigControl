using System.Security.Claims;

namespace JIGControl.Web.Common;

public static class AppClaims
{
    public const string Section = "section";
    public const string AdminRole = "Admin";
}

public static class ClaimsPrincipalExtensions
{
    public static int UserId(this ClaimsPrincipal u) =>
        int.TryParse(u.FindFirstValue(ClaimTypes.NameIdentifier), out var id) ? id : 0;

    public static string Section(this ClaimsPrincipal u) => u.FindFirstValue(AppClaims.Section) ?? "";

    public static string DisplayName(this ClaimsPrincipal u) => u.FindFirstValue(ClaimTypes.GivenName) ?? u.Identity?.Name ?? "";

    public static bool IsAdmin(this ClaimsPrincipal u) => u.IsInRole(AppClaims.AdminRole);
}
