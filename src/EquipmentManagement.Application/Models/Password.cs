using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;

namespace EquipmentManagement.Application.Models;
public class Password : IEquatable<string>, IEquatable<Password>
{
    private const int PasswordLength = 48;
    private readonly byte[] _password = Array.Empty<byte>();
    private readonly byte[] _salt = Array.Empty<byte>();    
    public string PasswordHash { get; }
    public string PasswordSalt { get; }

    private Password(byte[] password, byte[] salt)
    {
        _password = password;
        _salt = salt;
        PasswordHash = Convert.ToBase64String(_password);
        PasswordSalt = Convert.ToBase64String(_salt);
    }

#pragma warning disable CS8618
    protected Password()
    {
        
    }
#pragma warning restore CS8618

    public bool Equals(string? other)
    {
        if(other is null) 
            return false;
        var otherHash = GetBase64Hash(other, _salt);
        return PasswordHash.Equals(otherHash);
    }

    public static Password Create(string password)
    {
        var salt = GenerateSalt();
        var passwordHash = GetHash(password, salt);
        return new Password(passwordHash, salt);
    }

    public bool Equals(Password? other)
    {
        if (other is null)
            return false;
        return PasswordHash.Equals(other.PasswordHash);
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if(obj is Password other)
            return Equals(other);
        if (obj is string otherPassword)
            return Equals(otherPassword);

        return false;
    }

    public override int GetHashCode()
    {
        return PasswordHash.GetHashCode();
    }

    private static byte[] GetHash(string password, byte[] salt)
    {
        using var rfc2898 = new Rfc2898DeriveBytes(password, salt);
        return rfc2898.GetBytes(PasswordLength);
    }

    private static string GetBase64Hash(string password, byte[] salt)
        => Convert.ToBase64String(GetHash(password, salt));

    private static byte[] GenerateSalt() 
        => RandomNumberGenerator.GetBytes(16);
    
}
