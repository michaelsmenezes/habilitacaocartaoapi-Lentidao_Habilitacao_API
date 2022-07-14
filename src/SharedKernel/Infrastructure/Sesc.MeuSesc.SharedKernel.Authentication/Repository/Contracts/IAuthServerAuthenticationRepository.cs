using Sesc.MeuSesc.SharedKernel.Authentication.Models;

namespace Sesc.MeuSesc.SharedKernel.Authentication.Repository.Contracts
{
    public interface IAuthServerAuthenticationRepository
    {
        TokenAuth GetToken();

    }
}
