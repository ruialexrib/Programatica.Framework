using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Programatica.Framework.Core.Adapter;
using Programatica.Framework.Mvc.Options;
using System;
using System.Linq;

namespace Programatica.Framework.Mvc.Adapters
{
    public class ClaimBasedAuthAdapter : IAuthUserAdapter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ClaimBasedAuthAdapterOptions _options;

        public string Name
        {
            get
            {
                return _httpContextAccessor
                        .HttpContext
                        .User
                        .Claims
                        .FirstOrDefault(x => x.Type.Equals(_options.UserNameFieldName))
                        .Value;
            }
        }
        public string Password
        {
            get
            {
                return _httpContextAccessor
                        .HttpContext.User
                        .Claims
                        .FirstOrDefault(x => x.Type.Equals(_options.PasswordFieldName))
                        .Value;
            }
        }
        public string AuthenticationType
        {
            get
            {
                return _httpContextAccessor
                        .HttpContext
                        .User
                        .Identity
                        .AuthenticationType;
            }
        }
        public DateTime LastLoginDateTime
        {
            get
            {
                return DateTime.Parse(  
                                _httpContextAccessor
                                .HttpContext
                                .User
                                .Claims
                                .FirstOrDefault(x => x.Type.Equals(_options.LastLoginDateTimeFieldName))
                                .Value
                                );
            }
        }

        public ClaimBasedAuthAdapter(
            IOptions<ClaimBasedAuthAdapterOptions> options,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _options = options.Value;
        }
    }
}
