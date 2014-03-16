REM test.bat
REM http://stackoverflow.com/questions/734598/how-do-i-make-a-batch-file-terminate-upon-encountering-an-error
REM if %errorlevel% neq 0 exit /b %errorlevel%

set UNITY_EXE="C:\Program Files\Unity\Editor\Unity.exe"
set UNITY_BAT=%UNITY_EXE% -quit -batchmode
mkdir build

%UNITY_BAT% -buildWebPlayer build\orbitris .
%UNITY_BAT% -buildWindowsPlayer build\orbitris.exe .
%UNITY_BAT% -buildOSXPlayer build\orbitris.app .
%UNITY_BAT% -buildLinux32Player build\orbitris-linux32 .
%UNITY_BAT% -buildLinux64Player build\orbitris-linux64 .
