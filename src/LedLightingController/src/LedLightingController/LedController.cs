namespace LedLightingController;

public class LedController
{
    private readonly SerialTransport _transport;
    public LedController(SerialTransport transport) => _transport = transport;

    // Komutlar: ON / OFF / BRIGHT x / COLOR r,g,b
    public void On()              => _transport.Send("ON");
    public void Off()             => _transport.Send("OFF");
    public void Bright(byte pct)  => _transport.Send($"BRIGHT {pct}");  // 0-100
    public void Color(byte r, byte g, byte b) => _transport.Send($"COLOR {r},{g},{b}");
}
