using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Programatica.Framework.Mvc.Filters
{
    // usage [AuthorizedAction(permission: "Teste")]

    public class AuthorizedActionAttribute : TypeFilterAttribute
    {
        public AuthorizedActionAttribute(params string[] permission)
            : base(typeof(AuthorizedActionAttributeImpl))
        {
            Arguments = new object[] { permission };
        }

        private class AuthorizedActionAttributeImpl : IActionFilter
        {
            private string _permission;

            public AuthorizedActionAttributeImpl(string[] permission)
            {
                _permission = permission[0];
            }

            public void OnActionExecuted(ActionExecutedContext context)
            {
            }

            public void OnActionExecuting(ActionExecutingContext context)
            {
                if (string.IsNullOrEmpty(_permission))
                {
                    var controllerName = context.RouteData.Values["controller"];
                    var actionName = context.RouteData.Values["action"];
                    _permission = "/" + controllerName + "/" + actionName;
                }

                // check if permission is in the permissionsList of the user (if at leat one userRole has this permission) 

                bool valid = true;
                if (!valid) context.Result = new BadRequestObjectResult("Invalid!");

            }
        }

    }
}
