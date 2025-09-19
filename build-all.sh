#!/bin/bash

# PACountdown - Build All Platforms Script
# This script builds both macOS and Windows versions locally

set -e  # Exit on any error

echo "🚀 PACountdown - Building All Platforms"
echo "======================================"

# Colors for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
NC='\033[0m' # No Color

# Get version
VERSION=$(cat VERSION.txt 2>/dev/null || echo "1.0.0")
echo -e "${BLUE}Version: $VERSION${NC}"
echo ""

# Function to check if command exists
command_exists() {
    command -v "$1" >/dev/null 2>&1
}

# Build macOS version
echo -e "${YELLOW}📱 Building macOS version...${NC}"
if command_exists xcodebuild; then
    echo "Building with Xcode..."
    
    # Clean previous builds
    rm -rf build/
    
    # Build and archive
    xcodebuild -project PACountdown.xcodeproj \
        -scheme PACountdown \
        -configuration Release \
        archive \
        -archivePath ./build/PACountdown.xcarchive
    
    # Create export options
    cat > ExportOptions.plist << EOF
<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE plist PUBLIC "-//Apple//DTD PLIST 1.0//EN" "http://www.apple.com/DTDs/PropertyList-1.0.dtd">
<plist version="1.0">
<dict>
    <key>method</key>
    <string>mac-application</string>
    <key>destination</key>
    <string>export</string>
</dict>
</plist>
EOF
    
    # Export app
    xcodebuild -exportArchive \
        -archivePath ./build/PACountdown.xcarchive \
        -exportPath ./build \
        -exportOptionsPlist ExportOptions.plist
    
    # Create zip
    cd build
    zip -r "../PACountdown-macOS-v$VERSION.zip" PACountdown.app
    cd ..
    
    echo -e "${GREEN}✅ macOS build completed: PACountdown-macOS-v$VERSION.zip${NC}"
else
    echo -e "${RED}❌ Xcode not found. Skipping macOS build.${NC}"
    echo "   Install Xcode from the Mac App Store to build macOS version."
fi

echo ""

# Build Windows version
echo -e "${YELLOW}🪟 Building Windows version...${NC}"
if command_exists dotnet; then
    cd PACountdown.Windows
    
    echo "Restoring dependencies..."
    dotnet restore
    
    echo "Building Release configuration..."
    dotnet build -c Release --no-restore
    
    echo "Publishing self-contained version..."
    dotnet publish -c Release \
        -r win-x64 \
        --self-contained true \
        -p:PublishSingleFile=true \
        -p:IncludeNativeLibrariesForSelfExtract=true \
        -o ../build/win-x64-self-contained
    
    echo "Publishing framework-dependent version..."
    dotnet publish -c Release \
        -r win-x64 \
        --self-contained false \
        -p:PublishSingleFile=true \
        -o ../build/win-x64-framework-dependent
    
    cd ..
    
    # Create zip files
    if command_exists zip; then
        cd build
        zip -r "../PACountdown-Windows-SelfContained-v$VERSION.zip" win-x64-self-contained/
        zip -r "../PACountdown-Windows-FrameworkDependent-v$VERSION.zip" win-x64-framework-dependent/
        cd ..
    else
        echo -e "${YELLOW}⚠️  zip command not found. Creating tar.gz archives instead...${NC}"
        cd build
        tar -czf "../PACountdown-Windows-SelfContained-v$VERSION.tar.gz" win-x64-self-contained/
        tar -czf "../PACountdown-Windows-FrameworkDependent-v$VERSION.tar.gz" win-x64-framework-dependent/
        cd ..
    fi
    
    echo -e "${GREEN}✅ Windows builds completed:${NC}"
    echo -e "   - PACountdown-Windows-SelfContained-v$VERSION.*"
    echo -e "   - PACountdown-Windows-FrameworkDependent-v$VERSION.*"
else
    echo -e "${RED}❌ .NET SDK not found. Skipping Windows build.${NC}"
    echo "   Install .NET 8.0 SDK from https://dotnet.microsoft.com/download"
fi

echo ""
echo -e "${GREEN}🎉 Build process completed!${NC}"
echo ""
echo "📦 Generated files:"
ls -la PACountdown-*v$VERSION.* 2>/dev/null || echo "   No build outputs found"
echo ""
echo -e "${BLUE}💡 Tips:${NC}"
echo "   - Test the applications before distributing"
echo "   - For releases, use: git tag v$VERSION && git push origin v$VERSION"
echo "   - The GitHub Actions will automatically build and release when you push tags"