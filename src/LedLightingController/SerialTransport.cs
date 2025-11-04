using System.IO.Ports;

namespace LedLightingController;

public class SerialTransport : IDisposable
{
    private readonly SerialPort _port;

    public SerialTransport(string portName, int baud = 115200)
    {
        _port = new SerialPort(portName, baud)
        {
            NewLine = "\n"
        };
        _port.Open();
    }

    public void Send(string command)
    {
        _port.WriteLine(command);
    }

    public void Dispose()
    {
        if (_port.IsOpen) _port.Close();
        _port.Dispose();
    }
}
