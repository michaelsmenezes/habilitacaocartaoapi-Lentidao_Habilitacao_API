using AutoMapper;
using FluentValidation.Results;
using Sesc.CrossCutting.Validation.BaseException;
using Sesc.MeuSesc.SharedKernel.Infrastructure.DataContext;
using Sesc.MeuSesc.SharedKernel.Infrastructure.Entities;
using Sesc.MeuSesc.SharedKernel.Infrastructure.Pagination;
using Sesc.MeuSesc.SharedKernel.Infrastructure.Repositories;
using Sesc.MeuSesc.SharedKernel.Infrastructure.UnitOfWork;
using Sesc.MeuSesc.SharedKernel.Infrastructure.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Sesc.MeuSesc.SharedKernel.Application.Services
{
    public class Service<TEntity, Repository> : IService<TEntity> where TEntity : EntityBase where Repository : IRepository<TEntity>
    {
        protected readonly Repository _repository;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public Service(IUnitOfWork unitOfWork, Repository repository, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public TEntity FindByKey(int id)
        {
            return _repository.FindByKey(id);
        }

        public TEntity FindByKeyAsNoTracking(int id)
        {
            return _repository.FindByKeyAsNoTracking(id);
        }


        public TEntity FindByKeyInclude(int id, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return _repository.FindByKeyInclude(id, includeProperties);
        }

        public TEntity FindByKeyIncludeAsNoTracking(int id, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return _repository.FindByKeyIncludeAsNoTracking(id, includeProperties);
        }

        public IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return _repository.GetAll(includeProperties);
        }

        public virtual void Insert(TEntity entity)
        {
            _repository.Insert(entity);
            _unitOfWork.SaveChanges();
        }

        public virtual void Update(TEntity entity)
        {
            _repository.Update(entity);
            _unitOfWork.SaveChanges();
        }

        public virtual void DeleteByKey(int id)
        {
            _repository.DeleteByKey(id);
            _unitOfWork.SaveChanges();
        }

        public virtual void Delete(TEntity entity)
        {
            _repository.Delete(entity);
            _unitOfWork.SaveChanges();
        }

        public IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            return _repository.FindBy(predicate);
        }

        public IEnumerable<TEntity> FindByInclude(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return _repository.FindByInclude(predicate, includeProperties);
        }

        public PagedResult<TEntity> Select(int page, int pageSize, Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return _repository.Select(page, pageSize, filter, orderBy, includeProperties);
        }

        public void AddContext(IDataContext dataContext)
        {
            _unitOfWork.AddContext(dataContext);
        }

        public class Valid<V, Entity> where V : Validator<Entity> where Entity : EntityBase
        {
            public static V Instace() => (V)Activator.CreateInstance(typeof(V));

            public static void Dispatch(Entity entity)
            {
                ValidationResult result = Instace().DoValidate(entity);

                if (!result.IsValid)
                {
                    ContentSingleton.AddMessage(result);
                    ContentSingleton.Dispatch();
                }
            }
        }
    }
}
