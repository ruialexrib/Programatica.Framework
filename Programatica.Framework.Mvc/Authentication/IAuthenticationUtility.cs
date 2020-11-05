using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Programatica.Framework.Mvc.Authentication
{
    public interface IAuthenticationUtility
    {
        Task<IEnumerable<Claim>> GetUserPrincipalClaims(string user, string password);
        Task<IEnumerable<Claim>> GetUserRoleClaims(string user);
        Task<bool> AuthByUsernameAndPassword(string username, string password);

    }
}
