using Sesc.SharedKernel.Authentication.ViewModel;
using Sesc.MeuSesc.SharedKernel.Authentication.Models;
using System.Threading.Tasks;

namespace Sesc.MeuSesc.SharedKernel.Authentication.Repository.Contracts
{
    public interface ITokenAuthRepository
    {
        TokenAuthViewModel GetToken(UserAuth user);
        TokenAuthViewModel GetRefreshedToken(string refreshToken);
        TokenAuthViewModel GetTokenByAuthorizationCode(AuthorizationCodeAuth code);
        TokenAuth GetTokenByClientCredentials();
        Task<IntrospectResult> Introspect(string token);
    }
}
