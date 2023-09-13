namespace BiopSee.Services;

public class MockTranslationService : ITranslationService
{
    public string SerialPortName { get; set; } = string.Empty;

    public void Move(double x, double y, double z) =>
        Console.WriteLine($"X{x}\t Y{y}\t Z{z}");
    public void MoveFast(double x, double y, double z) =>
        Console.WriteLine($"Fast X{x}\t Y{y}\t Z{z}");

    public void Move(double x, double y) =>
        Console.WriteLine($"X{x}\t Y{y}");

    public void Move(double z) =>
        Console.WriteLine($"Z{z}");

    public void ResetToOrigin() =>
        Console.WriteLine("Setting to origin");

    public void MoveFast(double x, double y)
    {
        throw new NotImplementedException();
    }
}
