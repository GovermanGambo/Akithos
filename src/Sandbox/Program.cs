// See https://aka.ms/new-console-template for more information

using Akithos;
using Sandbox;

public static class Program
{
    public static void Main(string[] args) => BuildAkithosApp().Start();

    private static AppBuilder BuildAkithosApp()
        => AppBuilder.Configure<App>();
}