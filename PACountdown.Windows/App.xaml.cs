using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PACountdown.Windows.Services;
using PACountdown.Windows.ViewModels;
using PACountdown.Windows.Views;
using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows;

namespace PACountdown.Windows;

public partial class App : Application
{
    private IHost? _host;
    private string? _logFilePath;

    protected override void OnStartup(StartupEventArgs e)
    {
        // Use OS culture
        var culture = CultureInfo.CurrentUICulture;
        Thread.CurrentThread.CurrentCulture = culture;
        Thread.CurrentThread.CurrentUICulture = culture;

        // Init simple log file in user AppData
        var appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var logDir = Path.Combine(appData, "PACountdown");
        Directory.CreateDirectory(logDir);
        _logFilePath = Path.Combine(logDir, "app.log");

        AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        this.DispatcherUnhandledException += App_DispatcherUnhandledException;

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

        try
        {
            var mainWindow = _host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
        catch (Exception ex)
        {
            try { File.AppendAllText(_logFilePath!, $"[Startup] {DateTime.Now:o} {ex}\n"); } catch { }
            MessageBox.Show("Startup failed. See log for details.", "PACountdown", MessageBoxButton.OK, MessageBoxImage.Error);
            Shutdown(-1);
            return;
        }

        base.OnStartup(e);
    }

    private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
    {
        try { File.AppendAllText(_logFilePath!, $"[UI] {DateTime.Now:o} {e.Exception}\n"); } catch { }
        MessageBox.Show("An unexpected error occurred. The application will attempt to continue.", "PACountdown", MessageBoxButton.OK, MessageBoxImage.Error);
        e.Handled = true;
    }

    private void CurrentDomain_UnhandledException(object? sender, UnhandledExceptionEventArgs e)
    {
        try { File.AppendAllText(_logFilePath!, $"[Domain] {DateTime.Now:o} {e.ExceptionObject}\n"); } catch { }
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _host?.Dispose();
        base.OnExit(e);
    }
}