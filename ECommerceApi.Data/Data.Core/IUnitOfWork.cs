using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceApi.Data.Data.Core
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();

        Task AddEntityAsync<TEntity>(TEntity entity) where TEntity : class;

        IQueryable<TEntity> GetDbSetAsQueryable<TEntity>(bool ignoreQueryFilters = false) where TEntity : class;
    }
}
