REM
REM this file needs to 'save as' US-ASCII so it will function, default of UTF-8 will make it un-runnnable.
REM
REM NOTE: this is run from the _FactoryProject/FactoryDLL/FactoryModel directory

echo     Attempt to copy Library (DLL) into Unity

rem the script directory is also the solution directory
set SolutionDir=%~dp0

set TargetDir=%1

rem Read the config file located at: _developers\{devicename}_{username}.config
for /f "delims=" %%i in ('whoami') do SET UserKey=%%i
echo     UserKey=[%UserKey%]
SET ConfigFile=%SolutionDir%_developers\%UserKey:\=_%.config
echo     ConfigFile=[%ConfigFile%]

rem read fields from config file:
for /f "eol=# delims=" %%a in (%ConfigFile%) do SET "%%a"
rem echo unity.project.relative.path=%unity.project.relative.path%


REM copy build into Unity DLL folder
SET AssetsPath=%SolutionDir%\%unity.project.relative.path%\Assets\_DLLs\
IF exist %AssetsPath% (
    echo     Copying DLL 
    echo     from %TargetDir%; 
    echo     into %AssetsPath%; 
    copy %TargetDir%\*.dll %AssetsPath%\.
) ELSE ( 
    echo     Nothing copied because the DLLs Folder was not found at [%AssetsPath%]
)
