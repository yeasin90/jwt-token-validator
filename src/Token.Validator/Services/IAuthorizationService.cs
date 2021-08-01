using System.IdentityModel.Tokens.Jwt;

namespace Token.Validator.Services
{
    public interface IAuthorizationService
    {
        JwtSecurityToken ValidateJwtToken(string jwtToken);
    }
}
