using ImGuiNET;
using Veldrid;

namespace Akithos;

internal sealed class ImGuiController : IDisposable
{
    private readonly ImGuiRenderer m_imGuiRenderer;

    public ImGuiController(GraphicsDevice graphicsDevice)
    {
        m_imGuiRenderer = new ImGuiRenderer(graphicsDevice, graphicsDevice.SwapchainFramebuffer.OutputDescription,
            (int)graphicsDevice.SwapchainFramebuffer.Width, (int)graphicsDevice.SwapchainFramebuffer.Height);
    }
    
    public string? IniSettingsFilePath { get; set; }

    public void Initialize()
    {
        var io = ImGui.GetIO();
        io.ConfigFlags |= ImGuiConfigFlags.ViewportsEnable;
        io.ConfigFlags |= ImGuiConfigFlags.DockingEnable;

        //io.Fonts.AddFontFromFileTTF("Resources/Fonts/Rubik/static/Rubik-Regular.ttf", 17);
        //io.Fonts.AddFontFromFileTTF("Resources/Fonts/Rubik/static/Rubik-ExtraBold.ttf", 17);

        m_imGuiRenderer.RecreateFontDeviceTexture();
        
        if (IniSettingsFilePath is not null)
        {
            ImGui.LoadIniSettingsFromDisk(IniSettingsFilePath);
        }
    }

    public void Update(float deltaTime, InputSnapshot input)
    {
        m_imGuiRenderer.Update(deltaTime, input);
    }

    public void Render(GraphicsDevice graphicsDevice, CommandList commandList)
    {
        m_imGuiRenderer.Render(graphicsDevice, commandList);
    }

    public void ResizeViewport(int width, int height)
    {
        m_imGuiRenderer.WindowResized(width, height);
    }

    public void Dispose()
    {
        if (IniSettingsFilePath is not null)
        {
            ImGui.SaveIniSettingsToDisk(IniSettingsFilePath);
        }
        
        m_imGuiRenderer.Dispose();
    }
}