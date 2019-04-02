using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern
{
    [Export(InstanceType.SingleInstance)]
    public class ApplicationDataContext : IApplicationDataContext
    {
        public IExecutionContextDependencyResolver ExecutionContextDependencyResolver { get; set; }

        protected IApplicationDatabaseContext ApplicationDatabaseContext => this.ExecutionContextDependencyResolver
            .Get<IApplicationDatabaseContext>();

        public ApplicationDataContext(IExecutionContextDependencyResolver executionContextDependencyResolver)
        {
            this.ExecutionContextDependencyResolver = executionContextDependencyResolver;
        }
        public void BeginTransaction()
        {
            this.ApplicationDatabaseContext.BeginTransaction();
        }

        public void Commit()
        {
            this.ApplicationDatabaseContext.Commit();
        }

        public void DisableValidation()
        {
            this.ApplicationDatabaseContext.DisableValidation();
        }

        public void ExecuteNonQuery(string query, params object[] parameters)
        {
            this.ApplicationDatabaseContext.ExecuteNonQuery(query, parameters);
        }

        public void RollBack()
        {
            this.ApplicationDatabaseContext.RollBack();
        }

        public void Save()
        {
            this.ApplicationDatabaseContext.Save();
        }

        public DbSet<T> Set<T>() where T : class
        {
            return this.ApplicationDatabaseContext.Set<T>();
        }

        public IEnumerable<T> SqlQuery<T>(string query, params object[] parameters)
        {
            return this.ApplicationDatabaseContext.SqlQuery<T>(query, parameters);
        }

        public IApplicationDatabaseContext GetApplicationDatabaseContext()
        {
            return ApplicationDatabaseContext;
        }
    }
}
