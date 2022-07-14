using Sesc.Application.ApplicationServices.ViewModel;
using Sesc.MeuSesc.SharedKernel.Authentication.Models;
using Sesc.SharedKernel.Authentication.ViewModel;
using System.Threading.Tasks;

namespace Sesc.Application.ApplicationServices.Commands.Contracts
{
    public interface IAuthCommandServiceApplication
    {
        TokenAuthViewModel GetTokenByAuthorizationCode(AuthorizationCodeAuth code);
        TokenAuthViewModel GetByRefreshToken(RefreshTokenViewModel refreshTokenViewModel);
        Task<UserAuth> GetUserByCpf(string cpf); 
        Task<UserAuth> GetUserByEmail(string email);
        Task<string> UpdateUser(ChangeEmailViewModel changeEmail);

    }
}
