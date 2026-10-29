namespace JIGControl.Web.Features.Jigs;

public sealed class JigItem
{
    public int JigId { get; set; }
    public string ControlNo { get; set; } = "";
    public string Partcode { get; set; } = "";
    public string Partname { get; set; } = "";
    public string Section { get; set; } = "";
    public string? Status { get; set; }
    public DateTime? Updatestamp { get; set; }

    /// jig_masterlist.imageUrl: full path of the JIG's photo on the share, or null.
    public string? ImageUrl { get; set; }

    /// Browser URL for the photo. The query string changes when the photo is replaced, so browsers reload it.
    public string? PhotoSrc => string.IsNullOrEmpty(ImageUrl)
        ? null
        : $"/jig-photos/{JigId}?v={Path.GetFileNameWithoutExtension(ImageUrl)}";
}
