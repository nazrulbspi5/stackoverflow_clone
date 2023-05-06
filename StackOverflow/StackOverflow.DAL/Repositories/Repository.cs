using StackOverflow.DAL.Entities;
using System.Linq.Expressions;
using System.Data.Common;
using System.Data;
using System.Data.SqlTypes;
using System.Linq.Dynamic.Core;
using NHibernate;
using NHibernate.Linq;

namespace StackOverflow.DAL.Repositories
{
    public abstract class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {

        private readonly ISession _session;
        public Repository(ISession session)
        {
            _session = session;
        }
        public void Add(TEntity entity)
        {
            _session.SaveAsync(entity);
        }
        public TEntity Get(TKey id)
        {
            return _session.Get<TEntity>(id);
        }

        public void Update(TEntity entity)
        {
            _session.SaveOrUpdate(entity);
        }
        public IEnumerable<TEntity> GetAll()
        {
            return _session.Query<TEntity>().ToList();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _session.Query<TEntity>().Where(predicate);
        }
        public virtual async Task<(IList<TEntity> data, int total, int totalDisplay)> GetDynamicAsync(
            Expression<Func<TEntity, bool>> filter = null!, string orderBy = null!, int pageIndex = 1, int pageSize = 10)
        {
            IQueryable<TEntity> query = _session.Query<TEntity>();
            var total = query.Count();
            var totalDisplay = query.Count();

            if (filter != null)
            {
                query = query.Where(filter);
                totalDisplay = query.Count();
            }

            if (orderBy != null)
            {
                var result = await Task.Run(() => query.OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize));
                return (await result.ToListAsync(), total, totalDisplay);
            }
            else
            {
                var result = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
                return (result.ToList(), total, totalDisplay);
            }
        }

        public void Remove(TEntity entity)
        {
            _session.Delete(entity);
        }
        public void Remove(TKey id)
        {
            var entityToDelete = _session.Get<TEntity>(id);
            Remove(entityToDelete);
        }
        public void Merge(TEntity entity)
        {
            _session.Merge(entity);
        }
    }
}
