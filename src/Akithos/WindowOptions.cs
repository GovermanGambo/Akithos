namespace Akithos;

public sealed class WindowOptions
{
    public static WindowOptions Default => new("Akithos App", 1280, 720);
    
    public string Title;
    public int Width;
    public int Height;

    internal WindowOptions(string title, int width, int height)
    {
        Title = title;
        Width = width;
        Height = height;
    }
}