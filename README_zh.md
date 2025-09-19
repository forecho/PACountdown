# PACountdown

[![CI](https://github.com/forecho/PACountdown/actions/workflows/ci.yml/badge.svg)](https://github.com/forecho/PACountdown/actions/workflows/ci.yml)
[![Release](https://github.com/forecho/PACountdown/actions/workflows/release.yml/badge.svg)](https://github.com/forecho/PACountdown/actions/workflows/release.yml)
[![GitHub release (latest SemVer)](https://img.shields.io/github/v/release/forecho/PACountdown)](https://github.com/forecho/PACountdown/releases/latest)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

一个为股票交易员打造的5分钟K线倒计时器，支持 **macOS** 和 **Windows** 平台。

这款应用提供了一个简洁明了的界面，帮助交易员跟踪与真实市场时间同步的5分钟周期。

![应用截图](https://img.forecho.com/06w2ew.png)

## 功能特性

- **自动5分钟倒计时**: 与时钟的5分钟周期（例如 xx:00, xx:05, xx:10）自动同步。启动时，它会自动计算当前周期内的剩余时间。
- **感知市场时间**: 倒计时仅在美国股市交易时段（周一至周五，美东时间上午9:30 - 下午4:00）自动运行。
- **声音提醒**: 在每个5分钟周期结束前的最后几秒收到声音通知。
    - 提醒的提前时间可以调整（默认为10秒）。
    - 声音提醒可以随时静音/取消静音，而不会影响倒计时。
- **状态显示**:
    - 显示当前市场是"开盘"还是"休市"。
    - 显示当前系统时间。

## 📥 下载安装

前往 [**Releases 页面**](https://github.com/forecho/PACountdown/releases) 下载最新版本：

### macOS 版本
- 下载 `PACountdown-macOS-*.zip`
- 解压并将 `PACountdown.app` 移动到"应用程序"文件夹
- 首次运行需要右键点击选择"打开"（未签名应用要求）

### Windows 版本
- **独立版本**: `PACountdown-Windows-SelfContained-*.zip`（无需安装 .NET）
- **依赖版本**: `PACountdown-Windows-FrameworkDependent-*.zip`（需要 .NET 8.0）
- 解压后运行 `PACountdown.Windows.exe`

## 💻 系统要求

### macOS
- macOS 12.0 (Monterey) 或更高版本
- Apple Silicon 或 Intel 处理器

### Windows
- Windows 10 版本 1909 或更高版本（推荐 Windows 11）
- .NET 8.0 运行时（仅限依赖版本）

## 🛠 开发指南

### macOS 开发
1. 克隆仓库：
   ```bash
   git clone https://github.com/forecho/PACountdown.git
   cd PACountdown
   ```
2. 在 Xcode 中打开 `PACountdown.xcodeproj`
3. 设置构建目标为 "My Mac"
4. 运行项目 (Cmd+R)

### Windows 开发
1. 安装 [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
2. 进入 Windows 项目目录：
   ```bash
   cd PACountdown.Windows
   ```
3. 构建并运行：
   ```bash
   dotnet build
   dotnet run
   ```

## 🚀 如何发布新版本

### 方法一：使用 GitHub Actions（推荐）

1. **访问 GitHub 仓库页面**
2. **点击 "Actions" 标签页**
3. **选择 "Version Bump" 工作流**
4. **点击 "Run workflow" 按钮**
5. **选择版本类型**：
   - `patch`: 修复版本 (1.0.0 → 1.0.1)
   - `minor`: 功能版本 (1.0.0 → 1.1.0)  
   - `major`: 重大版本 (1.0.0 → 2.0.0)
   - 或输入自定义版本号
6. **点击绿色的 "Run workflow" 按钮**

系统将自动：
- 更新版本号
- 创建 Git tag
- 触发构建流程
- 发布到 Releases 页面

### 方法二：手动创建标签

```bash
# 创建新版本标签（例如 v1.0.1）
git tag v1.0.1

# 推送标签到 GitHub
git push origin v1.0.1
```

推送标签后，GitHub Actions 会自动构建并发布新版本。

### 方法三：本地构建

```bash
# 构建所有平台版本
./build-all.sh
```

## 🤖 自动化构建系统

本项目使用 GitHub Actions 实现自动化构建：

- **CI 流水线**: 每次推送和 PR 时自动测试构建
- **发布流水线**: 推送标签时自动构建和发布
- **版本管理**: 一键式版本升级和发布

📖 **[完整发布指南](RELEASE_GUIDE.md)** - 详细的新版本发布说明

### 构建产物

每次发布会自动生成以下文件：

**macOS:**
- `PACountdown-macOS-v*.zip` - 完整的应用程序包

**Windows:**
- `PACountdown-Windows-SelfContained-v*.zip` - 独立可执行文件
- `PACountdown-Windows-FrameworkDependent-v*.zip` - 需要 .NET 运行时

## 🏗 技术栈

### macOS 版本
- SwiftUI
- Combine 框架
- SwiftData

### Windows 版本
- WPF (.NET 8)
- MVVM 模式
- CommunityToolkit.Mvvm

## 📝 发布流程说明

1. **开发阶段**: 在 `main` 分支进行开发
2. **测试**: CI 系统自动测试所有构建
3. **版本发布**: 使用版本管理工作流或手动创建标签
4. **自动构建**: GitHub Actions 自动构建两个平台的版本
5. **自动发布**: 自动创建 Release 页面并上传构建产物

## 🔍 版本号规则

项目采用 [语义化版本控制](https://semver.org/lang/zh-CN/)：

- **主版本号**: 不兼容的 API 修改
- **次版本号**: 向下兼容的功能性新增
- **修订号**: 向下兼容的问题修正

## 💡 开发提示

- 推送到 `main` 分支前确保代码通过 CI 测试
- 使用版本管理工作流可以避免手动操作错误
- 发布前在本地测试构建产物的功能
- 查看 GitHub Actions 日志排查构建问题 