namespace BiopSee.Models;

public record ScanProgress
{
    public required int Total { get; init; }
    public required int Completed { get; init; }
    public required TimeSpan ElaspedTime { get; init; }
    public double Percentage =>
        (double)Completed * 100 / (double)Total;
    public TimeSpan? Projected =>
        Percentage < 10
        ? null
        : (ElaspedTime / Completed) * (Total - Completed);
}