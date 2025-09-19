using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PACountdown.Windows.Models;
using PACountdown.Windows.Services;
using System;
using System.Threading;
using System.Windows.Threading;

namespace PACountdown.Windows.ViewModels;

public partial class MainWindowViewModel : ObservableObject, IDisposable
{
    private readonly ISettingsService _settingsService;
    private readonly IAudioService _audioService;
    private readonly IMarketHoursService _marketHoursService;

    private readonly DispatcherTimer _countdownTimer;
    private readonly DispatcherTimer _marketHoursTimer;
    private readonly DispatcherTimer _clockTimer;

    private int _lastSecond = -1;

    [ObservableProperty]
    private TimeSpan _timeRemaining = TimeSpan.FromMinutes(5);

    [ObservableProperty]
    private bool _isTimerRunning;

    [ObservableProperty]
    private string _marketStatusMessage = "Market is closed";

    [ObservableProperty]
    private MarketMode _marketMode = MarketMode.US;

    [ObservableProperty]
    private int _preNotificationSeconds = 10;

    [ObservableProperty]
    private bool _areNotificationsEnabled = true;

    [ObservableProperty]
    private string _currentTime = "--:--:--";

    public string TimeRemainingDisplay => $"{TimeRemaining.Minutes:D2}:{TimeRemaining.Seconds:D2}";

    public MainWindowViewModel(
        ISettingsService settingsService,
        IAudioService audioService,
        IMarketHoursService marketHoursService)
    {
        _settingsService = settingsService;
        _audioService = audioService;
        _marketHoursService = marketHoursService;

        // Load settings
        MarketMode = _settingsService.MarketMode;
        PreNotificationSeconds = _settingsService.PreNotificationSeconds;
        AreNotificationsEnabled = _settingsService.AreNotificationsEnabled;

        // Initialize timers
        _countdownTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(50) };
        _countdownTimer.Tick += OnCountdownTimerTick;

        _marketHoursTimer = new DispatcherTimer { Interval = TimeSpan.FromMinutes(1) };
        _marketHoursTimer.Tick += OnMarketHoursTimerTick;

        _clockTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
        _clockTimer.Tick += OnClockTimerTick;

        // Start timers
        CheckMarketHours();
        UpdateCurrentTime();

        _marketHoursTimer.Start();
        _clockTimer.Start();
    }

    partial void OnMarketModeChanged(MarketMode value)
    {
        _settingsService.MarketMode = value;
        _settingsService.Save();
        CheckMarketHours();
    }

    partial void OnPreNotificationSecondsChanged(int value)
    {
        _settingsService.PreNotificationSeconds = value;
        _settingsService.Save();
    }

    partial void OnAreNotificationsEnabledChanged(bool value)
    {
        _settingsService.AreNotificationsEnabled = value;
        _settingsService.Save();
    }

    partial void OnTimeRemainingChanged(TimeSpan value)
    {
        OnPropertyChanged(nameof(TimeRemainingDisplay));
    }

    [RelayCommand]
    private void ToggleNotifications()
    {
        AreNotificationsEnabled = !AreNotificationsEnabled;
    }

    [RelayCommand]
    private void TestSound()
    {
        _audioService.TestSound();
    }

    [RelayCommand]
    private void SwitchMarketMode()
    {
        MarketMode = MarketMode == MarketMode.US ? MarketMode.Global : MarketMode.US;
    }

    private void OnCountdownTimerTick(object? sender, EventArgs e)
    {
        var currentSecond = DateTime.Now.Second;

        if (currentSecond != _lastSecond)
        {
            _lastSecond = currentSecond;

            TimeRemaining = TimeRemaining.Subtract(TimeSpan.FromSeconds(1));

            if (TimeRemaining.TotalSeconds < 0)
            {
                CalculateAndSetInitialTime();
                return;
            }

            // Handle sound notifications
            if (AreNotificationsEnabled)
            {
                if (TimeRemaining.TotalSeconds == 1)
                {
                    // Play distinct sound on the last second
                    _audioService.PlayFinalTickSound();
                }
                else if (TimeRemaining.TotalSeconds <= PreNotificationSeconds && TimeRemaining.TotalSeconds > 1)
                {
                    _audioService.PlayTickSound();
                }
            }
        }
    }

    private void OnMarketHoursTimerTick(object? sender, EventArgs e)
    {
        CheckMarketHours();
    }

    private void OnClockTimerTick(object? sender, EventArgs e)
    {
        UpdateCurrentTime();
    }

    private void CheckMarketHours()
    {
        MarketStatusMessage = _marketHoursService.GetMarketStatusMessage(MarketMode);

        if (_marketHoursService.IsMarketOpen(MarketMode))
        {
            StartTimer();
        }
        else
        {
            StopTimer();
        }
    }

    private void StartTimer()
    {
        if (IsTimerRunning) return;

        IsTimerRunning = true;
        CalculateAndSetInitialTime();
        _lastSecond = DateTime.Now.Second;
        _countdownTimer.Start();
    }

    private void StopTimer()
    {
        IsTimerRunning = false;
        _countdownTimer.Stop();
        _lastSecond = -1;
    }

    private void CalculateAndSetInitialTime()
    {
        var now = DateTime.Now;
        var minute = now.Minute;
        var second = now.Second;

        var secondsIntoInterval = (minute % 5) * 60 + second;
        var remainingSeconds = Math.Max(0, 300 - secondsIntoInterval);

        TimeRemaining = TimeSpan.FromSeconds(remainingSeconds);
    }

    private void UpdateCurrentTime()
    {
        CurrentTime = DateTime.Now.ToString("HH:mm:ss");
    }

    public void Dispose()
    {
        _countdownTimer?.Stop();
        _marketHoursTimer?.Stop();
        _clockTimer?.Stop();

        _settingsService?.Save();
    }
}