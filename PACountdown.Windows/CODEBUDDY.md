# PACountdown Windows — CodeBuddy Guide

## Commands

- **Build**: `dotnet build` (debug) or `dotnet build -c Release` (release)
- **Run**: `dotnet run` or `dotnet run -c Release`
- **Test**: `dotnet test` (if tests are added)
- **Publish**: `dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true`
- **Clean**: `dotnet clean`
- **Restore packages**: `dotnet restore`
- **Build script**: Run `build.bat` for automated build and publish

## Architecture Overview

- **Framework**: WPF + .NET 8 with MVVM pattern
- **Entry point**: App.xaml/App.xaml.cs sets up DI container and shows MainWindow
- **UI Layer**: MainWindow.xaml with data binding to MainWindowViewModel
- **View Model**: MainWindowViewModel manages all application state using CommunityToolkit.Mvvm
- **Services**:
  - `ISettingsService`: Windows Registry persistence for user preferences
  - `IAudioService`: Windows system sounds for notifications
  - `IMarketHoursService`: US market hours logic with Eastern timezone
- **Models**: MarketMode enum (US/Global)
- **Styling**: Dark theme in Styles/AppStyles.xaml with Windows 11 design
- **Localization**: .resx files for English/Chinese support

## Key Features

- **Timer Logic**: High-frequency polling (50ms) to detect system clock second changes
- **Market Hours**: Eastern timezone, weekdays 9:30-16:00 for US mode
- **Audio**: Windows SystemSounds.Beep/Exclamation with fallback to Console.Beep
- **Settings**: Registry storage under HKEY_CURRENT_USER\SOFTWARE\PACountdown
- **UI**: Responsive WPF with modern dark theme and emoji icons

## Development Notes

- Uses dependency injection with Microsoft.Extensions.DependencyInjection
- MVVM implementation with CommunityToolkit.Mvvm for commands and property notifications
- Timer synchronization matches macOS version behavior
- Settings automatically save on property changes
- Supports both US market hours and 24/7 global mode

## File Structure

```
PACountdown.Windows/
├── Models/MarketMode.cs
├── Services/
│   ├── I*Service.cs (interfaces)
│   └── *Service.cs (implementations)
├── ViewModels/MainWindowViewModel.cs
├── Views/MainWindow.xaml[.cs]
├── Styles/AppStyles.xaml
├── Resources/Strings[.zh-CN].resx
└── App.xaml[.cs]
```

## Troubleshooting

- **Timer issues**: Ensure DispatcherTimer is used for UI thread updates
- **Audio problems**: Check Windows system sounds are enabled
- **Registry access**: App gracefully handles denied registry access
- **Timezone**: Uses "Eastern Standard Time" Windows timezone ID