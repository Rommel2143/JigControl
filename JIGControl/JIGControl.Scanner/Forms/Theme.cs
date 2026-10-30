namespace JIGControl.Scanner.Forms;

/// Colors and fonts shared by the forms (same green as the web app's Excel theme).
internal static class Theme
{
    public static readonly Color Brand = Color.FromArgb(0x21, 0x73, 0x46);
    public static readonly Color BrandDark = Color.FromArgb(0x18, 0x5C, 0x37);
    public static readonly Color OkBack = Color.FromArgb(0xDF, 0xF6, 0xDD);
    public static readonly Color OkText = Color.FromArgb(0x10, 0x7C, 0x10);
    public static readonly Color ErrorBack = Color.FromArgb(0xFD, 0xE7, 0xE9);
    public static readonly Color ErrorText = Color.FromArgb(0xC5, 0x0F, 0x1F);
    public static readonly Color Muted = Color.FromArgb(0x61, 0x61, 0x61);

    public static readonly Font Base = new("Segoe UI", 10f);
    public static readonly Font Title = new("Segoe UI Semibold", 14f);
    public static readonly Font Big = new("Segoe UI Semibold", 18f);
    public static readonly Font Huge = new("Segoe UI Semibold", 28f);

    public static Button PrimaryButton(string text) => new()
    {
        Text = text,
        BackColor = Brand,
        ForeColor = Color.White,
        FlatStyle = FlatStyle.Flat,
        FlatAppearance = { BorderSize = 0 },
        Font = new Font("Segoe UI Semibold", 10.5f),
        Height = 38,
        Cursor = Cursors.Hand,
    };

    public static Button FlatButton(string text, Color fore) => new()
    {
        Text = text,
        ForeColor = fore,
        FlatStyle = FlatStyle.Flat,
        FlatAppearance = { BorderSize = 0 },
        AutoSize = true,
        Cursor = Cursors.Hand,
    };
}
