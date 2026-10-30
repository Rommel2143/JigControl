using System.Security.Claims;
using JIGControl.Web.Common;
using JIGControl.Web.Features.Auth;
using JIGControl.Web.Features.Inventory;
using JIGControl.Web.Features.Missing;
using JIGControl.Web.Features.Scan;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authorization;

namespace JIGControl.Web.Features.Api;

public sealed record ApiLoginRequest(string Username, string Password, string? Section);
public sealed record ApiScanRequest(string QrCode, string Status);
public sealed record ApiMe(int UserId, string Username, string DisplayName, string? Section, bool IsAdmin);
public sealed record ApiProgress(string Section, int Year, int Month, int Expected, int Scanned, int Missing, double Percentage);
public sealed record ApiError(string Message);

/// JSON API for desktop clients (JIGControl.Scanner). Same handlers and rules as the web pages.
/// Auth: POST /api/auth/login returns a bearer token (12 h); send it as "Authorization: Bearer <token>".
/// A token issued with a section is a scanning-station token: scans are limited to that section.
public static class ApiEndpoints
{
    public static void MapApiEndpoints(this WebApplication app)
    {
        var api = app.MapGroup("/api").DisableAntiforgery();
        var secured = api.MapGroup("").RequireAuthorization(new AuthorizeAttribute { AuthenticationSchemes = BearerTokenDefaults.AuthenticationScheme });

        api.MapGet("/lookups", () => Results.Ok(new { Lookups.Sections, Lookups.JigStatuses }));

        api.MapPost("/auth/login", async (ApiLoginRequest req, LoginHandler login) =>
        {
            if (req.Section is { Length: > 0 } s && !Lookups.Sections.Contains(s))
                return Results.BadRequest(new ApiError($"Unknown section '{s}'."));
            var user = await login.Handle(req.Username?.Trim() ?? "", req.Password ?? "");
            if (user is null) return Results.Json(new ApiError("Wrong username or password."), statusCode: StatusCodes.Status401Unauthorized);

            var principal = AuthEndpoints.BuildPrincipal(user, req.Section ?? "", BearerTokenDefaults.AuthenticationScheme);
            // Writes { tokenType, accessToken, expiresIn, refreshToken }.
            return Results.SignIn(principal, authenticationScheme: BearerTokenDefaults.AuthenticationScheme);
        });

        secured.MapGet("/me", (ClaimsPrincipal u) =>
            new ApiMe(u.UserId(), u.Identity?.Name ?? "", u.DisplayName(), StationSection(u), u.IsAdmin()));

        secured.MapPost("/scans", async (ApiScanRequest req, ClaimsPrincipal u, ScanJigHandler scan) =>
        {
            if (!Lookups.JigStatuses.Contains(req.Status))
                return Results.BadRequest(new ApiError($"Unknown status '{req.Status}'."));
            return Results.Ok(await scan.Handle(req.QrCode ?? "", req.Status, u.UserId(), StationSection(u)));
        });

        secured.MapGet("/sections/{section}/progress", async (string section, int? year, int? month, MissingBySectionHandler bySection) =>
        {
            var (y, m) = Period(year, month);
            var s = (await bySection.Handle(y, m)).FirstOrDefault(x => x.Section == section);
            return s is null
                ? Results.NotFound(new ApiError($"Unknown section '{section}'."))
                : Results.Ok(new ApiProgress(section, y, m, s.Total, s.Scanned, s.Missing, s.Percentage));
        });

        secured.MapGet("/sections/{section}/scans", async (string section, int? year, int? month, int? limit, ListScannedHandler scanned) =>
        {
            var (y, m) = Period(year, month);
            var rows = await scanned.Handle(section, y, m);
            return Results.Ok(rows.Take(Math.Clamp(limit ?? 50, 1, 1000)));
        });

        secured.MapGet("/sections/{section}/missing", async (string section, int? year, int? month, MissingJigsHandler missing) =>
        {
            var (y, m) = Period(year, month);
            return Results.Ok(await missing.Handle(section, y, m));
        });
    }

    /// The section a station token is locked to, or null for a normal user token.
    private static string? StationSection(ClaimsPrincipal u) => u.Section() is { Length: > 0 } s ? s : null;

    private static (int Year, int Month) Period(int? year, int? month)
    {
        var now = DateTime.Now;
        var m = month is >= 1 and <= 12 ? month.Value : now.Month;
        return (year is >= 2020 and <= 2100 ? year.Value : now.Year, m);
    }
}
