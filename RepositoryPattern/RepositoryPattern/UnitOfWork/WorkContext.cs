using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern.UnitOfWork
{
    /// <summary>
    /// Execution Work process
    /// </summary>
    public class WorkContext
    {

        /// <summary>
        /// Current unit of work
        /// </summary>
        public UnitOfWork CurrentUnitOfWork { get; private set; }

        /// <summary>
        /// Indicate if unit of work is executing
        /// </summary>
        public virtual bool IsUnitOfWorkRunning => this.CurrentUnitOfWork != null &&
            this.CurrentUnitOfWork.Status == UnitOfWorkStatus.Running;


        /// <summary>
        /// Begin a new Unit of work and save into current Container
        /// </summary>
        /// <param name="key">Key to save</param>
        /// <param name="applicationDatabaseContext"></param>
        /// <param name="useTransaction"></param>
        /// <returns></returns>
        public UnitOfWorkExecution BeginUnitOfWork(string key, 
            IApplicationDatabaseContext applicationDatabaseContext,
            bool useTransaction)
        {
            var execution = new UnitOfWorkExecution(key);
            execution.OnEnd(() => this.CurrentUnitOfWork = null);
            this.CurrentUnitOfWork = execution.Begin(applicationDatabaseContext, useTransaction);
            return execution;
        }
    }
}
