using EquipmentManagement.Auth.Abstractions;
using Microsoft.Extensions.Configuration;

namespace EquipmentManagement.Auth;

public class ApiKeyValidator : IApiKeyValidator
{
    private readonly IConfiguration _config;

    public ApiKeyValidator(IConfiguration config)
    {
        _config = config;
    }

    public bool IsValid(string apiKey)
    {
        string key = _config.GetSection("ApiKey").Value;

        return key == apiKey;
    }
}
