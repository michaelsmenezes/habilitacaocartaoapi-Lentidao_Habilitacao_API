using Sesc.CrossCutting.ServiceAgents.Clientela.Dto;
using System.Collections.Generic;

namespace Sesc.CrossCutting.ServiceAgents.Clientela.Services.Contracts
{
    public interface IPessoaScaService
    {
        PessoaScaDto GetPessoa(string cpf);
        IList<PessoaScaDto> GetGrupoFamiliar(string cpf);
    }
}