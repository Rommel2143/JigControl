using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace JIGControl.Scanner;

public sealed class Operator
{
    public int UserId { get; set; }
    public string Username { get; set; } = "";
    public string DisplayName { get; set; } = "";
}

public sealed record ScanResult(bool Ok, string Message);

public sealed record Progress(int Expected, int Scanned, int Missing, double Percentage);

public sealed class ScanRow
{
    public string ControlNo { get; set; } = "";
    public string Partname { get; set; } = "";
    public string InventoryStatus { get; set; } = "";
    public string User { get; set; } = "";
    public DateTime Timestamp { get; set; }
}

public sealed class MissingRow
{
    public string ControlNo { get; set; } = "";
    public string Partcode { get; set; } = "";
    public string Partname { get; set; } = "";
    public string? Status { get; set; }
    public DateTime? LastScanned { get; set; }
}

/// The station's token expired (12 h) or was rejected; the operator must sign in again.
public sealed class SessionExpiredException() : Exception("Your session has expired. Please sign in again.");

/// Talks to the JIGControl.Web API (/api). All rules (QR format, one scan per month, section lock,
/// expected = JIGs that existed by the 1st) run on the server, so the station needs no database access.
public sealed class ScannerApi
{
    private readonly HttpClient _http;
    private readonly string _section;

    public ScannerApi(StationConfig config)
    {
        _section = config.Section;
        _http = new HttpClient { BaseAddress = new Uri(config.ApiUrl.TrimEnd('/') + "/"), Timeout = TimeSpan.FromSeconds(15) };
    }

    /// Returns null for a wrong username/password. The token is locked to the station's section.
    public async Task<Operator?> LoginAsync(string username, string password)
    {
        _http.DefaultRequestHeaders.Authorization = null;
        using var res = await _http.PostAsJsonAsync("api/auth/login", new { username, password, section = _section });
        if (res.StatusCode == HttpStatusCode.Unauthorized) return null;
        await EnsureOkAsync(res);
        var token = await res.Content.ReadFromJsonAsync<TokenResponse>() ?? throw new InvalidOperationException("Empty login response.");
        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
        return await GetAsync<Operator>("api/me");
    }

    public void SignOut() => _http.DefaultRequestHeaders.Authorization = null;

    public async Task<ScanResult> ScanAsync(string qrCode, string status)
    {
        using var res = await _http.PostAsJsonAsync("api/scans", new { qrCode, status });
        await EnsureOkAsync(res);
        return (await res.Content.ReadFromJsonAsync<ScanResult>())!;
    }

    public Task<Progress> ProgressAsync(DateTime month) =>
        GetAsync<Progress>($"api/sections/{Section}/progress?year={month.Year}&month={month.Month}");

    public Task<List<ScanRow>> RecentScansAsync(DateTime month, int limit = 30) =>
        GetAsync<List<ScanRow>>($"api/sections/{Section}/scans?year={month.Year}&month={month.Month}&limit={limit}");

    public Task<List<MissingRow>> MissingAsync(DateTime month) =>
        GetAsync<List<MissingRow>>($"api/sections/{Section}/missing?year={month.Year}&month={month.Month}");

    private string Section => Uri.EscapeDataString(_section);

    private async Task<T> GetAsync<T>(string url)
    {
        using var res = await _http.GetAsync(url);
        await EnsureOkAsync(res);
        return (await res.Content.ReadFromJsonAsync<T>())!;
    }

    private static async Task EnsureOkAsync(HttpResponseMessage res)
    {
        if (res.IsSuccessStatusCode) return;
        if (res.StatusCode == HttpStatusCode.Unauthorized) throw new SessionExpiredException();
        string? message = null;
        try { message = (await res.Content.ReadFromJsonAsync<ApiError>())?.Message; } catch { /* not JSON */ }
        throw new HttpRequestException(message ?? $"Server returned {(int)res.StatusCode} {res.ReasonPhrase}.");
    }

    private sealed record TokenResponse(string AccessToken);
    private sealed record ApiError(string Message);
}
