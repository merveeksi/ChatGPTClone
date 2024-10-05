using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using ChatGPTClone.Application.Common.Interfaces;
using ChatGPTClone.Application.Common.Models.Jwt;
using ChatGPTClone.Domain.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ChatGPTClone.Infrastructure.Services;

public class JwtManager : IJwtService
{
    private readonly JwtSettings _jwtSettings;

    //bunu container'a kaydettiğimiz için geldi.
    public JwtManager(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    public JwtGenerateTokenResponse GenerateToken(JwtGenerateTokenRequest request)
    {
        var expirationInMinutes = _jwtSettings.AccessTokenExpiration;

        var expirationDate = DateTime.Now.AddMinutes(expirationInMinutes.Minutes);

        // Generate the token

        var claims = new List<Claim>
            {
                new Claim("uid", request.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, request.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),   //tokenin kendi Id'si
                new Claim(JwtRegisteredClaimNames.Iss, _jwtSettings.Issuer),     //bu tokeni kimin yayınladığı
                new Claim(JwtRegisteredClaimNames.Aud, _jwtSettings.Audience),      //bu tokenin kimin tarafından kullanılabileceği
                new Claim(JwtRegisteredClaimNames.Exp, expirationDate.ToFileTimeUtc().ToString()),    //tokenin ne zaman biteceği   //long olarak tutuluyor(tofiletime)
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToFileTimeUtc().ToString()),     //tokenin ne zaman başladığı
                new Claim("reklam", "En iyi akademi Reklam Academy! Just joking it's the god damn Yazilim Academy!"),
                new Claim(ClaimTypes.Role, "Admin") //bu tokenin hangi rolde olduğu
            }
            //birleştirme işlemi
            .Union(request.Roles.Select(role => new Claim(ClaimTypes.Role, role))); 

        // Generate the symmetric security key
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey)); 

        // Generate the signing credentials
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256); //şifreleme algoritması //en güvinilir olanı

        // Generate the JwtSecurityToken
        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: expirationDate,
            signingCredentials: signingCredentials
        );

        // Generate the token
        var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken); //tokeni string yazdırıyor

        return new JwtGenerateTokenResponse(token, expirationDate);
    }
    
    public Guid GetUserIdFromJwt(string token)
    {
        try
        {
            var claims = ParseClaimsFromJwt(token);
            var userId = claims.FirstOrDefault(c => c.Type == "uid")?.Value;
            if (string.IsNullOrWhiteSpace(userId))
                throw new AuthenticationException("Invalid token");
            return new Guid(userId);
        }
        catch (Exception ex)
        {
            throw new AuthenticationException("Invalid token", ex);
        }
    }
    
    //tokeni doğrulama
    public bool ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var secretKey = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);
        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                ValidateIssuer = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidateAudience = true,
                ValidAudience = _jwtSettings.Audience,
                ValidateLifetime = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);
            return true;
        }
        catch
        {
            return false;
        }
    }
    //tokeni parse etme
    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
    }
    private byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }
}