namespace JIGControl.Web.Common;

/// Photos live on the network share in PhotoStorage:Path, one subfolder per JIG:
/// \\ptif1-ds\...\JIGINVENTORY\{jig_id}\{guid}.jpg  (that full path is saved as jig_masterlist.imageUrl).
public sealed class PhotoStorage
{
    public static readonly string[] AllowedExtensions = [".jpg", ".jpeg", ".png", ".webp", ".gif"];
    public const long MaxBytes = 10 * 1024 * 1024;

    public string Root { get; }

    public PhotoStorage(IConfiguration config, IWebHostEnvironment env)
    {
        var path = config["PhotoStorage:Path"];
        if (string.IsNullOrWhiteSpace(path)) path = "App_Data/jig-photos";
        Root = Path.IsPathRooted(path) ? path : Path.Combine(env.ContentRootPath, path);
    }

    /// Full path for a new photo of a JIG; creates the JIG's folder if needed.
    public string NewPhotoPath(int jigId, string fileName)
    {
        var dir = Path.Combine(Root, jigId.ToString());
        Directory.CreateDirectory(dir);
        return Path.Combine(dir, Path.GetFileName(fileName));
    }

    /// Only files under Root are served or deleted, even if a DB row points elsewhere.
    public bool IsInside(string fullPath)
    {
        var root = Path.GetFullPath(Root).TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar;
        return Path.GetFullPath(fullPath).StartsWith(root, StringComparison.OrdinalIgnoreCase);
    }

    public void Delete(string imageUrl)
    {
        if (!string.IsNullOrEmpty(imageUrl) && IsInside(imageUrl) && File.Exists(imageUrl)) File.Delete(imageUrl);
    }
}
