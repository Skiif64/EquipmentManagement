using EquipmentManagement.Domain.Models.Base;
using System.Security.Cryptography;

namespace EquipmentManagement.Application.Models;

public class ApplicationUser : BaseModel
{
    private const int PasswordHashLength = 48;
    public string Login { get; set; } = string.Empty;    
    public string PasswordHash { get; set; }
    public string PasswordSalt { get; set; }
    public string Role { get; set; } = string.Empty;
    public Guid? RefreshToken { get; set; }
    public bool IsBlocked { get; set; }

    public ApplicationUser() : base()
    {
        
    }

    public static ApplicationUser Create(string login, string password, string role)
    {
        var salt = GenerateSalt();
        var passwordHash = GetBase64Hash(password, salt);
        return new ApplicationUser
        {
            Login = login,
            PasswordHash = passwordHash,
            PasswordSalt = Convert.ToBase64String(salt),
            Role = role
        };
    }

    public bool PasswordEquals(string? password)
    {
        if (password is null)
            return false;
        var passwordHash = GetBase64Hash(password, Convert.FromBase64String(PasswordSalt));
        return PasswordHash.Equals(passwordHash);
    }

    private static byte[] GetHash(string password, byte[] salt)
    {
        using var rfc2898 = new Rfc2898DeriveBytes(password, salt);
        return rfc2898.GetBytes(PasswordHashLength);
    }

    private static string GetBase64Hash(string password, byte[] salt)
        => Convert.ToBase64String(GetHash(password, salt));

    private static byte[] GenerateSalt()
        => RandomNumberGenerator.GetBytes(16);
}
