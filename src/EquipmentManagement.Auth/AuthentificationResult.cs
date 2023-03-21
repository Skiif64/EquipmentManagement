using System.Diagnostics.CodeAnalysis;

namespace EquipmentManagement.Auth;

public class AuthentificationResult
{
    public bool Success { get; }    
    public string? Token { get; }

    public AuthentificationResult(string token)
    {
        Success = true;
        Token = token;
    }

    public AuthentificationResult()
    {
        Success = false;
    }
}
