using System.Numerics;
using ImGuiNET;
using Veldrid;

namespace Akithos;

/// <summary>
///     Contains various utility methods for doing typical ImGui operations.
/// </summary>
public static class GUI
{
    #region DockSpace

    /// <summary>
    ///     Begins a new dockspace, which lets the user dock all windows defined within it.
    /// </summary>
    /// <param name="id">The id for the dockspace. Must be unique.</param>
    /// <param name="isFullScreen">Whether the dockspace should take up the entire window space.</param>
    public static void BeginDockSpace(string id, bool isFullScreen)
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
        ImGui.Begin(id, ref isOpen, windowFlags);
        ImGui.PopStyleVar();
            
        if (isFullScreen)
            ImGui.PopStyleVar(2);

        var io = ImGui.GetIO();
        if ((io.ConfigFlags & ImGuiConfigFlags.DockingEnable) != 0)
        {
            uint dockSpaceId = ImGui.GetID(id);
            ImGui.DockSpace(dockSpaceId, Vector2.Zero, dockSpaceFlags);
        }
    }

    public static void EndDockSpace()
    {
        ImGui.End();
    }
    
    #endregion

    /// <summary>
    ///     Renders the contents of a <see cref="Framebuffer"/> to an image.
    /// </summary>
    /// <param name="framebuffer">The framebuffer to render from.</param>
    /// <param name="size">The size of the resulting image.</param>
    /// <param name="graphicsDevice">GraphicsDevice required to generate texture data.</param>
    /// <param name="imGuiRenderer">ImGuiRenderer required to generate texture data.</param>
    public static void DrawFramebuffer(Framebuffer framebuffer, Vector2 size, GraphicsDevice graphicsDevice,
        ImGuiRenderer imGuiRenderer)
    {
        var texture = framebuffer.ColorTargets[0].Target;
        var textureId = imGuiRenderer.GetOrCreateImGuiBinding(graphicsDevice.ResourceFactory, texture);
        ImGui.Image(textureId, size, new Vector2(0f, 1f), new Vector2(1f, 0f));
    }
}