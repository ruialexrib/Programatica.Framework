using Programatica.Framework.Mvc.Models;
using System.Collections.Generic;

namespace Programatica.Framework.Mvc.Services
{
    public interface IMvcControllerDiscoveryService
    {
        IEnumerable<MvcControllerInfo> GetControllersAndActions();
    }
}
