@echo off
echo ==================================================
echo   FREEING MEMORY AND RESETTING WINDOWS SESSION
echo ==================================================
echo.

:: STEP 1 - Kill Heavy Apps (VS Code, Chrome, WSL)
echo Closing Chrome, VS Code, and WSL sessions...
taskkill /F /IM chrome.exe >nul 2>&1
taskkill /F /IM Code.exe >nul 2>&1
taskkill /F /IM bash.exe >nul 2>&1
echo Done.
echo.

:: STEP 2 - Clear TEMP Files
echo Clearing temporary files...
del /q /f /s %TEMP%\* >nul 2>&1
echo Temp files cleared.
echo.

:: STEP 3 - Flush DNS Cache
echo Flushing DNS cache...
ipconfig /flushdns
echo DNS cache flushed.
echo.

:: STEP 4 - Restart Explorer Safely
echo Restarting Windows Explorer...
taskkill /F /IM explorer.exe >nul 2>&1
timeout /t 2 >nul
start explorer.exe
echo Explorer restarted.
echo.

:: STEP 5 - Show Available Physical Memory
echo Checking available memory...
systeminfo | find "Available Physical Memory"
echo.

echo ==================================================
echo   PROCESS COMPLETED! YOUR MEMORY IS OPTIMIZED.
echo ==================================================
pause
