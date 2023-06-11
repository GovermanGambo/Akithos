namespace Akithos;

public class ApplicationOptions
{
    private readonly Application m_application;
    private readonly ImGuiController m_imGuiController;

    internal ApplicationOptions(Application application, ImGuiController imGuiController)
    {
        m_application = application;
        m_imGuiController = imGuiController;
    }

    public ApplicationOptions ConfigureWindow(Action<WindowOptions> configure)
    {
        var options = WindowOptions.Default;
        configure(options);

        m_application.MainWindow.Title = options.Title;
        m_application.MainWindow.Width = options.Width;
        m_application.MainWindow.Height = options.Height;

        return this;
    }

    public ApplicationOptions ConfigureImGui(Action<ImGuiOptions> configure)
    {
        var options = new ImGuiOptions(m_imGuiController);
        configure(options);

        return this;
    }
}