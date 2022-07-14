using Sesc.Domain.Habilitacao.Entities;
using Sesc.MeuSesc.SharedKernel.Infrastructure.Repositories;

namespace Sesc.Domain.Habilitacao.Repositories.Contracts
{
    public interface IDependenteRepository : IRepository<Dependente>
    {
        void Deletar(Dependente dependente);
        void Alterar(Dependente dependente);
    }
}
