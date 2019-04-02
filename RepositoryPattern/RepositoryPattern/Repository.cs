using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern
{
    public class Repository<tEntity> : IRepository<tEntity>, IExportInitializable
        where tEntity : class, new()
    {

        public IApplicationDataContext ApplicationDataContext { get; set; }

        protected DbSet<tEntity> DbSet => this.ApplicationDataContext.Set<tEntity>();

        public Repository(IApplicationDataContext applicationDatabaseContext)
        {
            this.ApplicationDataContext = applicationDatabaseContext;
        }

        public tEntity this[int id] => this.Get(id);

        public Expression Expression => this.GetQueryable().Expression;

        public Type ElementType => typeof(tEntity);

        public IQueryProvider Provider => this.GetQueryable().Provider;

        public void Add(tEntity entity)
        {
            this.DbSet.Add(entity);
        }

        public int Count()
        {
            return this.DbSet.Count();
        }

        public tEntity Get(int id)
        {
            return this.DbSet.Find(id);
        }

        public List<tEntity> GetAll()
        {
            return this.DbSet.ToList();
        }

        public IEnumerator<tEntity> GetEnumerator()
        {
            return this.GetQueryable().GetEnumerator();
        }

        public void Remove(tEntity entity)
        {
            var deleteable = entity as ILogicDeleteable;
            if(deleteable != null)
            {
                deleteable.DeletedDate = DateTime.Now;
                this.ApplicationDataContext.DisableValidation();
            }
            else
            {
                this.DbSet.Remove(entity);
            }
        }

        public void Remove(int id)
        {
            if(typeof(ILogicDeleteable).IsAssignableFrom(typeof(tEntity)))
            {
                var deleteable = this.Get(id) as ILogicDeleteable;
                if(deleteable != null)
                {
                    deleteable.DeletedDate = DateTime.Now;
                    this.ApplicationDataContext.DisableValidation();
                }
                else
                {
                    this.DbSet.Remove(this.Get(id));
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        protected IQueryable<tEntity> GetQueryable()
        {
            return this.DbSet;
        }

        public void Start()
        {
        }
    }
}
