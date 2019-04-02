

using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using RepositoryPattern;
using System;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;

namespace WebApp.App_Start
{
    public class IoCConfig
    {
        public static void Configure()
        {
            var config = GlobalConfiguration.Configuration;
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(typeof(WebApiApplication).Assembly).OnActivated(e =>
            {
                var instance = e.Instance as IExportInitializable;
                if (instance != null)
                    instance.Start();
            });


            builder.RegisterWebApiFilterProvider(config);

            var assemblyRepositoryPattern = typeof(IRepository<>).Assembly;
            var assemblyWeb = typeof(IoCConfig).Assembly;

            Assembly[] collectionAssemblies = new Assembly[] { assemblyWeb, assemblyRepositoryPattern };

            foreach (var assemblies in collectionAssemblies)
            {
                builder.RegisterAssemblyTypes(assemblies)
                    .Where(x => x.GetCustomAttributes(true)
                        .Any(y => y is ExportAttribute && ((ExportAttribute)y).InstanceType ==
                                  InstanceType.SingleInstance))
                    .SingleInstance()
                    .AsImplementedInterfaces()
                    .OnActivated(e =>
                    {
                        var instance = e.Instance as IExportInitializable;
                        if (instance != null)
                            instance.Start();
                    });

                builder.RegisterAssemblyTypes(assemblies)
                    .Where(x => x.GetCustomAttributes(true)
                        .Any(y => y is ExportAttribute && ((ExportAttribute)y).InstanceType ==
                                  InstanceType.InstancePerRequest))
                    .InstancePerRequest()
                    .AsImplementedInterfaces()
                    .OnActivated(e =>
                    {
                        var instance = e.Instance as IExportInitializable;
                        if (instance != null)
                            instance.Start();
                    });

                builder.RegisterAssemblyTypes(assemblies)
                    .Where(x => x.GetCustomAttributes(true)
                        .Any(y => y is ExportAttribute && ((ExportAttribute)y).InstanceType ==
                                  InstanceType.InstancePerDependency))
                    .InstancePerDependency()
                    .AsImplementedInterfaces()
                    .OnActivated(e =>
                    {
                        var instance = e.Instance as IExportInitializable;
                        if (instance != null)
                            instance.Start();
                    });
            }

            IContainer container = null;
            Func<IContainer> containerProxy = () => container;
            builder.RegisterInstance(containerProxy);

            builder.RegisterGeneric(typeof(Repository<>))
                .As(typeof(IRepository<>))
                .SingleInstance();
            container = builder.Build();
            InjectStatic(container);

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        private static void InjectStatic(IContainer container)
        {
            var staticClasses = typeof(IoCConfig).Assembly.GetTypes()
                .Where(t => t.IsClass && t.IsSealed && t.IsAbstract);

            foreach (var staticClass in staticClasses)
            {
                var properties = staticClass.GetProperties(BindingFlags.Public | BindingFlags.Static);

                foreach (var p in properties)
                {
                    if (!p.CanWrite || !p.CanRead)
                        continue;

                    var mget = p.GetGetMethod(false);
                    var mset = p.GetSetMethod(false);

                    if (mget == null)
                        continue;
                    if (mset == null)
                        continue;


                    p.SetValue(null, container.Resolve(p.PropertyType));
                }
            }
        }
    }
}