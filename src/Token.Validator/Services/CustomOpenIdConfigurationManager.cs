using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Token.Validator.Configurations;

namespace Token.Validator.Services
{
    public class CustomOpenIdConfigurationManager : IConfigurationManager<OpenIdConnectConfiguration>
    {
        private readonly AuthorizationServerConfig _authServerConfig;

        public CustomOpenIdConfigurationManager(IOptions<AuthorizationServerConfig> authServerConfig)
        {
            _authServerConfig = authServerConfig.Value;
        }

        public async Task<OpenIdConnectConfiguration> GetConfigurationAsync(CancellationToken cancel)
        {
            try
            {
                var configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(
                    _authServerConfig.Host + "/.well-known/openid-configuration", //example : https://demo.identityserver.io/.well-known/openid-configuration
                    new OpenIdConnectConfigurationRetriever(),
                    new HttpDocumentRetriever());

                var discoveryDocument = await configurationManager.GetConfigurationAsync();
                return discoveryDocument;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public void RequestRefresh()
        {
            throw new NotImplementedException();
        }
    }
}
