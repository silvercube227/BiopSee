namespace BiopSee.Services;

public sealed class PictureService : IPictureService
{
    private readonly string _imageFolder;
    private readonly ILogger<PictureService> _logger;
    private readonly HttpClient _httpClient;
    private readonly string _cameraEndpoint;
    private readonly int _cameraIndex;

    public PictureService(IConfiguration configuration, HttpClient httpClient, ILogger<PictureService> logger)
    {
        _logger = logger;
        _httpClient = httpClient;
        if (configuration["cameraIndex"] is string i)
            _cameraIndex = int.Parse(i);
        _cameraEndpoint = configuration["cameraEndpoint"] ?? "http://localhost:7701";

        // Find and make sure the folder to store images exists.
        _imageFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BiopSee", "Images");
        if (!Directory.Exists(_imageFolder))
            Directory.CreateDirectory(_imageFolder);
    }

    public string? TakePicture(bool align = false)
    {
        var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        var name = $"{now}---{Guid.NewGuid()}.png";
        var path = Path.Combine(_imageFolder, name);
        var endpoint = _cameraEndpoint + (align ? "/alignment/" : "/picture/") + _cameraIndex.ToString();
        var resp = _httpClient.Send(new() { RequestUri = new(endpoint), Method = HttpMethod.Get });
        if (!resp.IsSuccessStatusCode)
        {
            _logger.LogCritical($"Could not take picture at {_cameraEndpoint}.");
            return null;
        }
        using var fs = File.OpenWrite(path);
        resp.Content.ReadAsStream().CopyTo(fs);
        return name;
    }

    public void Prune(ISet<string> keep)
    {
        var now = DateTime.UtcNow;
        DirectoryInfo di = new(_imageFolder);
        foreach (var file in di.EnumerateFiles())
        {
            var creation = DateTimeOffset.FromUnixTimeSeconds(long.Parse(file.Name.Split("---")[0])).UtcDateTime;
            if (now - creation > TimeSpan.FromMinutes(5) && !keep.Contains(file.Name))
                file.Delete();
        }
    }
}
