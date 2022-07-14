using Microsoft.EntityFrameworkCore;
using Sesc.Domain.Habilitacao.Entities;
using Sesc.Domain.Habilitacao.Repositories.Contracts;
using Sesc.MeuSesc.Infrastructure.EntityFramework.Base;
using Sesc.MeuSesc.Infrastructure.EntityFramework.Context;
using Sesc.MeuSesc.SharedKernel.Infrastructure.DataContext;
using System;
using System.Linq;

namespace Sesc.MeuSesc.Infrastructure.EntityFramework.Persistence.Repositories
{
    public class AtendimentoRepository : Repository<Atendimento>, IAtendimentoRepository
    {
        public AtendimentoRepository(
            IDataContext context,
            IServiceProvider provider
        ) : base(context)
        {
            _context = context;
        }


        public Atendimento GetById(int id, bool noTrack = false)
        {
            var solicitacao = _dbSet.Select(x => x)
                .Where(s => s.Id == id);

            return noTrack == false
                ? solicitacao.FirstOrDefault()
                : solicitacao.AsNoTracking().FirstOrDefault();
        }

        public Atendimento Salvar(Atendimento Atendimento)
        {
            SescContext context = (SescContext)_context;

            context.Update(Atendimento);
            context.SaveChanges();

            return Atendimento;
        }
    }
}
