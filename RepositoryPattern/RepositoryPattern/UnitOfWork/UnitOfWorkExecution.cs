using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern.UnitOfWork
{
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

        public Exception Exception { get; private set; }

        public UnitOfWork Begin(IApplicationDatabaseContext applicationDatabaseContext, bool useTransaction)
        {
            this.unitOfWork = new UnitOfWork(applicationDatabaseContext, useTransaction);
            this.unitOfWork.Begin();
            return this.unitOfWork;
        }

        public void OnEnd(Action onEnd)
        {
            this.onEnd += onEnd;
        }

        public void End()
        {
            if (this.success)
                this.unitOfWork.Success();

            this.unitOfWork.Dispose();
            this.onEnd();
        }

        public void HandleException(Exception ex)
        {
            this.success = false;

            this.Exception = ex is TargetInvocationException
                ? ex.GetBaseException() : ex;

            this.unitOfWork.Fail(this.Exception);
        }
    }
}
