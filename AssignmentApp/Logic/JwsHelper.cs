using AssignmentApp.Models;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace AssignmentApp.Helpers;

public static class JwsHelper
{
    public static string CreateJws(string keyId, string sharedKey, RequestModel request)
    {
        var header = new
        {
            alg = "HS256",
            kid = keyId,
            signdate = DateTime.UtcNow.ToString("o"),
            cty = "application/json",
        };

        var payload = new
        {
            request.CardInfo,
        };

        var base64Header = ConvertToBase64UrlString(header);
        var base64Payload = ConvertToBase64UrlString(payload);

        var stringToSign = string.Concat(base64Header, ".", base64Payload);

        var signature = string.Empty;
        using (var hmac = new HMACSHA256(Convert.FromBase64String(sharedKey)))
        {
            var hash = hmac.ComputeHash(Encoding.ASCII.GetBytes(stringToSign));
            signature = ConvertTobase64String(hash);
        }

        return string.Concat(base64Header, ".", base64Payload, ".", signature);
    }

    public static string DecodeJwsElement(string element)
    {
        element = element
            .Replace('-', '+')
            .Replace('_', '/');

        switch (element.Length % 4)
        {
            case 2:
                element += "==";
                break;
            case 3:
                element += "=";
                break;
        }
        var elementBytes = Convert.FromBase64String(element);
        var elementJson = Encoding.UTF8.GetString(elementBytes);

        return elementJson;
    }

    private static string ConvertToBase64UrlString(object value)
    {
        var serializedValue = JsonConvert.SerializeObject(value);
        var encodedValue = Encoding.UTF8.GetBytes(serializedValue);

        return ConvertTobase64String(encodedValue);
    }

    private static string ConvertTobase64String(byte[] encodedValue)
    {
        return Convert.ToBase64String(encodedValue)
            .Replace('+', '-')
            .Replace('/', '_')
            .TrimEnd('=');
    }

}


