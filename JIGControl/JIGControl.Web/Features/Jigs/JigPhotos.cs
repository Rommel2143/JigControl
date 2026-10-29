using Dapper;
using JIGControl.Web.Common;
using Microsoft.AspNetCore.StaticFiles;

namespace JIGControl.Web.Features.Jigs;

/// One photo per JIG. The file goes to the share and its full path is saved in jig_masterlist.imageUrl.
/// Uploading again replaces the old photo and deletes the old file.
public sealed class UploadJigPhotoHandler(IDbConnectionFactory db, PhotoStorage storage)
{
    /// Returns the new imageUrl, or an error message.
    public async Task<(string? ImageUrl, string? Error)> Handle(int jigId, Stream content, string originalName, string contentType, long size, CancellationToken ct = default)
    {
        var ext = Path.GetExtension(originalName).ToLowerInvariant();
        if (!PhotoStorage.AllowedExtensions.Contains(ext) || !contentType.StartsWith("image/"))
            return (null, "Only JPG, PNG, WEBP or GIF images are allowed.");
        if (size > PhotoStorage.MaxBytes)
            return (null, "The photo is larger than 10 MB.");

        string imageUrl;
        try
        {
            imageUrl = storage.NewPhotoPath(jigId, $"{Guid.NewGuid():N}{ext}");
            await using var fs = File.Create(imageUrl);
            await content.CopyToAsync(fs, ct);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            return (null, $"Cannot save to the photo share ({ex.Message}).");
        }

        try
        {
            using var con = await db.OpenAsync(ct);
            var old = await con.QuerySingleOrDefaultAsync<string?>("SELECT imageUrl FROM jig_masterlist WHERE jig_id = @jigId", new { jigId });
            await con.ExecuteAsync("UPDATE jig_masterlist SET imageUrl = @imageUrl WHERE jig_id = @jigId", new { jigId, imageUrl });
            if (old is not null) storage.Delete(old);
        }
        catch
        {
            storage.Delete(imageUrl);
            throw;
        }
        return (imageUrl, null);
    }
}

public sealed class RemoveJigPhotoHandler(IDbConnectionFactory db, PhotoStorage storage)
{
    public async Task Handle(int jigId, CancellationToken ct = default)
    {
        using var con = await db.OpenAsync(ct);
        var old = await con.QuerySingleOrDefaultAsync<string?>("SELECT imageUrl FROM jig_masterlist WHERE jig_id = @jigId", new { jigId });
        await con.ExecuteAsync("UPDATE jig_masterlist SET imageUrl = NULL WHERE jig_id = @jigId", new { jigId });
        if (old is not null) storage.Delete(old);
    }
}

public static class JigPhotoEndpoints
{
    private static readonly FileExtensionContentTypeProvider Types = new();

    /// Browsers can't open \\server paths, so the app streams the file named in imageUrl.
    public static void MapJigPhotoEndpoints(this WebApplication app)
    {
        app.MapGet("/jig-photos/{jigId:int}", async (int jigId, IDbConnectionFactory db, PhotoStorage storage) =>
        {
            using var con = await db.OpenAsync();
            var imageUrl = await con.QuerySingleOrDefaultAsync<string?>("SELECT imageUrl FROM jig_masterlist WHERE jig_id = @jigId", new { jigId });
            if (string.IsNullOrEmpty(imageUrl) || !storage.IsInside(imageUrl) || !File.Exists(imageUrl))
                return Results.NotFound();
            var type = Types.TryGetContentType(imageUrl, out var t) ? t : "application/octet-stream";
            return Results.File(imageUrl, type, enableRangeProcessing: true);
        }).RequireAuthorization();
    }
}
