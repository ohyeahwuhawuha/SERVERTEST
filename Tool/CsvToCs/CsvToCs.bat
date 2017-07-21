@echo off

set RootPath=%CD%
set ConfigPath="%RootPath%\Config.txt"
set CSVPath="%RootPath%\CSV"
set CSPath="%RootPath%\CS"

for /f "eol=-" %%i in (Config.txt) do (
	echo Create %%i
	call python CsvToCs.py -f %%i -i %CSVPath% -o %CSPath%
)


pause