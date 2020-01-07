using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern.UnitOfWork
{
    /// <summary>
    /// Work Context Provider
    /// </summary>
    public interface IWorkContextProvider
    {
        /// <summary>
        /// Current Work Context
        /// </summary>
        WorkContext CurrentContext { get; }
    }
}
