using System.IdentityModel.Tokens.Jwt;

namespace EquipmentManagement.UI.Utils;

public class JwtTokenParser
{
    public static JwtSecurityToken? Parse(string token)
    {
        try
        {
            var handler = new JwtSecurityTokenHandler();
            var parsedToken = handler.ReadJwtToken(token);
            return parsedToken;
        }
        catch (ArgumentException)
        {
            return null;
        }
    }
}
