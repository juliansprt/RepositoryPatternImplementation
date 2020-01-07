using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern.UnitOfWork
{
    /// <summary>
    /// Unit of work status
    /// </summary>
    public enum UnitOfWorkStatus
    {
        NotStarted,
        Running,
        Successfull,
        Failed,
        Canceled
    }
}
