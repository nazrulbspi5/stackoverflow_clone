using StackOverflow.DAL.Entities;
using System.Linq.Expressions;

namespace StackOverflow.DAL.Repositories
{
    public interface IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        void Add(TEntity entity);
        TEntity Get(TKey id);
        void Update(TEntity entity);  
        IEnumerable<TEntity> FindAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        Task<(IList<TEntity> data, int total, int totalDisplay)> GetDynamicAsync(
            Expression<Func<TEntity, bool>> filter = null!, string orderBy = null!, int pageIndex = 1, int pageSize = 10);
       
        void Remove(TEntity entity);
        void Remove(TKey id);
        void Merge(TEntity entity);
    }
}
