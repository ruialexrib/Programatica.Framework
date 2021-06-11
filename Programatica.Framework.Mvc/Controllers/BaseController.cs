using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Programatica.Framework.Mvc.Controllers
{
    public class BaseController : Controller
    {
        public List<string> PageMessages { get; set; }
        public List<string> PageWarnings { get; set; }
        public List<string> PageAlerts { get; set; }

        private DateTime StartTime { get; set; }

        public BaseController()
        {
            PageMessages = new List<string>();
            PageWarnings = new List<string>();
            PageAlerts = new List<string>();
        }

        public override void OnActionExecuting(ActionExecutingContext ctx)
        {
            base.OnActionExecuting(ctx);
            StartTime = DateTime.Now;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);

            TempData["PageMessages"] = PageMessages;
            TempData["PageWarnings"] = PageWarnings;
            TempData["PageAlerts"] = PageAlerts;

            TempData["ControllerName"] = context.RouteData.Values["controller"].ToString();
            TempData["ControllerAction"] = context.RouteData.Values["action"].ToString();

            var mls = DateTime.Now - this.StartTime;
            TempData["ElapsedTime"] = mls.Milliseconds.ToString();
        }

    }
}
