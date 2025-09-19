# PACountdown

[![CI](https://github.com/forecho/PACountdown/actions/workflows/ci.yml/badge.svg)](https://github.com/forecho/PACountdown/actions/workflows/ci.yml)
[![Release](https://github.com/forecho/PACountdown/actions/workflows/release.yml/badge.svg)](https://github.com/forecho/PACountdown/actions/workflows/release.yml)
[![GitHub release (latest SemVer)](https://img.shields.io/github/v/release/forecho/PACountdown)](https://github.com/forecho/PACountdown/releases/latest)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

A 5-minute candlestick countdown timer for stock traders, available for **macOS** and **Windows**.

This app provides a simple, clean interface to help traders track 5-minute intervals, synchronized with real-world market time.

![App Screenshot](https://img.forecho.com/06w2ew.png)

## Features

- **Automatic 5-Minute Countdown**: Automatically syncs to the 5-minute intervals of the clock (e.g., xx:00, xx:05, xx:10). When launched, it calculates the remaining time in the current interval.
- **Market Hours Awareness**: The timer automatically runs only during US stock market hours (Mon-Fri, 9:30 AM - 4:00 PM ET).
- **Sound Alerts**: Get an audible notification in the final seconds before a 5-minute interval ends.
    - The pre-notification time is adjustable (default: 10 seconds).
    - Sound alerts can be muted/unmuted anytime without stopping the timer.
- **Status Display**:
    - Shows whether the market is currently "Open" or "Closed".
    - Displays the current system time.

## 📥 Download

Go to the [**Releases**](https://github.com/forecho/PACountdown/releases) page and download the latest version for your platform:

### macOS
- Download `PACountdown-macOS-*.zip`
- Unzip and move `PACountdown.app` to your Applications folder
- Right-click and select "Open" on first launch (required for unsigned apps)

### Windows
- **Self-contained**: `PACountdown-Windows-SelfContained-*.zip` (no .NET required)
- **Framework-dependent**: `PACountdown-Windows-FrameworkDependent-*.zip` (requires .NET 8.0)
- Extract and run `PACountdown.Windows.exe`

## 💻 System Requirements

### macOS
- macOS 12.0 (Monterey) or later
- Apple Silicon or Intel processor

### Windows
- Windows 10 version 1909 or later (Windows 11 recommended)
- .NET 8.0 Runtime (for framework-dependent version only)

## 🛠 Development

### macOS Development
1. Clone the repository:
   ```bash
   git clone https://github.com/forecho/PACountdown.git
   cd PACountdown
   ```
2. Open `PACountdown.xcodeproj` in Xcode
3. Set build target to "My Mac"
4. Run the project (Cmd+R)

### Windows Development
1. Install [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
2. Navigate to Windows project:
   ```bash
   cd PACountdown.Windows
   ```
3. Build and run:
   ```bash
   dotnet build
   dotnet run
   ```

### 🤖 Automated Builds

This project uses GitHub Actions for automated building and releasing:

- **CI Pipeline**: Tests builds on every push/PR
- **Release Pipeline**: Automatically builds and publishes releases when tags are pushed
- **Version Management**: Use the "Version Bump" workflow to create new releases

📖 **[Complete Release Guide](RELEASE_GUIDE.md)** - Detailed instructions for publishing new versions

## 🏗 Technologies Used

### macOS
- SwiftUI
- Combine Framework
- SwiftData

### Windows
- WPF (.NET 8)
- MVVM Pattern
- CommunityToolkit.Mvvm 