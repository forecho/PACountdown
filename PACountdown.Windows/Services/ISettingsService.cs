using PACountdown.Windows.Models;

namespace PACountdown.Windows.Services;

public interface ISettingsService
{
    int PreNotificationSeconds { get; set; }
    MarketMode MarketMode { get; set; }
    bool AreNotificationsEnabled { get; set; }
    
    void Save();
    void Load();
}