# PACountdown for Windows

A 5-minute candlestick countdown timer for stock traders, built for Windows 11 with WPF and .NET 8.

## Features

- **Automatic 5-Minute Countdown**: Syncs to 5-minute intervals (e.g., xx:00, xx:05, xx:10)
- **Market Hours Awareness**: Runs during US stock market hours (Mon-Fri, 9:30 AM - 4:00 PM ET) or 24/7 global mode
- **Sound Alerts**: Configurable pre-notification alerts with Windows system sounds
- **Dark Theme**: Modern Windows 11-style dark interface
- **Settings Persistence**: Saves preferences in Windows Registry
- **Bilingual Support**: English and Chinese (Simplified) interface

## Requirements

- Windows 10 version 1909 or later (Windows 11 recommended)
- .NET 8.0 Runtime (Desktop Apps)

## Installation

### Option 1: Download Release (Recommended)
1. Go to [Releases](https://github.com/forecho/PACountdown/releases)
2. Download `PACountdown.Windows.zip`
3. Extract and run `PACountdown.Windows.exe`

### Option 2: Build from Source
1. Install [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
2. Clone the repository:
   ```bash
   git clone https://github.com/forecho/PACountdown.git
   cd PACountdown/PACountdown.Windows
   ```
3. Build and run:
   ```bash
   dotnet build
   dotnet run
   ```

## Development

### Prerequisites
- Visual Studio 2022 or VS Code with C# extension
- .NET 8.0 SDK

### Build Commands
```bash
# Debug build
dotnet build

# Release build
dotnet build -c Release

# Run application
dotnet run

# Publish self-contained executable
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
```

### Project Structure
```
PACountdown.Windows/
├── Models/           # Data models (MarketMode)
├── Services/         # Business logic services
├── ViewModels/       # MVVM view models
├── Views/           # WPF windows and user controls
├── Styles/          # XAML styles and themes
├── Resources/       # Localization resources
└── App.xaml         # Application entry point
```

### Architecture
- **MVVM Pattern**: Clean separation of concerns
- **Dependency Injection**: Using Microsoft.Extensions.DependencyInjection
- **Services**: Market hours, audio notifications, settings persistence
- **Localization**: Resource-based multilingual support

## Configuration

Settings are automatically saved to Windows Registry under:
`HKEY_CURRENT_USER\SOFTWARE\PACountdown`

- `PreNotificationSeconds`: Alert timing (1-60 seconds)
- `MarketMode`: US market hours or Global 24/7
- `NotificationsEnabled`: Sound alerts on/off

## Troubleshooting

### Audio Issues
- Ensure Windows system sounds are enabled
- Check Windows audio mixer settings
- Try the "Test Sound" button

### Timer Accuracy
- The app uses high-frequency polling (50ms) for precise timing
- System clock changes are automatically detected

### Performance
- Minimal CPU usage when running
- Uses Windows DispatcherTimer for UI updates

## License

MIT License - see LICENSE file for details