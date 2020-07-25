using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceApi.Data.Data.Core
{
    public class BaseContext<TContext> : DbContext, IUnitOfWork
        where TContext : DbContext
    {
        public BaseContext(DbContextOptions<TContext> options) : base(options)
        {
        }

        public BaseContext()
        {
        }

        public void Commit()
        {
            try
            {
                base.SaveChanges();
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw dbUpdateException;
            }
        }

        public virtual DbSet<TEntity> CreateSet<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public IQueryable<TEntity> GetDbSetAsQueryable<TEntity>(bool ignoreQueryFilters = false) where TEntity : class
        {
            if (ignoreQueryFilters)
            {
                return CreateSet<TEntity>().AsQueryable().IgnoreQueryFilters();
            }
            else
            {
                return CreateSet<TEntity>().AsQueryable();
            }
        }

        public async Task AddEntityAsync<TEntity>(TEntity entity) where TEntity : class
        {
            await base.AddAsync(entity);
        }
    }
}