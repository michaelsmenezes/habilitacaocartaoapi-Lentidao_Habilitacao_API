using Sesc.CrossCutting.ServiceAgents.Clientela.Dto;

namespace Sesc.CrossCutting.ServiceAgents.Clientela.Services.Contracts
{
    public interface IEnderecoScaService
    {
        EnderecoScaDto GetEndereco(string cpf);
    }
}