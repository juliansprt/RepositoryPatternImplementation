using RepositoryPattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Dependencies;

namespace WebApp.RepositoryPatternImpl
{
    public static class DependencyResolverExtensions
    {
        private const string dependencyScopeKey = "DependencyResolver.key";
        public static IExecutionContext ExecutionContext { get; set; }

        public static IDependencyScope Current(this IDependencyResolver dependencyResolver)
        {
            return ExecutionContext.GetObject<IDependencyScope>(dependencyScopeKey);
        }

        public static void SetCurrentDependencyScope(this HttpRequestMessage request)
        {
            ExecutionContext.SetObject(dependencyScopeKey, request.GetDependencyScope());
        }
    }
}