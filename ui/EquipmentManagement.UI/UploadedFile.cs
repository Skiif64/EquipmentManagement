using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Headers;

namespace EquipmentManagement.UI;

public class UploadedFile
{
    private const long MaxFileSize = int.MaxValue;
    public string Name { get; set; }
    public string ContentType { get; }
    public StreamContent Content { get; set; }
    public UploadedFile(string name, string contentType, Stream content)
    {
        Name = name;
        ContentType = contentType;
        Content = new StreamContent(content);
        Content.Headers.ContentType = new MediaTypeHeaderValue(ContentType);        
    }

    public UploadedFile(IBrowserFile file)
        : this(file.Name, file.ContentType, file.OpenReadStream(MaxFileSize))
    {
        
    }
}
