using Microsoft.AspNetCore.Mvc;
using Token.Validator.Services;

namespace Token.Validator.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private readonly IAuthorizationUtility _authUtility;

        public TokenController(IAuthorizationUtility authUtility)
        {
            _authUtility = authUtility;
        }

        [Route("validate")]
        public IActionResult Validate(string jwtToken)
        {
            var result = _authUtility.ValidateJwtToken(jwtToken);
            return Ok();
        }
    }
}
