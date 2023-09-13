namespace BiopSee.Services;

using System.Text.Json;

public class CalibrationService : ICalibrationService
{
    private readonly string file;
    public CalibrationService()
    {
        var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), nameof(BiopSee));
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);
        file = Path.Combine(directory, "calibrations.json");
        if (!File.Exists(file))
            Save();
        else
            Load();
    }

    public void Save()
    {
        File.WriteAllText(file, JsonSerializer.Serialize(new Dictionary<string, string>
        {
            ["X"] = x.ToString(),
            ["Y"] = y.ToString(),
            ["Z"] = z.ToString(),
        }));
    }

    public void Load()
    {
        var json = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(file));
        x = int.Parse(json?["X"] ?? "0");
        y = int.Parse(json?["Y"] ?? "0");
        z = int.Parse(json?["Z"] ?? "0");
    }

    private double x;
    
    private double y;
    
    private double z;
    public double X 
    { 
        get => 100;
        set
        {
            x = value;
            Save();
        }
    }
    public double Y
    {
        get => 100;
        set
        {
            y = value;
            Save();
        }
    }
    public double Z
    {
        get => 100;
        set
        {
            z = value;
            Save();
        }
    }
}
