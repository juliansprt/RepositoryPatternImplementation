using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern.UnitOfWork
{
    /// <summary>
    /// Unit of work execution process
    /// </summary>
    public class UnitOfWorkExecution
    {
        private string key;
        private Action onEnd = () => { };
        private bool success = true;
        private UnitOfWork unitOfWork;

        public UnitOfWorkExecution(string key)
        {
            this.key = key;
        }

        /// <summary>
        /// Current exception
        /// </summary>
        public Exception Exception { get; private set; }

        /// <summary>
        /// Begin a new Unit of work
        /// </summary>
        /// <param name="applicationDatabaseContext"></param>
        /// <param name="useTransaction"></param>
        /// <returns></returns>
        public UnitOfWork Begin(IApplicationDatabaseContext applicationDatabaseContext, bool useTransaction)
        {
            this.unitOfWork = new UnitOfWork(applicationDatabaseContext, useTransaction);
            this.unitOfWork.Begin();
            return this.unitOfWork;
        }

        /// <summary>
        /// Callbac on end unit of work
        /// </summary>
        /// <param name="onEnd"></param>
        public void OnEnd(Action onEnd)
        {
            this.onEnd += onEnd;
        }

        /// <summary>
        /// Finalice Unit Of Work
        /// </summary>
        public void End()
        {
            if (this.success)
                this.unitOfWork.Success();

            this.unitOfWork.Dispose();
            this.onEnd();
        }


        /// <summary>
        /// Capture the current exception
        /// </summary>
        /// <param name="ex"></param>
        public void HandleException(Exception ex)
        {
            this.success = false;

            this.Exception = ex is TargetInvocationException
                ? ex.GetBaseException() : ex;

            this.unitOfWork.Fail(this.Exception);
        }
    }
}
