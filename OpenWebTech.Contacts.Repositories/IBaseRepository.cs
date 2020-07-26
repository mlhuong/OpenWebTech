using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OpenWebTech.Contacts.Repositories
{
    public interface IBaseRepository<TEntity, TKey>
    {
        Task<TEntity[]> GetList(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> Get(TKey id);

        Task Add(TEntity entity);

        Task Update(TEntity entity);

        Task<bool> Any(Expression<Func<TEntity, bool>> predicate);

        Task Delete(TEntity entity);
    }
}
