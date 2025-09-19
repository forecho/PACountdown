using PACountdown.Windows.Models;
using System;

namespace PACountdown.Windows.Services;

public class MarketHoursService : IMarketHoursService
{
    public bool IsMarketOpen(MarketMode marketMode)
    {
        return marketMode switch
        {
            MarketMode.Global => true,
            MarketMode.US => IsUSMarketOpen(),
            _ => false
        };
    }

    public string GetMarketStatusMessage(MarketMode marketMode)
    {
        return marketMode switch
        {
            MarketMode.Global => "Always available (24/7)",
            MarketMode.US => IsUSMarketOpen() ? "Market is open" : "Market is closed",
            _ => "Unknown market mode"
        };
    }

    private bool IsUSMarketOpen()
    {
        var easternTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
        var easternTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, easternTimeZone);

        // Check if it's a weekday (Monday = 1, Friday = 5)
        var dayOfWeek = (int)easternTime.DayOfWeek;
        if (dayOfWeek < 1 || dayOfWeek > 5)
            return false;

        // Check if it's within market hours (9:30 AM - 4:00 PM ET)
        var currentTimeInMinutes = easternTime.Hour * 60 + easternTime.Minute;
        var marketOpenTimeInMinutes = 9 * 60 + 30; // 9:30 AM
        var marketCloseTimeInMinutes = 16 * 60;    // 4:00 PM

        return currentTimeInMinutes >= marketOpenTimeInMinutes &&
               currentTimeInMinutes < marketCloseTimeInMinutes;
    }
}