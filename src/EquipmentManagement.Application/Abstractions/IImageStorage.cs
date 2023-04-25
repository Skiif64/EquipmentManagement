using Microsoft.AspNetCore.Http;

namespace EquipmentManagement.Application.Abstractions;
public interface IImageStorage
{
    Stream GetImageStream(string imageName);
    Task<string> SaveImageAsync(IFormFile file, CancellationToken cancellationToken);
    Task RemoveImagesAsync(IEnumerable<string> imageNames, CancellationToken cancellationToken);
}