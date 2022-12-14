using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace CarRentService.Areas.User.Customs {
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ClientAuthorize : ActionFilterAttribute {
        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            if (filterContext == null) {
                throw new ArgumentNullException(nameof(filterContext));
            }

            if (!filterContext.HttpContext.User.Identity.IsAuthenticated) {
                filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                    { "controller", "Login" },
                    { "action", "UserLogin" },
                    { "url", filterContext.HttpContext.Request.Path}
                });
            } else {
                if (!filterContext.HttpContext.User.IsInRole("User")) {
                    filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        { "controller", "Login" },
                        { "action", "UserLogin" },
                        { "url", filterContext.HttpContext.Request.Path}
                    });
                }
            }
        }
    }

}
