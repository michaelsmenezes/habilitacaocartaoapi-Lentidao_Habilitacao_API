using Sesc.MeuSesc.SharedKernel.Authentication.Models;
using System.Threading.Tasks;

namespace Sesc.SharedKernel.Authentication.Services.Base.Contracts
{
    public interface IBaseUserService
    {
        Task<UserCreated> Create(UserAuth user);
        Task<UserAuth> Update(UserAuth user);
        Task<UserAuth> FindByEmail(string email);
        Task<UserAuth> FindByCpf(string cpf);
        Task<ResponseData> ChangePassword(ChangePassword changePassword);
        Task<ChangeCode> ChangeEmail(ChangeEmail changeEmail);
        Task<ResponseData> ConfirmChangeEmail(ChangeEmail changeEmail);
        Task<ChangeCode> RequestResetPassword(UserAuth userAuth);
        Task<ResponseData> ResetPassword(UserResetPassword user);
    }
}
