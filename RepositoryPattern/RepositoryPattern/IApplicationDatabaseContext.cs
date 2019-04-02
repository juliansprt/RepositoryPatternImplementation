using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern
{
    public interface IApplicationDatabaseContext
    {
        DbSet<T> Set<T>() where T : class;
        void Save();
        void BeginTransaction();
        void RollBack();
        void Commit();
        DbEntityEntry<T> Entry<T>(T enity) where T : class;
        void DisableValidation();
        IEnumerable<T> SqlQuery<T>(string query, params object[] parameters);
        void ExecuteNonQuery(string query, params object[] parameters);

    }
}
