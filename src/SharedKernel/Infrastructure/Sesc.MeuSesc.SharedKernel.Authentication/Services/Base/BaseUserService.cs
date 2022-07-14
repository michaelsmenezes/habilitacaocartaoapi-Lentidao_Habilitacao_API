using Sesc.SharedKernel.Authentication.Services.Base.Contracts;
using Sesc.MeuSesc.SharedKernel.Authentication.Models;
using Sesc.MeuSesc.SharedKernel.Authentication.Repository.Contracts;
using System.Threading.Tasks;

namespace Sesc.SharedKernel.Authentication.Services.Base
{
    public abstract class BaseUserService : IBaseUserService
    {
        protected readonly IUserAuthRepository _userAuthRepository;

        public BaseUserService(IUserAuthRepository userAuthRepository)
        {
            _userAuthRepository = userAuthRepository;
        }

        public virtual async Task<UserCreated> Create(UserAuth user)
        {
            return await _userAuthRepository.CreateUser(user);
        }

        public virtual async Task<UserAuth> Update(UserAuth user)
        {
            return await _userAuthRepository.UpdateUser(user.Email, user);
        }

        public virtual async Task<ResponseData> ChangePassword(ChangePassword changePassword)
        {
            return await _userAuthRepository.ChangePassword(changePassword);
        }

        public virtual async Task<ChangeCode> ChangeEmail(ChangeEmail changeEmail)
        {
            return await _userAuthRepository.ChangeEmail(changeEmail);
        }

        public virtual async Task<ResponseData> ConfirmChangeEmail(ChangeEmail changeEmail)
        {
            return await _userAuthRepository.ConfirmChangeEmail(changeEmail);
        }

        public virtual async Task<UserAuth> FindByEmail(string email)
        {
            return await _userAuthRepository.FindByEmail(email);
        }

        public virtual async Task<UserAuth> FindByCpf(string cpf)
        {
            return await _userAuthRepository.FindByCpf(cpf);
        }

        public virtual async Task<ChangeCode> RequestResetPassword(UserAuth userAuth)
        {
            return await _userAuthRepository.RequestResetPassword(userAuth);
        }

        public virtual async Task<ResponseData> ResetPassword(UserResetPassword user)
        {
            return await _userAuthRepository.ResetPassword(user);
        }
    }
}
