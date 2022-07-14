using Sesc.Domain.Habilitacao.Entities;
using System.Threading.Tasks;

namespace Sesc.Domain.Habilitacao.Services.Contracts
{
    public interface IDependenteService
    {
        Task<Dependente> GetById(int id);
        Task Deletar(Dependente dependente);
        Task Alterar(Dependente dependente);
    }
}
