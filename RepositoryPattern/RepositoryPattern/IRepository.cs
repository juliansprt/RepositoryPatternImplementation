using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern
{
    /// <summary>
    /// Repository
    /// </summary>
    /// <typeparam name="tEntity"></typeparam>
    public interface IRepository<tEntity> : IQueryable<tEntity>
        where tEntity : class
    {
        /// <summary>
        /// Entity by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        tEntity this[int id] { get; }

        /// <summary>
        /// Get all entity
        /// </summary>
        /// <returns></returns>
        List<tEntity> GetAll();

        /// <summary>
        /// Get by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        tEntity Get(int id);

        /// <summary>
        /// Add new Entity
        /// </summary>
        /// <param name="entity"></param>
        void Add(tEntity entity);

        /// <summary>
        /// Remove entity
        /// </summary>
        /// <param name="entity"></param>
        void Remove(tEntity entity);

        /// <summary>
        /// Remove entity by Id
        /// </summary>
        /// <param name="id"></param>
        void Remove(int id);

        /// <summary>
        /// Get count of entity
        /// </summary>
        /// <returns></returns>
        int Count();
    }
}
