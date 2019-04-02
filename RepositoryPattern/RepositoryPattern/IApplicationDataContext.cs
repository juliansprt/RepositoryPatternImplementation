using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern
{
    public interface IApplicationDataContext
    {
        DbSet<T> Set<T>() where T : class;

        void Save();

        void BeginTransaction();

        void RollBack();

        void Commit();

        void DisableValidation();

        IEnumerable<T> SqlQuery<T>(string query, params object[] parameters);

        void ExecuteNonQuery(string query, params object[] parameters);

        IApplicationDatabaseContext GetApplicationDatabaseContext();

    }
}
