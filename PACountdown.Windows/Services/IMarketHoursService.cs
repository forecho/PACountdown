using PACountdown.Windows.Models;

namespace PACountdown.Windows.Services;

public interface IMarketHoursService
{
    bool IsMarketOpen(MarketMode marketMode);
    string GetMarketStatusMessage(MarketMode marketMode);
}