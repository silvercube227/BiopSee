namespace BiopSee.Services;

public interface IScanner
{
    Scan TakeScan(string name, IReadOnlyList<ScanInstruction> scanInstructions, double x, double y, double z, IProgress<ScanProgress> progress, CancellationToken cancellationToken);
}
