using Sesc.Domain.Habilitacao.Entities;
using Sesc.Domain.Habilitacao.Repositories.Contracts;
using Sesc.MeuSesc.Infrastructure.EntityFramework.Base;
using Sesc.MeuSesc.Infrastructure.EntityFramework.Context;
using Sesc.MeuSesc.SharedKernel.Infrastructure.DataContext;
using System;

namespace Sesc.MeuSesc.Infrastructure.EntityFramework.Persistence.Repositories
{
    public class DependenteRepository : Repository<Dependente>, IDependenteRepository
    {
        public DependenteRepository(
            IDataContext context,
            IServiceProvider provider
        ) : base(context)
        {
            _context = context;
        }

        public void Deletar(Dependente dependente)
        {
            SescContext context = (SescContext)_context;

            context.Remove(dependente);
            context.SaveChanges();
        }

        public void Alterar(Dependente dependente)
        {
            SescContext context = (SescContext)_context;

            context.Update(dependente);
            context.SaveChanges();
        }
    }
}
