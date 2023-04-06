using EquipmentManagement.Application.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace EquipmentManagement.DAL.ImagesStorage;
public class ImageStorage : IImageStorage
{
    private readonly IConfiguration _configuration;
    private readonly string _storagePath;
    private static readonly string[] SupportedFileExtensions = new[] { ".jpg", ".jpeg", ".png" };

    public ImageStorage(IConfiguration configuration)
    {
        _configuration = configuration;
        _storagePath = _configuration.GetRequiredSection("ImageStorageLocation").Value;
    }

    public Stream GetImageStream(string imageName)
    {
        var path = Path.Combine(_storagePath, imageName);
        var fs = File.OpenRead(path);
        return fs;
    }

    public async Task<string> SaveImageAsync(IFormFile file, CancellationToken cancellationToken)
    {
        var imageExtension = Path.GetExtension(file.FileName);
        if (!SupportedFileExtensions.Any(x => x == imageExtension))
            throw new ArgumentException("Invalid FileType");
        var imageName = Path.GetRandomFileName() + imageExtension;
        var path = Path.Combine(_storagePath, imageName);
        using var fileStream = File.Create(path);
        using var formFileStream = file.OpenReadStream();
        await formFileStream.CopyToAsync(fileStream, 1024, cancellationToken);      
        return imageName;
    }

}
