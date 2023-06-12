namespace Akithos;

public sealed class ImGuiOptions
{
    private readonly ImGuiController m_imGuiController;

    internal ImGuiOptions(ImGuiController imGuiController)
    {
        m_imGuiController = imGuiController;
    }

    public void UseIniSettingsFile(string filePath)
    {
        m_imGuiController.IniSettingsFilePath = filePath;
    }
}