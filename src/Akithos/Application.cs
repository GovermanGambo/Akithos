using ImGuiNET;
using Veldrid;
using Veldrid.Sdl2;
using Veldrid.StartupUtilities;

namespace Akithos;

public abstract class Application
{
    private readonly ImGuiController m_imGuiController;
    private readonly GraphicsDevice m_graphicsDevice;
    private readonly ApplicationOptions m_applicationOptions;

    public Application()
    {
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

        m_imGuiController = new ImGuiController(m_graphicsDevice);
        
        m_applicationOptions = new ApplicationOptions(this, m_imGuiController);
    }

    public Sdl2Window MainWindow { get; }
    
    internal void Run()
    {
        Configure(m_applicationOptions);
        
        m_imGuiController.Initialize();
        
        var commandList = m_graphicsDevice.ResourceFactory.CreateCommandList();

        while (MainWindow.Exists)
        {
            var input = MainWindow.PumpEvents();

            if (!MainWindow.Exists)
            {
                break;
            }
            
            m_imGuiController.Update(1f / 60f, input);

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
            m_imGuiController.Render(m_graphicsDevice, commandList);
            commandList.End();
            m_graphicsDevice.SubmitCommands(commandList);
            m_graphicsDevice.SwapBuffers(m_graphicsDevice.MainSwapchain);
        }
        
        m_imGuiController.Dispose();
        commandList.Dispose();
        m_graphicsDevice.Dispose();
    }

    protected abstract void Configure(ApplicationOptions options);

    protected abstract void OnDrawGUI();

    private void BindWindowEvents()
    {
        MainWindow.Resized += MainWindow_OnResized;
    }

    private void MainWindow_OnResized()
    {
        m_imGuiController.ResizeViewport(MainWindow.Width, MainWindow.Height);
        m_graphicsDevice.ResizeMainWindow((uint)MainWindow.Width, (uint)MainWindow.Height);
    }
}