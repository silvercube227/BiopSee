namespace BiopSee.Models;

public record Scan
{
    public required Guid Id { get; init; }
    public required DateTime Creation { get; init; }
    public required string Name { get; init; }
    public int Count => Images.Count;
    public required IReadOnlyList<string> Images { get; init; }
    public required IReadOnlyList<string> ScanLocations { get; init; }
    public string? AnalysisResult { get; init; }
}
