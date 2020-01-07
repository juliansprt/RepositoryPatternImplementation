using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern.UnitOfWork
{
    /// <summary>
    /// Contains every objects in Containers
    /// </summary>
    public interface IExecutionContext : IDisposable
    {
        /// <summary>
        /// Get object by Key
        /// </summary>
        /// <typeparam name="T">Type Object</typeparam>
        /// <param name="key">Key with which it was kept</param>
        /// <returns></returns>
        T GetObject<T>(string key);

        /// <summary>
        /// Set or save into container
        /// </summary>
        /// <param name="key">Key to save</param>
        /// <param name="val">Object to saved</param>
        void SetObject(string key, object val);

        /// <summary>
        /// Remove object in Container
        /// </summary>
        /// <param name="key"></param>
        void RemoveObject(string key);
    }
}
