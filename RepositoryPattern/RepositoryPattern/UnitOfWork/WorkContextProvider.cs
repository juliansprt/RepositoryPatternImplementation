using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern.UnitOfWork
{
    /// <summary>
    /// Work Context provider default implementation
    /// </summary>
    [Export]
    public class WorkContextProvider : IWorkContextProvider
    {
        protected const string ContextKey = "RepositoryPattern.UnitOfWork.Key";

        public IExecutionContext ExecutionContext { private get; set; }

        public WorkContextProvider(IExecutionContext executionContext)
        {
            this.ExecutionContext = executionContext;
        }

        public WorkContext CurrentContext
        {
            get
            {
                var context = this.GetContextFromStore();
                if(context == null)
                {
                    context = this.GetNewWorkContext();
                    this.SetContextToStore(context);
                }
                return context;
            }
        }

        protected virtual WorkContext GetNewWorkContext()
        {
            return new WorkContext();
        }

        protected virtual WorkContext GetContextFromStore()
        {
            return this.ExecutionContext.GetObject<WorkContext>(ContextKey);
        }
        protected virtual void SetContextToStore(WorkContext context)
        {
            this.ExecutionContext.SetObject(ContextKey, context);
        }
    }
}
