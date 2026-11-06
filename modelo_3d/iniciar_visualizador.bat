@echo off
rem Abre o visualizador 3D da planta Smart 4.0 em http://localhost:8040/viewer/
rem (navegadores bloqueiam carregar .glb direto de file://, por isso o servidor local)
cd /d "%~dp0"
set PORTA=8040
start "" cmd /c "timeout /t 2 >nul & start http://localhost:%PORTA%/viewer/"
where python >nul 2>nul
if %errorlevel%==0 (
  python -m http.server %PORTA% --bind 127.0.0.1
) else (
  where py >nul 2>nul && (py -m http.server %PORTA% --bind 127.0.0.1) || (npx --yes http-server -p %PORTA% -a 127.0.0.1 .)
)
