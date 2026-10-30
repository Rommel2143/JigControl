using System.Security.Claims;
using JIGControl.Web.Common;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace JIGControl.Web.Features.Auth;

public static class AuthEndpoints
{
    /// Web sign-ins carry no section (pages pick one with a dropdown); only scanning-station
    /// API tokens are locked to a section.
    public static ClaimsPrincipal BuildPrincipal(LoginUser u, string? section = null, string authenticationType = CookieAuthenticationDefaults.AuthenticationScheme)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, u.UserId.ToString()),
            new(ClaimTypes.Name, u.Username),
            new(ClaimTypes.GivenName, $"{u.Firstname} {u.Lastname}".Trim()),
        };
        if (!string.IsNullOrEmpty(section)) claims.Add(new(AppClaims.Section, section));
        if (u.Admin) claims.Add(new(ClaimTypes.Role, AppClaims.AdminRole));
        return new ClaimsPrincipal(new ClaimsIdentity(claims, authenticationType));
    }

    public static void MapAuthEndpoints(this WebApplication app)
    {
        // Reads the form by hand, so UseAntiforgery alone would not reject a forged post.
        app.MapPost("/logout", async (HttpContext ctx, IAntiforgery antiforgery) =>
        {
            if (!await antiforgery.IsRequestValidAsync(ctx)) return Results.BadRequest();
            await ctx.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Results.Redirect("/login");
        }).RequireAuthorization();
    }
}
