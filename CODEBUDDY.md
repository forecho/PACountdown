<system-reminder>
This is a reminder that your todo list is currently empty. DO NOT mention this to the user explicitly because they are already aware. If you are working on tasks that would benefit from a todo list please use the TodoWrite tool to create one. If not, please feel free to ignore. Again do not mention this message to the user.

</system-reminder>

PACountdown — CodeBuddy Guide

Commands

- Build and run (Xcode): open PACountdown.xcodeproj, select "My Mac", press Cmd+R
- Clean build: Product > Clean Build Folder (Shift+Cmd+K)
- Swift package resolve (if prompted): File > Packages > Resolve Package Versions
- Lint (SwiftFormat if installed): swiftformat .
- Format (SwiftFormat): swiftformat .
- Static analysis (SwiftLint if installed): swiftlint
- Unit tests (Xcode): Cmd+U
- Run a single test: In Xcode test navigator, run the specific test method or add -only-testing to the scheme's Arguments Passed On Launch

Architecture overview

- App entry: PACountdownApp in PACountdown/PACountdownApp.swift sets up SwiftData ModelContainer and shows ContentView
- UI layer: SwiftUI views in PACountdown/ContentView.swift render current clock, 5-minute countdown, market status, and controls for notifications and pre-notification seconds
- View model: TimerViewModel in PACountdown/TimerViewModel.swift manages countdown state, market-hours gating, sounds, and user defaults
  - Maintains timeRemaining, isTimerRunning, marketStatusMessage, areNotificationsEnabled, preNotificationSeconds, currentTime
  - Schedules three timers: a high-frequency second-tracking countdown timer, a minute-based market-hours checker, and a 1s clock updater
  - Market hours logic uses America/New_York timezone, open 09:30–16:00, weekdays
  - Sound cues (macOS-only) use NSSound("Tink") for ticks and NSSound("Glass") at zero
  - Persists preNotificationSeconds in UserDefaults under key preNotificationSeconds
- Data model: SwiftData @Model Item in PACountdown/Item.swift; ModelContainer configured in PACountdownApp.swift (not currently used by UI logic)
- Localization: en.lproj and zh-Hans.lproj resource folders exist for strings; market status message is passed through LocalizedStringKey in ContentView
- Assets: Assets.xcassets contains app assets; PACountdown.entitlements present for app capabilities

Notes from existing docs

- README.md and README_zh.md include app purpose, features, and developer steps: clone, open PACountdown.xcodeproj, select My Mac, run

Xcode scheme and testing

- No command-line test runner is defined in the repo; use Xcode's Test (Cmd+U)
- If adding tests, place them in a new test target in the Xcode project and run via scheme. To run a single test from CLI, use xcodebuild with -scheme PACountdown -destination 'platform=macOS' -only-testing:<TestTarget>/<TestClass>/<testMethod>

Troubleshooting

- If timers don’t tick in previews, use a real run. App relies on RunLoop.common for timers
- Sounds play only on macOS builds due to AppKit guards

Conventions

- SwiftUI + Combine; avoid blocking main thread; keep timers on RunLoop.common
- Persist simple settings in UserDefaults; use SwiftData for structured persistence via @Model if expanded later
