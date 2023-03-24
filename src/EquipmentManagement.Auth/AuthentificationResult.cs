using System.Diagnostics.CodeAnalysis;

namespace EquipmentManagement.Auth;

public class AuthentificationResult
{
    public bool IsSuccess { get; }    
    public string? Token { get; }
    public IDictionary<string, string>? Errors { get; }

    protected AuthentificationResult(string token)
    {
        IsSuccess = true;
        Token = token;
    }

    protected AuthentificationResult(IDictionary<string, string> errors)
    {
        IsSuccess = false;
        Errors = errors;
    }    

    public static AuthentificationResult CreateSuccess(string token)
        => new AuthentificationResult(token);
    public static AuthentificationResult CreateFailure(IDictionary<string, string> errors)
        => new AuthentificationResult(errors);
    public static AuthentificationResult CreateFailure(IEnumerable<KeyValuePair<string, string>> errors)
        => new AuthentificationResult(new Dictionary<string, string>(errors));
}
