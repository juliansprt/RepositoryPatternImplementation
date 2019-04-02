using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using WebApp.RepositoryPatternImpl;

namespace WebApp.Filters
{
    public class IoCScopeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            actionContext.Request.SetCurrentDependencyScope();
        }
    }
}