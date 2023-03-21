using System.Security.Claims;

namespace EquipmentManagement.UI.Authentification;

public class UserCredentialStorage
{
    private static readonly List<ClaimsIdentity> _claims = new();

    public void SetClaims(IEnumerable<ClaimsIdentity> claims)
    {
        _claims.AddRange(claims);
    }

    public IEnumerable<ClaimsIdentity> GetClaims()
    {
        return _claims;
    }
}
