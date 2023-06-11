using ImGuiNET;
using Veldrid;
using Veldrid.Sdl2;
using Veldrid.StartupUtilities;

namespace Akithos;

public abstract class Application
{
    private readonly ImGuiRenderer m_imGuiRenderer;
    private readonly GraphicsDevice m_graphicsDevice;
    private readonly ApplicationOptions m_applicationOptions;

    public Application()
    {
        m_applicationOptions = new ApplicationOptions(this);
        
        var windowCreateInfo = new WindowCreateInfo
        {
            WindowWidth = 1920,
            WindowHeight = 1080,
            WindowTitle = "Akithos App"
        };
        MainWindow = VeldridStartup.CreateWindow(windowCreateInfo);
        
        BindWindowEvents();
        
        var options = new GraphicsDeviceOptions()
        {
            PreferDepthRangeZeroToOne = true,
            PreferStandardClipSpaceYDirection = true,
            ResourceBindingModel = ResourceBindingModel.Improved,
            SyncToVerticalBlank = true
        };
        m_graphicsDevice = VeldridStartup.CreateGraphicsDevice(MainWindow, options);
        
        m_imGuiRenderer = new ImGuiRenderer(m_graphicsDevice, m_graphicsDevice.SwapchainFramebuffer.OutputDescription, 1920, 1080);
        
        InitializeImGui();
    }

    public Sdl2Window MainWindow { get; }
    
    internal void Run()
    {
        Configure(m_applicationOptions);
        
        var commandList = m_graphicsDevice.ResourceFactory.CreateCommandList();

        while (MainWindow.Exists)
        {
            var input = MainWindow.PumpEvents();

            if (!MainWindow.Exists)
            {
                break;
            }
            
            m_imGuiRenderer.Update(1f / 60f, input);

            OnDrawGUI();
            
            var io = ImGui.GetIO();
            // Update and Render additional Platform Windows
            if ((io.ConfigFlags & ImGuiConfigFlags.ViewportsEnable) != 0)
            {
                ImGui.UpdatePlatformWindows();
                ImGui.RenderPlatformWindowsDefault();
            }

            commandList.Begin();
            commandList.SetFramebuffer(m_graphicsDevice.SwapchainFramebuffer);
            commandList.ClearColorTarget(0, RgbaFloat.Black);
            m_imGuiRenderer.Render(m_graphicsDevice, commandList);
            commandList.End();
            m_graphicsDevice.SubmitCommands(commandList);
            m_graphicsDevice.SwapBuffers(m_graphicsDevice.MainSwapchain);
            
            
        }
    }

    protected abstract void Configure(ApplicationOptions options);

    protected abstract void OnDrawGUI();

    private void InitializeImGui()
    {
        var io = ImGui.GetIO();
        io.ConfigFlags |= ImGuiConfigFlags.ViewportsEnable;
        io.ConfigFlags |= ImGuiConfigFlags.DockingEnable;

        //io.Fonts.AddFontFromFileTTF("Resources/Fonts/Rubik/static/Rubik-Regular.ttf", 17);
        //io.Fonts.AddFontFromFileTTF("Resources/Fonts/Rubik/static/Rubik-ExtraBold.ttf", 17);

        m_imGuiRenderer.RecreateFontDeviceTexture();
    }

    private void BindWindowEvents()
    {
        MainWindow.Resized += MainWindow_OnResized;
    }

    private void MainWindow_OnResized()
    {
        m_imGuiRenderer.WindowResized(MainWindow.Width, MainWindow.Height);
        m_graphicsDevice.ResizeMainWindow((uint)MainWindow.Width, (uint)MainWindow.Height);
    }
}