using EquipmentManagement.DAL.ImagesStorage;
using Microsoft.AspNetCore.Mvc;

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
}
