using Sesc.Domain.Habilitacao.Entities;
using Sesc.MeuSesc.SharedKernel.Infrastructure.Repositories;

namespace Sesc.Domain.Habilitacao.Repositories.Contracts
{
    public interface IAtendimentoRepository : IRepository<Atendimento>
    {
        Atendimento GetById(int id, bool noTrack = false);
        Atendimento Salvar(Atendimento Atendimento);
    }
}
