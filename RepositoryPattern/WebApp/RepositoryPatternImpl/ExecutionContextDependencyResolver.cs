using RepositoryPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace WebApp.RepositoryPatternImpl
{
    [Export(InstanceType.SingleInstance)]
    public class ExecutionContextDependencyResolver : IExecutionContextDependencyResolver
    {
        public T Get<T>() where T : class
        {
            var config = GlobalConfiguration.Configuration;
            return (T)config.DependencyResolver.Current().GetService(typeof(T));
        }
    }
}