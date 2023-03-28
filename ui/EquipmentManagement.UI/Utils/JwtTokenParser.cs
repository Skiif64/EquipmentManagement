﻿using System.IdentityModel.Tokens.Jwt;

namespace EquipmentManagement.UI.Utils;

public class JwtTokenParser
{
    public static JwtSecurityToken Parse(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var parsedToken = handler.ReadJwtToken(token);
        return parsedToken;
    }
}