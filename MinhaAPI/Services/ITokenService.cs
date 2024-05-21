using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MinhaAPI.Services;

public interface ITokenService
{
    JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims, IConfiguration _config);

    string GenerateRefreshToken();

    ClaimsPrincipal GetPrincipalFromExpireToken(string token, IConfiguration _config);
}
