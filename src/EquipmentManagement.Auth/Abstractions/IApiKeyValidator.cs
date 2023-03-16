namespace EquipmentManagement.Auth.Abstractions;

public interface IApiKeyValidator
{
    bool IsValid(string apiKey);
}
