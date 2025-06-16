# PACountdown

A 5-minute candlestick countdown timer for stock traders, built for macOS.

This app provides a simple, clean interface to help traders track 5-minute intervals, synchronized with real-world market time.

![App Screenshot](https://raw.githubusercontent.com/forecho/PACountdown/main/screenshot.png)

## Features

- **Automatic 5-Minute Countdown**: Automatically syncs to the 5-minute intervals of the clock (e.g., xx:00, xx:05, xx:10). When launched, it calculates the remaining time in the current interval.
- **Market Hours Awareness**: The timer automatically runs only during US stock market hours (Mon-Fri, 9:30 AM - 4:00 PM ET).
- **Sound Alerts**: Get an audible notification in the final seconds before a 5-minute interval ends.
    - The pre-notification time is adjustable (default: 10 seconds).
    - Sound alerts can be muted/unmuted anytime without stopping the timer.
- **Status Display**:
    - Shows whether the market is currently "Open" or "Closed".
    - Displays the current system time.

## How to Use

1.  **Download**: Go to the [Releases](https://github.com/forecho/PACountdown/releases) page and download the latest version.
2.  **Install**: Unzip the file and move `PACountdown.app` to your Applications folder.
3.  **Run**: Since the app is not from the App Store, you may need to right-click the app icon and select "Open" the first time you run it.

## For Developers

1.  Clone the repository:
    ```bash
    git clone https://github.com/forecho/PACountdown.git
    ```
2.  Open `PACountdown.xcodeproj` in Xcode.
3.  Make sure your build target is set to "My Mac".
4.  Run the project.

## Technologies Used

- SwiftUI
- Combine Framework 