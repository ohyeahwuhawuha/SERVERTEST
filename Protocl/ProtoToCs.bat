@echo off  
set SOURCE_DIR=.
set PROTOGEN_DIR=..\Tool\ProtoGen\protogen.exe
set CS_TARGET_DIR=..\ServerA\Protocl

rem delete old cs
del %CS_TARGET_DIR%\*.cs /f /s /q

rem Gen
for /f "delims=" %%i in ('dir /b "%SOURCE_DIR%\*.proto"') do ( 
echo %PROTOGEN_DIR% -i:%%i -o:%CS_TARGET_DIR%\%%~ni.cs -ns:Protocl
%PROTOGEN_DIR% -i:%%i -o:%CS_TARGET_DIR%\%%~ni.cs -ns:Protocl
)
pause  
