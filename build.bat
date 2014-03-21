REM test.bat
REM http://stackoverflow.com/questions/734598/how-do-i-make-a-batch-file-terminate-upon-encountering-an-error
REM if %errorlevel% neq 0 exit /b %errorlevel%

set UNITY_EXE="C:\Program Files (x86)\Unity\Editor\Unity.exe"
set UNITY_BAT=%UNITY_EXE% -quit -batchmode
set NAME=orbitris
mkdir build
mkdir build\win
mkdir build\win\%NAME%
mkdir build\mac
mkdir build\linux32
mkdir build\linux32\%NAME%
mkdir build\linux64
mkdir build\linux64\%NAME%

%UNITY_BAT% -buildWebPlayer build\%NAME% .
%UNITY_BAT% -buildWindowsPlayer build\win\%NAME%\%NAME%.exe .
%UNITY_BAT% -buildOSXPlayer build\mac\%NAME%.app .
%UNITY_BAT% -buildLinux32Player build\linux32\%NAME%\%NAME% .
%UNITY_BAT% -buildLinux64Player build\linux64\%NAME%\%NAME% .
git rev-parse HEAD > build\bat-hash
REM because DOS coding sucks
echo "Windows build done. Now run build.sh from linux."
