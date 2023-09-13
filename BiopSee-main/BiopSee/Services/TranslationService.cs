namespace BiopSee.Services;

using System.IO.Ports;

public sealed class TranslationService : ITranslationService, IDisposable
{
    private const string PORTNAME = "/dev/cu.usbserial-140";
    private SerialPort? _serialPort;
    private readonly string _file;
    private readonly ILogger<TranslationService> _logger;

    public TranslationService(IConfiguration configuration, ILogger<TranslationService> logger)
    {
        _logger = logger;
        var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BiopSee");
        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);
        _file = Path.Combine(folder, "printerPort");
        if (!File.Exists(_file))
            File.WriteAllText(_file, "COM4");
        try
        {
            _serialPort = new(PORTNAME, 115200);
            _serialPort.Open();
        }
        catch
        {
            _logger.LogCritical($"Unable to find serial port: {SerialPortName}");
        }
        _serialPort?.WriteLine("M107 P2");
        Console.WriteLine(_serialPort?.ReadLine());   
    }

    public string SerialPortName
    {
        get => File.ReadAllText(_file);
        set
        {
            File.WriteAllText(_file, value);
            _serialPort?.Close();
            try
            {
                _serialPort = new("/dev/cu.usbserial-120", 115200);
                _serialPort.Open();
            }
            catch
            {
                _logger.LogCritical($"Unable to find serial port: {SerialPortName}");
            }
        }
    }

    public void Move(double x, double y, double z)
    {
        _serialPort?.WriteLine($"G0 F1000 X{x} Y{y} Z{z}");
        Console.WriteLine(_serialPort?.ReadLine());
    }

    public void MoveFast(double x, double y, double z)
    {
        _serialPort?.WriteLine($"G0 F7000 X{x} Y{y} Z{z}");
        Console.WriteLine(_serialPort?.ReadLine());
    }

    public void MoveFast(double x, double y)
    {
        _serialPort?.WriteLine($"G0 F7000 X{x} Y{y}");
        Console.WriteLine(_serialPort?.ReadLine());
    }

    public void Move(double x, double y)
    {
        _serialPort?.WriteLine($"G0 F1000 X{x} Y{y}");
        Console.WriteLine(_serialPort?.ReadLine());    
    }

    public void Move(double z)
    {
        _serialPort?.WriteLine($"G0 F1000 Z{z}");
        Console.WriteLine(_serialPort?.ReadLine());
    }

    public void ResetToOrigin()
    {
        _serialPort?.WriteLine("G0 F7000 X0 Y0 Z50");
        _serialPort?.ReadLine();
    }

    public void Dispose() =>
        _serialPort?.Close();
}
