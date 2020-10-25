using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Programatica.Framework.Mvc.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IAuthenticationUtility _authenticationUtility;

        public AuthenticationService(
            ILogger<AuthenticationService> logger,
            IAuthenticationUtility authenticationUtility)
        {
            _logger = logger;
            _authenticationUtility = authenticationUtility;
        }

        public async Task SignIn(HttpContext httpContext, string username, string password, bool isPersistent = false)
        {
            if (_authenticationUtility.AuthByUsernameAndPassword(username, password))
            {
                var claims = _authenticationUtility.GetUserPrincipalClaims(username, password);
                ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                _logger.LogInformation($"User: {username} signin sucessfully.");
            }
            else
            {
                _logger.LogInformation($"Wrong username or password");
                throw new AuthenticationException("Wrong username or password");
            }
        }

        public async Task SignOut(HttpContext httpContext)
        {
            _logger.LogInformation($"User signout sucessfully.");
            await httpContext.SignOutAsync();
        }
    }
}
