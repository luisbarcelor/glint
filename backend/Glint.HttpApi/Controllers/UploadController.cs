using Microsoft.AspNetCore.Mvc;

namespace Glint.HttpApi.Controllers;

[ApiController]
[Route("api")]
public class UploadController : ControllerBase
{

    [HttpPost("upload")]
    public IActionResult UploadVideo([FromForm] IFormFile file)
    {
        if (!file.ContentType.StartsWith("video/"))
            return BadRequest("Only video files are allowed.");
        
        Directory.CreateDirectory("/app/temp");
        
        var fileId = Guid.NewGuid();
        var filePath = Path.Combine("/app/temp", fileId+Path.GetExtension(file.FileName));

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            file.CopyTo(stream);
        }
        
        return System.IO.File.Exists(filePath) 
            ? Created(string.Empty,new {fileId})
            :StatusCode(500, "File could not be saved.");
    }
}