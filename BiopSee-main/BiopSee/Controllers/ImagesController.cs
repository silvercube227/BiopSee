namespace BiopSee.Controllers;

using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public sealed class ImagesController : ControllerBase
{
    [HttpGet("{name}")]
    public IActionResult GetAsync(string name)
    {
        var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BiopSee", "Images");
        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);
        var path = Path.Combine(folder, name);
        if (!System.IO.File.Exists(path))
            return NotFound();
        return new FileStreamResult(System.IO.File.OpenRead(path), "image/png")
        {
            FileDownloadName = $"{name}"
        };
    }
}
