using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Programatica.Framework.Mvc.Options;
using System.Linq;

namespace Programatica.Framework.Mvc.Filters
{
    public class AuthorizedRoleAttribute : TypeFilterAttribute
    {
        public AuthorizedRoleAttribute(params string[] roles)
            : base(typeof(AuthorizedRoleAttributeImpl))
        {
            Arguments = new object[] { roles };
        }

        private class AuthorizedRoleAttributeImpl : IActionFilter
        {
            private string[] _roles;
            private readonly IHttpContextAccessor _httpContextAccessor;
            private readonly ClaimBasedAuthAdapterOptions _options;

            public AuthorizedRoleAttributeImpl(string[] roles, IHttpContextAccessor httpContextAccessor, IOptions<ClaimBasedAuthAdapterOptions> options)
            {
                _roles = roles;
                _httpContextAccessor = httpContextAccessor;
                _options = options.Value;
            }

            public void OnActionExecuted(ActionExecutedContext context)
            {
            }

            public void OnActionExecuting(ActionExecutingContext context)
            {
                var identity = _httpContextAccessor
                                .HttpContext
                                .User
                                .Identity;

                if (identity.IsAuthenticated)
                {
                    var claims = _httpContextAccessor
                                    .HttpContext
                                    .User
                                    .Claims
                                    .Where(x => x.Type.Equals(_options.UserRoleFieldName))
                                    .ToList();

                    bool valid = false;
                    foreach (var role in _roles)
                    {
                        foreach (var claim in claims)
                        {
                            if (claim.Value.Equals(role))
                            {
                                valid = true;
                                break;
                            }
                        }
                    }
                    if (!valid)
                    {
                        context.Result = new ForbidResult();
                    }
                }else
                {
                    context.Result = new ChallengeResult(CookieAuthenticationDefaults.AuthenticationScheme);
                }
            }
        }

    }
}
