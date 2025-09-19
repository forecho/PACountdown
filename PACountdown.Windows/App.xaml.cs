using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PACountdown.Windows.Services;
using PACountdown.Windows.ViewModels;
using PACountdown.Windows.Views;
using System.Globalization;
using System.Threading;
using System.Windows;

namespace PACountdown.Windows;

public partial class App : Application
{
    private IHost? _host;

    protected override void OnStartup(StartupEventArgs e)
    {
        // Set default culture
        var culture = CultureInfo.GetCultureInfo("en-US");
        Thread.CurrentThread.CurrentCulture = culture;
        Thread.CurrentThread.CurrentUICulture = culture;

        _host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                // Services
                services.AddSingleton<ISettingsService, SettingsService>();
                services.AddSingleton<IAudioService, AudioService>();
                services.AddSingleton<IMarketHoursService, MarketHoursService>();

                // ViewModels
                services.AddTransient<MainWindowViewModel>();

                // Views
                services.AddTransient<MainWindow>();
            })
            .Build();

        var mainWindow = _host.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();

        base.OnStartup(e);
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _host?.Dispose();
        base.OnExit(e);
    }
}