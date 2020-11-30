using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Programatica.Framework.Mvc.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Programatica.Framework.Mvc.Services
{
    public class MvcControllerDiscoveryService : IMvcControllerDiscoveryService
    {
        private List<MvcControllerInfo> _mvcControllers;
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;

        public MvcControllerDiscoveryService(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
        }

        public IEnumerable<MvcControllerInfo> GetControllersAndActions()
        {
            if (_mvcControllers != null)
                return _mvcControllers;

            _mvcControllers = new List<MvcControllerInfo>();

            var items = _actionDescriptorCollectionProvider
                .ActionDescriptors.Items
                .Where(descriptor => descriptor.GetType() == typeof(ControllerActionDescriptor))
                .Select(descriptor => (ControllerActionDescriptor)descriptor)
                .GroupBy(descriptor => descriptor.ControllerTypeInfo.FullName)
                .ToList();

            foreach (var actionDescriptors in items)
            {
                if (!actionDescriptors.Any())
                    continue;

                var actionDescriptor = actionDescriptors.First();
                var controllerTypeInfo = actionDescriptor.ControllerTypeInfo;
                var currentController = new MvcControllerInfo
                {
                    DisplayName = controllerTypeInfo.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName,
                    Name = actionDescriptor.ControllerName,
                    CustomAttributes = controllerTypeInfo.CustomAttributes
                };

                var actions = new List<MvcActionInfo>();

                foreach (var descriptor in actionDescriptors.GroupBy(a => a.ActionName).Select(g => g.First()))
                {
                    var methodInfo = descriptor.MethodInfo;
                    actions.Add(new MvcActionInfo
                    {
                        ControllerId = currentController.Id,
                        Name = descriptor.ActionName,
                        DisplayName = methodInfo.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName,
                        CustomAttributes = methodInfo.CustomAttributes
                    });
                }

                currentController.Actions = actions;
                _mvcControllers.Add(currentController);
            }

            return _mvcControllers;
        }
    }
}

//example: actions by customattribute

//var secureActions = _mvcControllerDiscoveryService
//.GetControllersAndActions()
//.Select(s => new MvcControllerInfo
//{
//    Actions = s.Actions
//               .Where(x => x.CustomAttributes
//                            .Any(z => z.AttributeType == typeof(AuthorizedActionAttribute))
//                ),
//    DisplayName = s.DisplayName,
//    Name = s.Name
//})
//.Where(x => x.Actions.Count() > 0);