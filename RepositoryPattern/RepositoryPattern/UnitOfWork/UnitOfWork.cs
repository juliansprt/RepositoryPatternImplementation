using System;

namespace RepositoryPattern.UnitOfWork
{
    /// <summary>
    /// Unit of work implementation
    /// </summary>
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

        /// <summary>
        /// Cancel a request
        /// </summary>
        public void Cancel()
        {
            this.canceled = true;
        }

        /// <summary>
        /// Begin a transaction into current Unit Work
        /// </summary>
        public void Begin()
        {
            if (this.useTransaction)
                this.applicationDatabaseContext.BeginTransaction();
            this.Status = UnitOfWorkStatus.Running;
        }

        /// <summary>
        /// Finalice a transaction
        /// </summary>
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

        /// <summary>
        /// Save changes of unit work
        /// </summary>
        public virtual void CheckPoint()
        {
            if(this.useTransaction)
            {
                this.applicationDatabaseContext.Save();
                this.applicationDatabaseContext.Commit();
                this.applicationDatabaseContext.BeginTransaction();
            }
        }

        /// <summary>
        /// Set error in current unit of work
        /// </summary>
        /// <param name="ex"></param>
        public void Fail(Exception ex)
        {
            if (this.useTransaction)
                this.applicationDatabaseContext.RollBack();
            this.Status = UnitOfWorkStatus.Failed;
        }

        /// <summary>
        /// On Sucess Callback
        /// </summary>
        /// <param name="onSuccess"></param>
        public void OnSuccess(Action onSuccess)
        {
            this.onSuccess = onSuccess;
        }
        public void Dispose()
        {
        }
    }
}
