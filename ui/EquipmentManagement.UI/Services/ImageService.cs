using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Headers;
using System.Net.Http.Json;

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

    public async Task<string[]> AddImagesAsync(IEnumerable<UploadedFile> files, CancellationToken cancellationToken = default)
    {
        using var content = new MultipartFormDataContent();
        foreach (var file in files)
        {            
            content.Add(file.Content, "\"images\"", file.Name);
        }

        var response = await _client.PostAsync("/api/image/add", content, cancellationToken);
        return await response.Content.ReadFromJsonAsync<string[]>();

    }
}
