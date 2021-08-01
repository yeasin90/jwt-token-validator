using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Token.Validator.Configurations;

namespace Token.Validator.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly AuthorizationServerConfig _authServerConfig;
        public ICollection<SecurityKey> SigningKeys { get; set; }

        public AuthorizationService(IOptions<AuthorizationServerConfig> authServerConfig)
        {
            _authServerConfig = authServerConfig.Value;
        }

        public JwtSecurityToken ValidateJwtToken(string jwtToken)
        {
            if (string.IsNullOrEmpty(jwtToken))
                throw new ArgumentNullException(nameof(jwtToken));

            var validationParameters = new TokenValidationParameters
            {
                RequireExpirationTime = true,
                RequireSignedTokens = true,
                ValidateIssuer = true,
                ValidIssuer = _authServerConfig.Host,
                ValidateIssuerSigningKey = true,
                IssuerSigningKeys = SigningKeys,
                IssuerSigningKey = null,
                ValidateLifetime = true,
                // Allow for some drift in server time
                // (a lower value is better; we recommend two minutes or less)
                ClockSkew = TimeSpan.FromMinutes(2),
                // See additional validation for aud below
                ValidateAudience = false
            };

            try
            {
                var principal = new JwtSecurityTokenHandler()
                            .ValidateToken(jwtToken, validationParameters, out var rawValidatedToken);

                var result = (JwtSecurityToken)rawValidatedToken;
                return result;
            }
            catch (SecurityTokenValidationException ex)
            {
                return null;
            }
        }
    }
}
