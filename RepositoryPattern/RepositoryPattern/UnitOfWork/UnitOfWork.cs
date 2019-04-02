using System;

namespace RepositoryPattern.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        private readonly IApplicationDatabaseContext applicationDatabaseContext;
        private readonly bool useTransaction;
        private bool canceled;
        private Action onSuccess;
        

        public UnitOfWork(IApplicationDatabaseContext applicationDatabaseContext, bool useTransaction)
        {
            this.applicationDatabaseContext = applicationDatabaseContext;
            this.useTransaction = useTransaction;
        }

        public UnitOfWorkStatus Status { get; private set; } = UnitOfWorkStatus.NotStarted;

        public void Cancel()
        {
            this.canceled = true;
        }

        public void Begin()
        {
            if (this.useTransaction)
                this.applicationDatabaseContext.BeginTransaction();
            this.Status = UnitOfWorkStatus.Running;
        }

        public void Success()
        {
            if(this.canceled)
            {
                if (this.useTransaction)
                    this.applicationDatabaseContext.RollBack();
                this.Status = UnitOfWorkStatus.Canceled;
            }
            else
            {
                if(this.useTransaction)
                {
                    this.applicationDatabaseContext.Save();
                    this.applicationDatabaseContext.Commit();
                }
                this.Status = UnitOfWorkStatus.Successfull;
                this.onSuccess?.Invoke();
            }

        }

        public virtual void CheckPoint()
        {
            if(this.useTransaction)
            {
                this.applicationDatabaseContext.Save();
                this.applicationDatabaseContext.Commit();
                this.applicationDatabaseContext.BeginTransaction();
            }
        }

        public void Fail(Exception ex)
        {
            if (this.useTransaction)
                this.applicationDatabaseContext.RollBack();
            this.Status = UnitOfWorkStatus.Failed;
        }

        public void OnSuccess(Action onSuccess)
        {
            this.onSuccess = onSuccess;
        }
        public void Dispose()
        {
        }
    }
}
