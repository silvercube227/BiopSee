namespace BiopSee.Models;

public record ScanInstructionSet
{
    public required double Z { get; set; }
    public required int XCount { get; set; }
    public required int YCount { get; set; }
    public required double Spacing { get; set; }
    public required double XCenter { get; set; }
    public required double YCenter { get; set; }
    public double XWidth => (XCount - 1) * Spacing;
    public double YWidth => (YCount - 1) * Spacing;
    public double TotalCount => XCount * YCount;
    public double Area => YWidth * XWidth;
    public IReadOnlyList<string> Errors
    {
        get
        {
            List<string> errors = new();
            if (XCount < 1)
                errors.Add("X Count cannot be less than 1");

            if (YCount < 1)
                errors.Add("Y Count cannot be less than 1");

            if (Spacing < 0.1)
                errors.Add("Spacing cannot be less than 0.1");
            return errors;
        }
    }
    public IReadOnlyList<ScanInstruction> Instructions()
    {
        var centerXIndex = (XCount % 2 == 0 ? XCount : XCount - 1) / 2;
        var centerYIndex = (YCount % 2 == 0 ? YCount : YCount - 1) / 2;

        var Ys = Enumerable.Range(0, YCount).Select(y => Math.Round(Spacing * (y - centerYIndex), 1));
        var Xs = Enumerable.Range(0, XCount).Select(X => Math.Round(Spacing * (X - centerXIndex), 1)).ToList();
        return Ys.SelectMany(y =>
        {
            Xs.Reverse();
            return Xs.Select(x => new ScanInstruction() { X = x, Y = y, Z = Z });
        }).ToList();
    }
}
