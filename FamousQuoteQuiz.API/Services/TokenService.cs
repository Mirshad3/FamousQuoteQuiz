using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace FamousQuoteQuiz.Api.Services;
public class TokenService
{
    private readonly string _key;
    private readonly string _issuer;
    public TokenService(string key, string issuer)
    {
        _key = key;
        _issuer = issuer;
    }

    public string CreateToken(int userId, string email, string fullName, int expireMinutes = 120)
    {
        var keyBytes = Encoding.UTF8.GetBytes(_key);
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim("name", fullName)
        };
        var creds = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(_issuer, null, claims, expires: DateTime.UtcNow.AddMinutes(expireMinutes), signingCredentials: creds);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
