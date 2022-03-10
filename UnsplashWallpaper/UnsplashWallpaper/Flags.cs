using System.Runtime.InteropServices;

[Flags]
public enum DesktopSlideshowOptions
{
    None = 0,
    ShuffleImages = 0x01
}

[Flags]
public enum DesktopSlideshowState
{
    None = 0,
    Enabled = 0x01,
    Slideshow = 0x02,
    DisabledByRemoteSession = 0x04
}

public enum DesktopSlideshowDirection
{
    Forward = 0,
    Backward = 1
}

public enum DesktopWallpaperPosition
{
    Center = 0,
    Tile = 1,
    Stretch = 2,
    Fit = 3,
    Fill = 4,
    Span = 5,
}

[StructLayout(LayoutKind.Sequential)]
public struct Rect
{
    public int Left;
    public int Top;
    public int Right;
    public int Bottom;
}