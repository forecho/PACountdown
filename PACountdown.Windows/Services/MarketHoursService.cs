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
        TimeZoneInfo? easternTimeZone = null;
        try
        {
            easternTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
        }
        catch
        {
            // Fallback for systems where the ID differs (e.g., some environments)
            try { easternTimeZone = TimeZoneInfo.FindSystemTimeZoneById("America/New_York"); } catch { }
        }

        var utcNow = DateTime.UtcNow;
        var easternTime = easternTimeZone != null
            ? TimeZoneInfo.ConvertTimeFromUtc(utcNow, easternTimeZone)
            : utcNow; // best effort fallback

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