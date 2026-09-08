using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using TeklaModelInspector.Services;
using TeklaModelInspector.ViewModels;

namespace TeklaModelInspector;

public partial class App : Application
{
    private readonly ServiceProvider _serviceProvider;

    public App()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        var services = new ServiceCollection();

        var useMock = bool.TryParse(
            configuration["Tekla:UseMock"],
            out var configuredValue)
            && configuredValue;

        if (useMock)
        {
            services.AddSingleton<ITeklaService, MockTeklaService>();
        }
        else
        {
            services.AddSingleton<ITeklaService, TeklaService>();
        }

        services.AddSingleton<IConfiguration>(configuration);
        services.AddTransient<MainViewModel>();
        services.AddTransient<MainWindow>();

        _serviceProvider = services.BuildServiceProvider();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _serviceProvider.Dispose();
        base.OnExit(e);
    }
}