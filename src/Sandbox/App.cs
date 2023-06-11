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
        });
    }

    protected override void OnDrawGUI()
    {
        GUI.BeginDockSpace(true);

        ImGui.Begin("Test Window");
        
        ImGui.Text("Hello!");
        
        ImGui.End();
        
        GUI.EndDockSpace();
    }
}