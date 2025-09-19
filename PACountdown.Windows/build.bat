@echo off
echo Building PACountdown for Windows...

REM Clean previous builds
if exist "bin\Release" rmdir /s /q "bin\Release"
if exist "obj\Release" rmdir /s /q "obj\Release"

REM Build Release configuration
dotnet build -c Release
if %ERRORLEVEL% neq 0 (
    echo Build failed!
    pause
    exit /b %ERRORLEVEL%
)

echo Build completed successfully!

REM Optional: Create self-contained executable
echo.
echo Creating self-contained executable...
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -o "publish\win-x64"

if %ERRORLEVEL% neq 0 (
    echo Publish failed!
    pause
    exit /b %ERRORLEVEL%
)

echo.
echo Self-contained executable created in: publish\win-x64\
echo You can distribute PACountdown.Windows.exe without requiring .NET installation.

pause