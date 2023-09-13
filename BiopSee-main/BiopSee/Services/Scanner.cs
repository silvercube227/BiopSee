namespace BiopSee.Services;

public class Scanner : IScanner
{
    private readonly IPictureService _scanningService;
    private readonly ITranslationService _translationService;
    private readonly ICalibrationService _calibrationService;

    public Scanner(IPictureService scanningService, ITranslationService translationService, ICalibrationService calibrationService)
    {
        _scanningService = scanningService;
        _translationService = translationService;
        _calibrationService = calibrationService;
    }

    public Scan TakeScan(string name, IReadOnlyList<ScanInstruction> scanInstructions, double x, double y, double z, IProgress<ScanProgress> progress, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        progress.Report(new()
        {
            Completed = 0,
            Total = scanInstructions.Count,
            ElaspedTime = TimeSpan.Zero,
        });
        var start = DateTime.UtcNow;
        var completed = 0;
        List<string> images = new();
        _translationService.MoveFast(x, y);
        Thread.Sleep(3000);
        foreach (var instruction in scanInstructions)
        {
            var xpos = x + instruction.X;
            var ypos = y + instruction.Y;
            _translationService.Move(xpos, ypos);
            Thread.Sleep(200);
            images.Add(_scanningService.TakePicture() ?? "ERROR");
            cancellationToken.ThrowIfCancellationRequested();
            progress.Report(new()
            {
                Completed = ++completed,
                Total = scanInstructions.Count,
                ElaspedTime = DateTime.UtcNow - start,
            });
        }
        var locations = scanInstructions.Select(x => $"X{x.X} Y{x.Y} Z{x.Z}").ToList();
        return new()
        {
            Id = Guid.NewGuid(),
            Name = name,
            Creation = DateTime.UtcNow,
            Images = images,
            ScanLocations = locations,
        };
    }
}
