namespace BiopSee.Services;

public interface ITranslationService
{
    void Move(double x, double y, double z);
    void MoveFast(double x, double y, double z);
    void MoveFast(double x, double y);
    void Move(double x, double y);
    void Move(double z);
    void ResetToOrigin();
    public string SerialPortName { get; set; }
}
