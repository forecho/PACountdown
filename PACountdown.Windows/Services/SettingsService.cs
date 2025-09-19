using Microsoft.Win32;
using PACountdown.Windows.Models;
using System;

namespace PACountdown.Windows.Services;

public class SettingsService : ISettingsService
{
    private const string RegistryKeyPath = @"SOFTWARE\PACountdown";
    private const string PreNotificationSecondsKey = "PreNotificationSeconds";
    private const string MarketModeKey = "MarketMode";
    private const string NotificationsEnabledKey = "NotificationsEnabled";

    public int PreNotificationSeconds { get; set; } = 10;
    public MarketMode MarketMode { get; set; } = MarketMode.US;
    public bool AreNotificationsEnabled { get; set; } = true;

    public SettingsService()
    {
        Load();
    }

    public void Save()
    {
        try
        {
            using var key = Registry.CurrentUser.CreateSubKey(RegistryKeyPath);
            key.SetValue(PreNotificationSecondsKey, PreNotificationSeconds);
            key.SetValue(MarketModeKey, MarketMode.ToString());
            key.SetValue(NotificationsEnabledKey, AreNotificationsEnabled);
        }
        catch (Exception)
        {
            // Silently fail if registry access is denied
        }
    }

    public void Load()
    {
        try
        {
            using var key = Registry.CurrentUser.OpenSubKey(RegistryKeyPath);
            if (key != null)
            {
                if (key.GetValue(PreNotificationSecondsKey) is int preNotificationSeconds)
                    PreNotificationSeconds = preNotificationSeconds;

                if (key.GetValue(MarketModeKey) is string marketModeStr &&
                    Enum.TryParse<MarketMode>(marketModeStr, out var marketMode))
                    MarketMode = marketMode;

                if (key.GetValue(NotificationsEnabledKey) is bool notificationsEnabled)
                    AreNotificationsEnabled = notificationsEnabled;
            }
        }
        catch (Exception)
        {
            // Use defaults if loading fails
        }
    }
}