using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern.UnitOfWork
{
    public enum UnitOfWorkStatus
    {
        NotStarted,
        Running,
        Successfull,
        Failed,
        Canceled
    }
}
