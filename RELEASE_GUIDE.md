# 📦 PACountdown 发布指南

## 🚀 快速发布新版本

### 最简单的方法（推荐）

1. **访问 GitHub 仓库**: https://github.com/forecho/PACountdown
2. **点击 "Actions"** 标签页
3. **选择 "Version Bump"** 工作流
4. **点击 "Run workflow"** 按钮
5. **选择版本类型**:
   - `patch`: 修复版本 (1.0.0 → 1.0.1) - 用于 bug 修复
   - `minor`: 功能版本 (1.0.0 → 1.1.0) - 用于新功能
   - `major`: 重大版本 (1.0.0 → 2.0.0) - 用于重大更改
6. **点击绿色的 "Run workflow"** 按钮

✅ **完成！** 系统会自动：
- 更新版本号
- 创建 Git 标签
- 构建 macOS 和 Windows 版本
- 发布到 Releases 页面

---

## 🔄 发布流程详解

### 步骤 1: 版本管理工作流触发

当你运行 "Version Bump" 工作流时：

```
Version Bump Workflow
├── 获取当前版本号
├── 计算新版本号
├── 更新项目文件中的版本号
├── 提交更改到仓库
└── 创建并推送新的 Git 标签
```

### 步骤 2: 自动构建触发

新标签推送后，自动触发 Release 工作流：

```
Release Workflow
├── macOS 构建作业
│   ├── 使用 Xcode 构建
│   ├── 创建 .app 包
│   └── 生成 PACountdown-macOS-v*.zip
├── Windows 构建作业
│   ├── 使用 .NET 8 构建
│   ├── 创建独立可执行文件
│   ├── 创建依赖运行时版本
│   ├── 生成 PACountdown-Windows-SelfContained-v*.zip
│   └── 生成 PACountdown-Windows-FrameworkDependent-v*.zip
└── 发布作业
    ├── 下载所有构建产物
    ├── 生成发布说明
    └── 创建 GitHub Release
```

### 步骤 3: 自动发布

最终在 Releases 页面生成：

- 📱 **macOS 版本**: `PACountdown-macOS-v1.0.1.zip`
- 🪟 **Windows 独立版**: `PACountdown-Windows-SelfContained-v1.0.1.zip`
- 🪟 **Windows 依赖版**: `PACountdown-Windows-FrameworkDependent-v1.0.1.zip`
- 📄 **发布说明**: 包含功能介绍和安装指南

---

## 🛠 手动发布方法

### 方法 A: Git 命令行

```bash
# 1. 确保代码已提交
git add .
git commit -m "准备发布 v1.0.1"
git push origin main

# 2. 创建标签
git tag v1.0.1

# 3. 推送标签（触发自动构建）
git push origin v1.0.1
```

### 方法 B: 本地构建

```bash
# 构建所有平台
./build-all.sh

# 手动上传到 Releases 页面
```

---

## 📋 发布前检查清单

### 开发完成
- [ ] 所有功能已实现并测试
- [ ] 代码已提交到 `main` 分支
- [ ] CI 测试通过（绿色状态）

### 版本准备
- [ ] 确定版本号类型（patch/minor/major）
- [ ] 检查 `VERSION.txt` 文件
- [ ] 更新 CHANGELOG（如果有）

### 发布执行
- [ ] 运行 Version Bump 工作流
- [ ] 等待构建完成（约 10-15 分钟）
- [ ] 检查 Releases 页面
- [ ] 下载并测试构建产物

### 发布后
- [ ] 测试下载的应用程序
- [ ] 更新文档（如需要）
- [ ] 通知用户新版本发布

---

## 🔍 故障排除

### 构建失败
1. **检查 Actions 页面的错误日志**
2. **常见问题**:
   - macOS 构建: Xcode 版本兼容性
   - Windows 构建: .NET SDK 版本
   - 权限问题: GitHub token 权限

### 版本号问题
- 确保标签格式为 `vX.Y.Z`（如 `v1.0.1`）
- 不要重复使用已存在的标签

### 发布页面问题
- 检查 GitHub token 是否有 `contents: write` 权限
- 确保仓库设置允许创建 Releases

---

## 📊 版本号策略

### 语义化版本控制

| 版本类型 | 何时使用 | 示例 |
|---------|---------|------|
| **Patch** | Bug 修复、小改进 | 1.0.0 → 1.0.1 |
| **Minor** | 新功能、向后兼容 | 1.0.0 → 1.1.0 |
| **Major** | 重大更改、不兼容 | 1.0.0 → 2.0.0 |

### 版本号示例

```
v1.0.0  - 首次正式发布
v1.0.1  - 修复倒计时精度问题
v1.1.0  - 添加 Windows 版本支持
v1.1.1  - 修复 Windows 音频问题
v1.2.0  - 添加多语言支持
v2.0.0  - 重构架构，UI 大改版
```

---

## 🎯 发布最佳实践

### 发布频率
- **Bug 修复**: 及时发布（patch 版本）
- **新功能**: 每 2-4 周发布（minor 版本）
- **重大更新**: 每 3-6 月发布（major 版本）

### 测试建议
- 本地测试所有主要功能
- 在不同系统版本上测试
- 检查安装和卸载过程

### 用户沟通
- 在 Releases 页面提供详细的更新说明
- 说明新功能和修复的问题
- 提供安装和升级指南

---

## 📞 需要帮助？

如果在发布过程中遇到问题：

1. **查看 GitHub Actions 日志**
2. **检查本项目的 Issues 页面**
3. **参考 GitHub Actions 官方文档**
4. **联系项目维护者**

记住：自动化构建让发布变得简单，但理解整个流程有助于解决问题！