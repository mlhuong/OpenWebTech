using Microsoft.EntityFrameworkCore;
using OpenWebTech.Contacts.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OpenWebTech.Contacts.Repositories
{
    public abstract class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> 
        where TEntity : class
    {
        protected readonly ContactsDbContext ContactsDbContext;

        public BaseRepository(ContactsDbContext contactsDbContext)
        {
            this.ContactsDbContext = contactsDbContext;
        }

        public async Task Add(TEntity entity)
        {
            await this.ContactsDbContext.AddAsync<TEntity>(entity);
            await this.ContactsDbContext.SaveChangesAsync();
        }

        public Task<TEntity[]> GetList(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> query = this.ContactsDbContext.Set<TEntity>().AsQueryable();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return query.ToArrayAsync();
        }

        public Task Update(TEntity entity)
            => this.ContactsDbContext.SaveChangesAsync();
        

        public Task<TEntity> Get(TKey id)
            => this.ContactsDbContext.FindAsync<TEntity>(id).AsTask();

        public Task<bool> Any(Expression<Func<TEntity, bool>> predicate)
            => this.ContactsDbContext.Set<TEntity>().AnyAsync(predicate);

        public async Task Delete(TEntity entity)
        {
            this.ContactsDbContext.Set<TEntity>().Remove(entity);
            await this.ContactsDbContext.SaveChangesAsync();
        }
    }
}
