using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern
{
    public interface IRepository<tEntity> : IQueryable<tEntity>
        where tEntity : class
    {
        tEntity this[int id] { get; }

        List<tEntity> GetAll();

        tEntity Get(int id);

        void Add(tEntity entity);

        void Remove(tEntity entity);

        void Remove(int id);

        int Count();
    }
}
