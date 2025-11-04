# led-lighting-controller-csharp

Minimal C#/.NET console app to control LED lighting via **serial** (Arduino/STM32).
Commands: `on`, `off`, `bright <0-100>`, `color <r> <g> <b>`.

## Quick Start
```bash
# Build & run (example on Windows, COM3)
dotnet build
dotnet run --project src/LedLightingController -- COM3 on
dotnet run --project src/LedLightingController -- COM3 bright 60
dotnet run --project src/LedLightingController -- COM3 color 255 100 0
