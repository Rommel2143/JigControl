using System.Security.Claims;
using JIGControl.Web.Common;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace JIGControl.Web.Features.Auth;

public static class AuthEndpoints
{
    public static ClaimsPrincipal BuildPrincipal(LoginUser u, string section)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, u.UserId.ToString()),
            new(ClaimTypes.Name, u.Username),
            new(ClaimTypes.GivenName, $"{u.Firstname} {u.Lastname}".Trim()),
            new(AppClaims.Section, section),
        };
        if (u.Admin) claims.Add(new(ClaimTypes.Role, AppClaims.AdminRole));
        return new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
    }

    public static void MapAuthEndpoints(this WebApplication app)
    {
        // These read the form by hand, so UseAntiforgery alone would not reject a forged post.
        app.MapPost("/logout", async (HttpContext ctx, IAntiforgery antiforgery) =>
        {
            if (!await antiforgery.IsRequestValidAsync(ctx)) return Results.BadRequest();
            await ctx.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Results.Redirect("/login");
        }).RequireAuthorization();

        // Section lives in the auth cookie, so switching it re-issues the cookie.
        app.MapPost("/account/section", async (HttpContext ctx, IAntiforgery antiforgery) =>
        {
            if (!await antiforgery.IsRequestValidAsync(ctx)) return Results.BadRequest();
            var form = await ctx.Request.ReadFormAsync();
            var section = form["section"].ToString();
            if (!Lookups.Sections.Contains(section)) return Results.BadRequest();

            var id = (ClaimsIdentity)ctx.User.Identity!;
            var claims = id.Claims.Where(c => c.Type != AppClaims.Section).Append(new Claim(AppClaims.Section, section));
            await ctx.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(new ClaimsIdentity(claims, id.AuthenticationType)));
            return Results.Redirect("/account?saved=section");
        }).RequireAuthorization();
    }
}
