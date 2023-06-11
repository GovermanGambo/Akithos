using System.Numerics;
using ImGuiNET;

namespace Akithos;

public class GUI
{
    public static void BeginDockSpace(bool isFullScreen)
    {
        var dockSpaceFlags = ImGuiDockNodeFlags.None;

        var windowFlags = ImGuiWindowFlags.MenuBar | ImGuiWindowFlags.NoDocking;
        if (isFullScreen)
        {
            var viewport = ImGui.GetMainViewport();
            ImGui.SetNextWindowPos(viewport.Pos);
            ImGui.SetNextWindowSize(viewport.Size);
            ImGui.SetNextWindowViewport(viewport.ID);
            ImGui.PushStyleVar(ImGuiStyleVar.WindowRounding, 0.0f);
            ImGui.PushStyleVar(ImGuiStyleVar.WindowBorderSize, 0.0f);
            windowFlags |= ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoResize |
                           ImGuiWindowFlags.NoMove;
            windowFlags |= ImGuiWindowFlags.NoBringToFrontOnFocus | ImGuiWindowFlags.NoNavFocus;
        }

        ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, Vector2.Zero);
        bool isOpen = true;
        ImGui.Begin("DockSpace", ref isOpen, windowFlags);
        ImGui.PopStyleVar();
            
        if (isFullScreen)
            ImGui.PopStyleVar(2);

        var io = ImGui.GetIO();
        if ((io.ConfigFlags & ImGuiConfigFlags.DockingEnable) != 0)
        {
            uint dockSpaceId = ImGui.GetID("Waddle_DockSpace");
            ImGui.DockSpace(dockSpaceId, Vector2.Zero, dockSpaceFlags);
        }
    }

    public static void EndDockSpace()
    {
        ImGui.End();
    }
}