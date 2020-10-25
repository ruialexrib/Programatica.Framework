using System.Collections.Generic;
using System.Security.Claims;

namespace Programatica.Framework.Mvc.Authentication
{
    public interface IAuthenticationUtility
    {
        IEnumerable<Claim> GetUserPrincipalClaims(string user, string password);
        IEnumerable<Claim> GetUserRoleClaims(string user);
        bool AuthByUsernameAndPassword(string username, string password);

    }
}
