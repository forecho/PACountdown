using System.Globalization;
using System.Resources;

namespace PACountdown.Windows.Properties;

public static class Resources
{
    private static readonly ResourceManager ResourceManagerInstance =
        new ResourceManager("PACountdown.Windows.Resources.Strings", typeof(Resources).Assembly);

    public static string AppTitle => GetString("AppTitle");
    public static string CountdownTitle => GetString("CountdownTitle");
    public static string MarketOpen => GetString("MarketOpen");
    public static string MarketClosed => GetString("MarketClosed");
    public static string AlwaysAvailable => GetString("AlwaysAvailable");
    public static string SwitchToGlobal => GetString("SwitchToGlobal");
    public static string SwitchToUS => GetString("SwitchToUS");
    public static string MuteNotifications => GetString("MuteNotifications");
    public static string UnmuteNotifications => GetString("UnmuteNotifications");
    public static string TestSound => GetString("TestSound");
    public static string NotificationSettings => GetString("NotificationSettings");
    public static string PreNotificationTime(object seconds) =>
        string.Format(CultureInfo.CurrentUICulture, GetString("PreNotificationTime"), seconds);

    private static string GetString(string name) =>
        ResourceManagerInstance.GetString(name, CultureInfo.CurrentUICulture) ?? name;
}


