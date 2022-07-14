using Microsoft.EntityFrameworkCore.Storage;
using Sesc.MeuSesc.SharedKernel.Infrastructure.DataContext;
using Sesc.MeuSesc.SharedKernel.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sesc.MeuSesc.Infrastructure.EntityFramework.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        protected IDataContext _context;

        protected IDbContextTransaction _transaction;
        public UnitOfWork(IDataContext context)
        {
            _context = context;
        }

        public virtual void Commit()
        {
            throw new NotImplementedException();
        }

        public virtual void Rollback()
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void AddContext(IDataContext dataContext)
        {
            _context = dataContext;
        }

        public virtual void OpenTransaction()
        {
            throw new NotImplementedException();
        }
    }
}
