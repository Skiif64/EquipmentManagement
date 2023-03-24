namespace EquipmentManagement.UI.Models;

public class AuthentificationResult
{
    public bool IsSuccess { get; init; }
    public IDictionary<string, string>? Errors { get; init; }
    public AuthentificationResult()
    {
        IsSuccess = true;
    }
    public AuthentificationResult(IDictionary<string, string> errors)
    {
        IsSuccess = false;
        Errors = errors;
    }
}
