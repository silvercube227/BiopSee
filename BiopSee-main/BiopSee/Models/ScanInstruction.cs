namespace BiopSee.Models;

public record ScanInstruction
{
    public required double X { get; init; }
    public required double Y { get; init; }
    public required double Z { get; init; }
}
