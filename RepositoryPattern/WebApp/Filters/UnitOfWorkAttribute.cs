using RepositoryPattern;
using RepositoryPattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebApp.Filters
{
    public class UnitOfWorkAttribute : ActionFilterAttribute
    {
        private const string ExecutionKey = "UnitOfWork.Execution";

        private IWorkContextProvider WorkContextProvider;
        private IExecutionContext ExecutionContext;
        private IApplicationDataContext ApplicationDataContext;

        public bool UseTransaction { get; set; }
        public UnitOfWorkAttribute(IWorkContextProvider workContextProvider, IExecutionContext executionContext, IApplicationDataContext applicationDataContext)
        {
            this.WorkContextProvider = workContextProvider;
            this.ExecutionContext = executionContext;
            this.ApplicationDataContext = applicationDataContext;
            this.UseTransaction = false;
        }

        private static string GetUowName(HttpActionDescriptor actionDescriptor)
        {
            return $"{actionDescriptor.ControllerDescriptor.ControllerName}.{actionDescriptor.ActionName}";
        }

        public override void OnActionExecuting(HttpActionContext filterContext)
        {


            var unitOfWork =
    WorkContextProvider.CurrentContext.BeginUnitOfWork(GetUowName(filterContext.ActionDescriptor),
        ApplicationDataContext.GetApplicationDatabaseContext(), this.UseTransaction);
            unitOfWork.OnEnd(() => this.ExecutionContext.RemoveObject(ExecutionKey));

            this.ExecutionContext.SetObject(ExecutionKey, unitOfWork);

            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(HttpActionExecutedContext filterContext)
        {

            base.OnActionExecuted(filterContext);
            var execution = this.ExecutionContext.GetObject<UnitOfWorkExecution>(ExecutionKey);

            if (filterContext.Exception != null)
                execution.HandleException(execution.Exception);

            execution.End();
        }
    }
}