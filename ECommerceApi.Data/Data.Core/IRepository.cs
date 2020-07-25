using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ECommerceApi.DomainCore;

namespace ECommerceApi.Data.Data.Core
{
    public interface IRepository<TEntity> where TEntity : AggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }

        #region Get

        Task<IEnumerable<TEntity>> GetAllAsync(params Func<IQueryable<TEntity>, IQueryable<TEntity>>[] includes);

        Task<TEntity> GetByKeyAsync(long id, params Func<IQueryable<TEntity>, IQueryable<TEntity>>[] includes);

        Task<TEntity> GetByFilterAsync(Expression<Func<TEntity, bool>> filter = null, params Func<IQueryable<TEntity>, IQueryable<TEntity>>[] includes);

        int GetCount(Expression<Func<TEntity, bool>> filter = null);

        #endregion Get

        #region Add

        Task<TEntity> AddAndSaveAsync(TEntity entity);

        #endregion Add

        #region Modify

        Task<TEntity> ModifyAndSaveAsync(TEntity entity);

        #endregion Modify
    }
}