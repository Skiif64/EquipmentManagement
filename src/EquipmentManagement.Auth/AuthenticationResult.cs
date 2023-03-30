using System.Diagnostics.CodeAnalysis;

namespace EquipmentManagement.Auth;

public class AuthenticationResult
{
    public bool IsSuccess { get; }    
    public string? Token { get; }
    public string? RefreshToken { get; }
    public IDictionary<string, string>? Errors { get; }

    protected AuthenticationResult(string token)
    {
        IsSuccess = true;
        Token = token;
    }
    protected AuthenticationResult(string token, string refreshToken)
    {
        IsSuccess = true;
        Token = token;
        RefreshToken = refreshToken;
    }

    protected AuthenticationResult(IDictionary<string, string> errors)
    {
        IsSuccess = false;
        Errors = errors;
    }    

    public static AuthenticationResult CreateSuccess(string token)
        => new AuthenticationResult(token);
    public static AuthenticationResult CreateSuccess(string token, string refreshToken)
        => new AuthenticationResult(token, refreshToken);
    public static AuthenticationResult CreateFailure(IDictionary<string, string> errors)
        => new AuthenticationResult(errors);
    public static AuthenticationResult CreateFailure(IEnumerable<KeyValuePair<string, string>> errors)
        => new AuthenticationResult(new Dictionary<string, string>(errors));
}
