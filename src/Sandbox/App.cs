using Akithos;
using ImGuiNET;

namespace Sandbox;

public class App : Application
{
    protected override void Configure(ApplicationOptions options)
    {
        options.ConfigureWindow(windowOptions =>
            {
                windowOptions.Title = "Sandbox App";
                windowOptions.Width = 1920;
                windowOptions.Height = 1080;
            })
            .ConfigureImGui(imGuiOptions =>
            {
                imGuiOptions.UseIniSettingsFile("imgui.ini");
            });
    }

    protected override void OnDrawGUI()
    {
        GUI.BeginDockSpace("DockSpace", true);

        ImGui.ShowDemoWindow();
        
        GUI.EndDockSpace();
    }
}