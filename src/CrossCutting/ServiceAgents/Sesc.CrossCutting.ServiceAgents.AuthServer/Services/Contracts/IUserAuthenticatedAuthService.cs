using Sesc.CrossCutting.ServiceAgents.AuthServer.Dto;
using Sesc.SharedKernel.Authentication.Services.Base.Contracts;

namespace Sesc.CrossCutting.ServiceAgents.AuthServer.Services.Contracts
{
    public interface IUserAuthenticatedAuthService : IBaseUserAuthenticatedAuthService
    {
        //PessoaFisicaDto GetPessoaFisicaAuthenticated();
        bool HasAuthorization(string cpf);
        string GetCpfByToken(string accessToken);
        string GetMatriculaByToken(string accessToken);
        bool HasScope(string scope);
        bool HasPermissionByCpf(string cpf);
    }
}
