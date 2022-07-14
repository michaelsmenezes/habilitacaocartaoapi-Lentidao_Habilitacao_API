using Sesc.Domain.Habilitacao.Dto;
using System.Threading.Tasks;

namespace Sesc.Domain.Habilitacao.Services.Contracts
{
    public interface IPessoaService
    {
        //IList<ClienteScaDto> GetGrupoFamiliarReserva();

        Task<ClienteScaDto> GetByCpfAsync(string Cpf);

        bool SalvarPessoa(ClienteScaDto Pessoa);

        Task<string> GetFotoByCpf(string Cpf);
    }
}
