namespace Digitamentos.Models
{
    public enum CursorShape { Line, Block, Underline }

    public class UserSettings
    {
        public string FontFamily { get; set; } = "Consolas";
        public double FontSize { get; set; } = 22.0;
        public string ThemePreset { get; set; } = "System"; // System, Light, Dark, Dracula, Nord, GitHub Dark, Solarized
        public string CustomBackgroundHex { get; set; } = "";
        public string CustomForegroundHex { get; set; } = "";

        public CursorShape CursorShape { get; set; } = CursorShape.Line;
        public bool SmoothCursor { get; set; } = true;
    }
}
