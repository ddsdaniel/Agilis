mkdir c:\backups
cd C:\Program Files\MongoDB\Server\4.4\bin
mongodump -h localhost -o c:\backups
"%ProgramFiles%\WinRAR\Rar.exe" a -ep1 -idq -r -y "C:\Agilis_backups\%date:~-4,4%-%date:~-7,2%-%date:~-10,2%_%time:~0,2%-%time:~3,2%-%time:~6,2%.rar" "c:\backups"
rmdir c:\backups /s /q
cd C:\Agilis_backups\
forfiles /S /M *.* /D -15 /C "cmd /c del @file
