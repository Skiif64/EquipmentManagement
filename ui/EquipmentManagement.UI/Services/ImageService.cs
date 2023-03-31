using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Headers;

namespace EquipmentManagement.UI.Services;

public class ImageService
{
    private const int MaxFiles = 10;
    private const long MaxFileSize = int.MaxValue;

    private readonly HttpClient _client;

    public ImageService(HttpClient client)
    {
        _client = client;
    }

    public async Task AddImagesAsync(IEnumerable<IBrowserFile> files, CancellationToken cancellationToken = default)
    {
        using var content = new MultipartFormDataContent();
        foreach (var file in files)
        {
            var fileContent = new StreamContent(file.OpenReadStream(MaxFileSize, cancellationToken));
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
            content.Add(fileContent, "\"images\"", file.Name);
        }

        var response = await _client.PostAsync("/api/image/add", content, cancellationToken);

    }
}
