using EquipmentManagement.DAL.ImagesStorage;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace EquipmentManagement.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImageController : ControllerBase
{
    private readonly ImageStorage _storage;

    public ImageController(ImageStorage storage)
    {
        _storage = storage;
    }

    [HttpPost("add")]
    public async Task<ActionResult> AddImageAsync(CancellationToken cancellationToken)
    {
        var files = Request.Form.Files;
        if (!files.All(x => x.ContentType.StartsWith("image/")))
            return BadRequest();
        foreach (var file in files)
        {
            await _storage.SaveImageAsync(file, cancellationToken);
        }

        return Ok();
    }

    [HttpGet("{imageName}")]
    public async Task<ActionResult> GetImageByNameAsync(string imageName, CancellationToken cancellationToken)
    {
        var imageExtension = Path.GetExtension(imageName);
        var contentType = "image/" + imageExtension.Substring(1);
        var fileStream = _storage.GetImageStream(imageName);
        return File(fileStream, contentType);
    }
}
