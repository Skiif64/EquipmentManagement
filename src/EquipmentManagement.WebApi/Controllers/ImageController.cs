using AutoMapper;
using EquimentManagement.Contracts.Requests;
using EquipmentManagement.Application.Abstractions;
using EquipmentManagement.Application.Images.RemoveByNames;
using EquipmentManagement.DAL.ImagesStorage;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace EquipmentManagement.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImageController : ControllerBase
{
    private readonly IImageStorage _storage;
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public ImageController(IImageStorage storage, ISender sender, IMapper mapper)
    {
        _storage = storage;
        _sender = sender;
        _mapper = mapper;
    }

    [HttpPost("add")]
    public async Task<ActionResult> AddImageAsync(CancellationToken cancellationToken)
    {
        var files = Request.Form.Files;
        if (!files.All(x => x.ContentType.StartsWith("image/")))
            return BadRequest();
        List<string> fileNames = new();
        foreach (var file in files)
        {
            var fileName = await _storage.SaveImageAsync(file, cancellationToken);
           fileNames.Add(fileName);
        }

        return Ok(fileNames);
    }

    [HttpGet("{imageName}")]
    public async Task<ActionResult> GetImageByNameAsync(string imageName, CancellationToken cancellationToken)
    {
        var imageExtension = Path.GetExtension(imageName);
        var contentType = "image/" + imageExtension.Substring(1);
        var fileStream = _storage.GetImageStream(imageName);
        return File(fileStream, contentType);
    }

    [HttpPost("delete")]
    public async Task<IActionResult> RemoveImagesByNamesAsync(DeleteImagesRequest request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<RemoveImagesCommand>(request);

        await _sender.Send(command, cancellationToken);

        return Ok();
    }
}
