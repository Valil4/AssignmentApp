using AssignmentApp.Models;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AssignmentApp.Helpers;

public static class JwsHelper
{
    public static string CreateJws(string keyId, string secretKey, CardInfoModel cardInfo)
    {
        var payloadJson = JsonConvert.SerializeObject(cardInfo);
        var claims = new[]{
            new Claim("payload", payloadJson)
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var header = new JwtHeader(credentials)
        {
            { "kid", keyId }
        };

        var token = new JwtSecurityToken(
            header,
            new JwtPayload(claims)
        );

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }
}

