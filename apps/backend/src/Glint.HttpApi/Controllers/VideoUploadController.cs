using Glint.HttpApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace Glint.HttpApi.Controllers;

[ApiController]
[Route("api")]
public class VideoUploadController : ControllerBase
{
    private readonly IVideoUploadService _videoUploadService;

    public VideoUploadController(IVideoUploadService videoUploadService)
    {
        _videoUploadService = videoUploadService;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadVideo([FromForm] IFormFile? file)
    {
        try
        {
            var fileId = await _videoUploadService.UploadAsync(file);
            return Created(string.Empty, new { fileId });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (IOException)
        {
            return StatusCode(500, "File could not be saved.");
        }
    }
}