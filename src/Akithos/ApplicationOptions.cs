namespace Akithos;

public class ApplicationOptions
{
    private readonly Application m_application;
    
    public ApplicationOptions(Application application)
    {
        m_application = application;
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
}