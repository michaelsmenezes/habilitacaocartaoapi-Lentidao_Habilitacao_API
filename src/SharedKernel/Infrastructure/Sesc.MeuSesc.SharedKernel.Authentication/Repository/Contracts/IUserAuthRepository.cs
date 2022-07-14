using Sesc.MeuSesc.SharedKernel.Authentication.Models;
using System.Threading.Tasks;

namespace Sesc.MeuSesc.SharedKernel.Authentication.Repository.Contracts
{
    public interface IUserAuthRepository
    {
        Task<UserAuth> UpdateUser(string email, UserAuth user);
        Task<UserCreated> CreateUser(UserAuth user);
        Task<UserAuth> FindByEmail(string email);
        Task<UserAuth> FindByCpf(string cpf);
        Task<ResponseData> ChangePassword(ChangePassword change);
        Task<ChangeCode> ChangeEmail(ChangeEmail changeEmail);
        Task<ResponseData> ConfirmChangeEmail(ChangeEmail changeEmail);
        Task<ChangeCode> RequestResetPassword(UserAuth user);
        Task<ResponseData> ResetPassword(UserResetPassword user);
        Task<bool> RequestResetByEmail(string email);
        
    }

}
