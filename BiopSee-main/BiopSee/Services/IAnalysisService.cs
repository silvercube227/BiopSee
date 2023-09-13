namespace BiopSee.Services;

public interface IAnalysisService
{
    Task<string> AnalyzeAsync(Scan scan, string type);
}
