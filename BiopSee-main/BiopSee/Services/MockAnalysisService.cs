namespace BiopSee.Services;

public class MockAnalysisService : IAnalysisService
{
    public async Task<string> AnalyzeAsync(Scan scan, string type)
    {
        await Task.Delay(3000);
        return "X.2 Y.32 96.8% +-1% confidence";
    }
}
