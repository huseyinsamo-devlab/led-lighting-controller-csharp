using LedLightingController;

// Kullanım:
// dotnet run -- COM3 on
// dotnet run -- COM3 bright 60
// dotnet run -- COM3 color 255 100 0
// dotnet run -- COM3 off
if (args.Length == 0 || args[0].Equals("--help", StringComparison.OrdinalIgnoreCase))
{
    Console.WriteLine("Usage: dotnet run -- <PORT> <on|off|bright|color> [args]");
    Environment.Exit(0);
}

string port = args[0];
string cmd  = args.Length > 1 ? args[1].ToLowerInvariant() : "on";

using var transport = new SerialTransport(port);
var led = new LedController(transport);

switch (cmd)
{
    case "on":
        led.On();
        Console.WriteLine("LED: ON");
        break;
    case "off":
        led.Off();
        Console.WriteLine("LED: OFF");
        break;
    case "bright":
        byte pct = args.Length > 2 && byte.TryParse(args[2], out var p) ? p : (byte)100;
        led.Bright(pct);
        Console.WriteLine($"LED: BRIGHT {pct}%");
        break;
    case "color":
        byte r = args.Length > 2 && byte.TryParse(args[2], out var rr) ? rr : (byte)255;
        byte g = args.Length > 3 && byte.TryParse(args[3], out var gg) ? gg : (byte)255;
        byte b = args.Length > 4 && byte.TryParse(args[4], out var bb) ? bb : (byte)255;
        led.Color(r, g, b);
        Console.WriteLine($"LED: COLOR {r},{g},{b}");
        break;
    default:
        Console.WriteLine("Unknown command. Use on|off|bright|color");
        break;
}
