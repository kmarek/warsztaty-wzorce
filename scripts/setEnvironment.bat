@ECHO OFF

IF "%~1"=="/?" GOTO :help
IF "%~1"=="--help" GOTO :help
IF "%~1"=="" GOTO :help
IF "%~2" NEQ "" GOTO :help

ECHO Executing script with env %1

SETX ROMANUM-ENV %1

goto :end

:help
ECHO Example usage: 
ECHO 	SetEnvironment.bat ws0-dev
ECHO 	SetEnvironment.bat stage

goto :end

:end