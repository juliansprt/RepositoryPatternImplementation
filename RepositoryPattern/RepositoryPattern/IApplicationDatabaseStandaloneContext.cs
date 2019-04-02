using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern
{
    public interface IApplicationDatabaseStandaloneContextCreator
    {
        IApplicationDatabaseStandaloneContext Create();
    }

    public interface IApplicationDatabaseStandaloneContext : IDisposable
    {
        IEnumerable<T> SqlQuery<T>(string query, params object[] parameters);
        void ExecuteNonQuery(string query, params object[] parameters);
    }


}
