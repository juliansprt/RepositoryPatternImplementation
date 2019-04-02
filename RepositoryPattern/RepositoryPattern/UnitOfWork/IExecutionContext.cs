using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern.UnitOfWork
{
    public interface IExecutionContext : IDisposable
    {
        T GetObject<T>(string key);
        void SetObject(string key, object val);
        void RemoveObject(string key);
    }
}
