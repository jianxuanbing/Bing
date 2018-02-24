@echo off

set /p key=input key:
set source=http://t.loonxierp.com:9498/nuget

for /R "packages" %%s in (*.nupkg) do ( 
    call nuget push %%s %key% -Source %source%	
)

pause