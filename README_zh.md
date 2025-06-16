# PACountdown

一个为股票交易员打造的5分钟K线倒计时器，专为 macOS 设计。

这款应用提供了一个简洁明了的界面，帮助交易员跟踪与真实市场时间同步的5分钟周期。

![应用截图](https://raw.githubusercontent.com/forecho/PACountdown/main/screenshot.png)

## 功能特性

- **自动5分钟倒计时**: 与时钟的5分钟周期（例如 xx:00, xx:05, xx:10）自动同步。启动时，它会自动计算当前周期内的剩余时间。
- **感知市场时间**: 倒计时仅在美国股市交易时段（周一至周五，美东时间上午9:30 - 下午4:00）自动运行。
- **声音提醒**: 在每个5分钟周期结束前的最后几秒收到声音通知。
    - 提醒的提前时间可以调整（默认为10秒）。
    - 声音提醒可以随时静音/取消静音，而不会影响倒计时。
- **状态显示**:
    - 显示当前市场是"开盘"还是"休市"。
    - 显示当前系统时间。

## 如何使用

1.  **下载**: 前往 [Releases 页面](https://github.com/forecho/PACountdown/releases) 下载最新版本。
2.  **安装**: 解压文件并将 `PACountdown.app` 移动到您的"应用程序"文件夹中。
3.  **运行**: 由于该应用并非来自 App Store，首次运行时您可能需要右键点击应用图标，然后选择"打开"。

## 开发者指南

1.  克隆仓库:
    ```bash
    git clone https://github.com/forecho/PACountdown.git
    ```
2.  在 Xcode 中打开 `PACountdown.xcodeproj`。
3.  确保您的构建目标设置为 "My Mac"。
4.  运行项目。

## 使用技术

- SwiftUI
- Combine 框架 