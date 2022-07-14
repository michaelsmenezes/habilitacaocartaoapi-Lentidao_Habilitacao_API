using Sesc.MeuSesc.SharedKernel.Infrastructure.DataContext;

namespace Sesc.MeuSesc.SharedKernel.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        int SaveChanges();

        void OpenTransaction();

        void Commit();

        void Rollback();

        void AddContext(IDataContext dataContext);
    }
}
