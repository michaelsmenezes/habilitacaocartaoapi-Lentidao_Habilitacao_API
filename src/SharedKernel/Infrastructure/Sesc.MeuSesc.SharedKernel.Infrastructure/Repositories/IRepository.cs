using Sesc.MeuSesc.SharedKernel.Infrastructure.DataContext;
using Sesc.MeuSesc.SharedKernel.Infrastructure.Entities;
using Sesc.MeuSesc.SharedKernel.Infrastructure.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Sesc.MeuSesc.SharedKernel.Infrastructure.Repositories
{
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        TEntity FindByKey(int id);

        TEntity FindByKeyInclude(int id, params Expression<Func<TEntity, object>>[] includeProperties);

        TEntity FindByKeyIncludeAsNoTracking(int id, Expression<Func<TEntity, object>>[] includeProperties);

        TEntity FindByKeyAsNoTracking(int id);

        IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TEntity> FindByInclude(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties);

        IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includeProperties);

        IEnumerable<TEntity> GetAllInclude(params Expression<Func<TEntity, object>>[] includeProperties);

        IQueryable<TEntity> AsQueryable();

        PagedResult<TEntity> GetAll(int page, int pageSize);

        PagedResult<TEntity> Select(
            int page,
            int pageSize,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity, object>>[] includeProperties);

        void Insert(TEntity entity);

        void Update(TEntity entity);

        void DeleteByKey(int id);

        void Delete(TEntity entity);

        void AddContext(IDataContext dataContext);
    }
}
