using Sesc.CrossCutting.ServiceAgents.Clientela.Dto;
using Sesc.Domain.Habilitacao.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sesc.Application.ApplicationServices.Queries.Contracts
{
    public interface IPessoaScaQueryServiceApplication : IQueryServiceApplication
    {
        PessoaScaDto GetByCpf(string cpf);
        IList<PessoaScaDto> GetDependentesUsuarioLogado();
    }
}
