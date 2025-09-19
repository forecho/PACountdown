using Microsoft.Extensions.DependencyInjection;
using PACountdown.Windows.ViewModels;
using System;
using System.Windows;

namespace PACountdown.Windows.Views;

public partial class MainWindow : Window
{
    private MainWindowViewModel ViewModel => (MainWindowViewModel)DataContext;

    public MainWindow(MainWindowViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }

    private void DecreasePreNotification(object sender, RoutedEventArgs e)
    {
        if (ViewModel.PreNotificationSeconds > 1)
        {
            ViewModel.PreNotificationSeconds--;
        }
    }

    private void IncreasePreNotification(object sender, RoutedEventArgs e)
    {
        if (ViewModel.PreNotificationSeconds < 60)
        {
            ViewModel.PreNotificationSeconds++;
        }
    }

    protected override void OnClosed(EventArgs e)
    {
        ViewModel?.Dispose();
        base.OnClosed(e);
    }
}