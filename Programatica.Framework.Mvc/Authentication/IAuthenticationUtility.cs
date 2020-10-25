using Programatica.Framework.Data.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace Programatica.Framework.Mvc.Authentication
{
    public interface IAuthenticationUtility<T>
        where T : IModel
    {
        IEnumerable<Claim> GetUserPrincipalClaims(string user, string password);
        IEnumerable<Claim> GetUserRoleClaims(string user);
        T GetByUsernameAndPassword(string username, string password);

    }
}
