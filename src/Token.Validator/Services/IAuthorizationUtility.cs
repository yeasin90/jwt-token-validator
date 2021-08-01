using System.IdentityModel.Tokens.Jwt;

namespace Token.Validator.Services
{
    public interface IAuthorizationUtility
    {
        JwtSecurityToken ValidateJwtToken(string jwtToken);
    }
}
