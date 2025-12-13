using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BananaProtocol.Application.Common.Interfaces;
using BananaProtocol.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BananaProtocol.Infrastructure.Services;

public class TokenProvider(IOptions<JwtOptions> options) : ITokenProvider
{
    private readonly JwtOptions _jwtOptions = options.Value;

    public string GenerateToken(object userId, string email, IEnumerable<string>? roles = null)
    {
        ArgumentNullException.ThrowIfNull(userId, nameof(userId));

        List<Claim> claims =
        [
            new(JwtRegisteredClaimNames.Sub, userId.ToString()!),
            new(JwtRegisteredClaimNames.Email, email)
        ];

        if (roles is not null)
        {
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SigningKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expires = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpirationMinutes);

        var token = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            expires: expires,
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}