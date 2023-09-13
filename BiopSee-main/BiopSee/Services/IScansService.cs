namespace BiopSee.Services;

public interface IScansService
{
    void CreateScan(Scan scan);
    void UpdateScan(Scan scan);
    Scan? ReadScan(Guid id);
    void DeleteScan(Guid id);
    IEnumerable<Scan> AllScans();
    event Action? Changed;
}
