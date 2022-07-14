using Sesc.MeuSesc.SharedKernel.Authentication.Models;
using Sesc.SharedKernel.Authentication.Services.Base.Contracts;
using Sesc.SharedKernel.Authentication.ViewModel;

namespace Sesc.CrossCutting.ServiceAgents.AuthServer.Services.Contracts
{
    public interface ITokenAuthService : IBaseTokenAuthService
    {
        TokenAuth GetTokenByClientCredentials();
        TokenAuthViewModel GetTokenByAuthorizationCode(AuthorizationCodeAuth code);
        TokenAuthViewModel RefreshToken(string refreshToken);
    }
}
