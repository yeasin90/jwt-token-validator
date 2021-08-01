# JWT token validator using JWKs
RnD project for validating JWT token from JWK key sets

### Steps: 
- Set your authorization server address in **appsettings.Development.json** _(Host property of AuthorizationServerConfig)_
- Run the project and hit : https://localhost:44347/api/token/validate?jwtToken={your_jwt_token}

### References: 
[Sample OpenId discovery document](https://demo.identityserver.io/.well-known/openid-configuration)

[JWT Token validation steps using JWKS](https://auth0.com/blog/navigating-rs256-and-jwks/)

[JWK property explanation](https://auth0.com/docs/tokens/json-web-tokens/json-web-key-set-properties)

[JWT Token signing basics using RSA (asymmetric encryption)](https://vmsdurano.com/-net-core-3-1-signing-jwt-with-rsa/)

[Example repo on **OpenIdConnectConfiguration** object population from jwk url](https://github.com/NetDevPack/Security.JwtExtensions)

[.net core stratup initialization with Labmda](https://www.thecodebuzz.com/initialize-instances-within-configservices-in-startup/)
