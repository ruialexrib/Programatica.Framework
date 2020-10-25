using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Programatica.Framework.Core.Adapter;
using Programatica.Framework.Data.Models;
using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Programatica.Framework.Mvc.Authentication
{
    public class AuthenticationService<T> : IAuthenticationService
        where T : IModel
    {
        private readonly ILogger<AuthenticationService<T>> _logger;
        private readonly IAuthenticationUtility<T> _authenticationUtility;

        public AuthenticationService(
            ILogger<AuthenticationService<T>> logger,
            IAuthenticationUtility<T> authenticationUtility)
        {
            _logger = logger;
            _authenticationUtility = authenticationUtility;
        }

        public async Task SignIn(HttpContext httpContext, string username, string password, bool isPersistent = false)
        {
            if (_authenticationUtility.GetByUsernameAndPassword(username, password) != null)
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
