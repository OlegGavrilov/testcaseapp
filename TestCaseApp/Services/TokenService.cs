using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace TestCaseApp.Services;

public class TokenService
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private const string TokenSubject = "TokenForTheTestCaseApp";
    private const string SecretKey = "secretsecretsecretsecretsecret";

    public TokenService(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }
    
    public string CreateToken(IdentityUser user)
    {
        var currentTime = _dateTimeProvider.Now();
        
        var expiration = currentTime.AddMinutes(30);
        
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(SecretKey)
            ),
            SecurityAlgorithms.HmacSha256
        );
        
        var claims = new List<Claim>
        {
            new (JwtRegisteredClaimNames.Sub, TokenSubject),
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new (JwtRegisteredClaimNames.Iat, currentTime.ToString(CultureInfo.InvariantCulture)),
            new (ClaimTypes.NameIdentifier, user.Id)
        };

        if (user.UserName is not null)
        {
            claims.Add(new (ClaimTypes.Name, user.UserName));
        }
        if (user.Email is not null)
        {
            claims.Add(new (ClaimTypes.Email, user.Email));
        }
        
        var token = new JwtSecurityToken("TestCaseApp",
            "TestCaseApp",
            claims,
            expires: expiration,
            signingCredentials: signingCredentials
        );
        
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }
}