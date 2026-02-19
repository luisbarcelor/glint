using Glint.Application.Dtos;
using Glint.Application.Services;
using Glint.HttpApi.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Glint.HttpApi.Controllers;

[ApiController]
[Route("api")]
public class AssetsController : ControllerBase
{
    private readonly IAssetService _assetService;

    public AssetsController(IAssetService assetService)
    {
        _assetService = assetService;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload([FromForm] UploadAssetRequest request, CancellationToken stoppingToken)
    {
        try
        {
            var fileId = await _assetService.UploadAsync(new UploadAssetInput
            {
                FileName = request.File?.FileName ?? "",
                ContentType = request.File?.ContentType ?? "",
                Length = request.File?.Length ?? 0,
                UploadStream = request.File?.OpenReadStream() ?? Stream.Null
            }, stoppingToken);

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