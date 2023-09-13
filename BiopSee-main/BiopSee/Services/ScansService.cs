namespace BiopSee.Services;

using LiteDB;

public sealed class ScansService : IScansService, IDisposable
{
    public void Dispose()
    {
        database.Dispose();
    }

    private readonly LiteDatabase database;
    private readonly ILiteCollection<Scan> scans;

    public ScansService()
    {
        var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BiopSee");
        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);
        database = new(Path.Combine(folder, "scans.db"));
        scans = database.GetCollection<Scan>("scans");
    }
    public IEnumerable<Scan> AllScans() =>
        scans.FindAll();

    public void CreateScan(Scan scan)
    {
        scans.Insert(scan);
        Changed?.Invoke();
    }

    public void DeleteScan(Guid id)
    {
        scans.Delete(id);
        Changed?.Invoke();
    }

    public void UpdateScan(Scan scan)
    {
        scans.Update(scan);
        Changed?.Invoke();
    }

    public Scan? ReadScan(Guid id) =>
        scans.FindById(id);
    public event Action? Changed;
}
