using Sesc.SharedKernel.Authentication.ViewModel;
using Sesc.MeuSesc.SharedKernel.Authentication.Models;
using System.Threading.Tasks;

namespace Sesc.SharedKernel.Authentication.Services.Base.Contracts
{
    public interface IBaseTokenAuthService
    {
        TokenAuthViewModel GetToken(UserAuth user);
        TokenAuthViewModel GetRefreshedToken(string refreshToken);
        Task<IntrospectResult> Introspect(string token);
    }
}
