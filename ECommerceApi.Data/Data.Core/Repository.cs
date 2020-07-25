using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Autofac.Features.AttributeFilters;
using ECommerceApi.DomainCore;

namespace ECommerceApi.Data.Data.Core
{
    public class Repository<TEntity> : IRepository<TEntity>
       where TEntity : AggregateRoot
    {
        public IUnitOfWork UnitOfWork => _unitOfWork;
        protected readonly IUnitOfWork _unitOfWork;

        public Repository([KeyFilter("Context")]IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Get

        public IEnumerable<TEntity> GetAll(params Func<IQueryable<TEntity>, IQueryable<TEntity>>[] includes)
        {
            return IncludeQuery(_unitOfWork.GetDbSetAsQueryable<TEntity>(), includes).ToList();
        }

        public TEntity GetByKey(long id, params Func<IQueryable<TEntity>, IQueryable<TEntity>>[] includes)
        {
             return LocalGetByKey(id, false, includes).FirstOrDefault();
        }

        private IQueryable<TEntity> LocalGetByKey(long id, bool ignoreQueryFilters, params Func<IQueryable<TEntity>, IQueryable<TEntity>>[] includes)
        {
            var dbSet = _unitOfWork.GetDbSetAsQueryable<TEntity>(ignoreQueryFilters);
            return IncludeQuery(dbSet.Where(c => c.Id.Equals(id)), includes);
        }

        public async Task<TEntity> GetByFilterAsync(Expression<Func<TEntity, bool>> filter = null, params Func<IQueryable<TEntity>, IQueryable<TEntity>>[] includes)
        {
            IQueryable<TEntity> query = filter == null ? _unitOfWork.GetDbSetAsQueryable<TEntity>() : _unitOfWork.GetDbSetAsQueryable<TEntity>().Where(filter);
            return await Task.Run(() => IncludeQuery(query, includes).FirstOrDefault());
        }

        public int GetCount(Expression<Func<TEntity, bool>> filter = null)
        {
            return _unitOfWork.GetDbSetAsQueryable<TEntity>().Count(filter);
        }

        private IQueryable<TEntity> IncludeQuery(IQueryable<TEntity> query, params Func<IQueryable<TEntity>, IQueryable<TEntity>>[] includes)
        {
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = include(query);
                }
            }

            return query;
        }
        #endregion Get

        #region Save

        private async Task SaveAsync()
        {
            await Task.Run(() => _unitOfWork.Commit());
        }

        #endregion Save

        #region Add

        public async Task<TEntity> AddAndSaveAsync(TEntity entity)
        {
            await _unitOfWork.AddEntityAsync(entity);
            await SaveAsync();
            return entity;
        }

        #endregion Add

        #region Modify
        public async Task<TEntity> ModifyAndSaveAsync(TEntity entity)
        {
            await SaveAsync();
            return entity;
        }

        #endregion Modify
    }
}