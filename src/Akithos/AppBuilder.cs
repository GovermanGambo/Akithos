namespace Akithos;

public class AppBuilder
{
    private readonly Func<Application> m_appFactory;
    
    private AppBuilder(Func<Application> appFactory)
    {
        m_appFactory = appFactory;
    }

    public static AppBuilder Configure<TApp>() where TApp : Application, new()
    {
        return new AppBuilder(() => new TApp());
    }

    public void Start()
    {
        var app = m_appFactory();
        
        app.Run();
    }
}