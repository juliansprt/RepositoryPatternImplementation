using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern
{
    /// <summary>
    /// Application data context default implementation
    /// </summary>
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

        /// <summary>
        /// Begins a new transaction
        /// </summary>
        public void BeginTransaction()
        {
            this.ApplicationDatabaseContext.BeginTransaction();
        }

        /// <summary>
        /// Accepts all changes
        /// </summary>
        public void Commit()
        {
            this.ApplicationDatabaseContext.Commit();
        }

        /// <summary>
        /// Disable validation for entity framewrok
        /// </summary>
        public void DisableValidation()
        {
            this.ApplicationDatabaseContext.DisableValidation();
        }

        /// <summary>
        /// Execute a SQL sentence
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        public void ExecuteNonQuery(string query, params object[] parameters)
        {
            this.ApplicationDatabaseContext.ExecuteNonQuery(query, parameters);
        }


        /// <summary>
        /// Rollback request
        /// </summary>
        public void RollBack()
        {
            this.ApplicationDatabaseContext.RollBack();
        }

        /// <summary>
        /// Save data
        /// </summary>
        public void Save()
        {
            this.ApplicationDatabaseContext.Save();
        }

        /// <summary>
        /// DbSet implementation
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public DbSet<T> Set<T>() where T : class
        {
            return this.ApplicationDatabaseContext.Set<T>();
        }

        /// <summary>
        /// Execute SQL to object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IEnumerable<T> SqlQuery<T>(string query, params object[] parameters)
        {
            return this.ApplicationDatabaseContext.SqlQuery<T>(query, parameters);
        }

        /// <summary>
        /// Get curren application database context
        /// </summary>
        /// <returns></returns>
        public IApplicationDatabaseContext GetApplicationDatabaseContext()
        {
            return ApplicationDatabaseContext;
        }
    }
}
