using Sesc.CrossCutting.ServiceAgents.AuthServer.Enum;
using Sesc.CrossCutting.ServiceAgents.AuthServer.Services.Contracts;
using Sesc.CrossCutting.ServiceAgents.AuthServer.ViewModel;
using Sesc.MeuSesc.SharedKernel.Authentication.Models;
using Sesc.MeuSesc.SharedKernel.Authentication.Repository.Contracts;
using Sesc.SharedKernel.Authentication.Services.Base;
using System.Linq;
using System.Threading.Tasks;

namespace Sesc.CrossCutting.ServiceAgents.AuthServer.Services
{
    public class UserAuthService : BaseUserAuthService, IUserAuthService
    {
        public UserAuthService(IUserAuthRepository userAuthRepository) : base(userAuthRepository)
        {
        }

        public async Task<UserAuth> AddCleanComerciario(string cpf, string matricula)
        {
            var user = await _userAuthRepository.FindByCpf(cpf);


            if (user.Claims.Where(c => c.ClaimType == ClaimsAuth.Comerciario).Count() <= 0)
            {
                user.Claims.RemoveAll(c => c.ClaimType == ClaimsAuth.Comerciario);
                user.Claims.Add(new ClaimAuth { ClaimValue = matricula, ClaimType = ClaimsAuth.Comerciario });

                user = await _userAuthRepository.UpdateUser(user.Email, user);
            }

            return user;
        }
        public async Task<UserAuth> RemoveCleanComerciario(string cpf, string matricula)
        {
            var user = await _userAuthRepository.FindByCpf(cpf);


            if (user.Claims.Where(c => c.ClaimType == ClaimsAuth.Comerciario).Count() > 0)
            {
                user.Claims.RemoveAll(c => c.ClaimType == ClaimsAuth.Comerciario);
                user = await _userAuthRepository.UpdateUser(user.Email, user);
            }

            return user;
        }

        public async Task<UserAuth> GetUserByCpf(string cpf)
        {
            var user = await _userAuthRepository.FindByCpf(cpf);

            if (user != null)
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        public async Task<UserAuth> GetUserByEmail(string email)
        {
            var user = await _userAuthRepository.FindByEmail(email);

            if (user != null)
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        public async Task<UserAuth> UpdateUser(string cpf, UserAuth user)
        {
            var result = await _userAuthRepository.UpdateUser(cpf, user);

            if (result != null)
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> RequestResetByEmail(string email)
        {
            return await _userAuthRepository.RequestResetByEmail(email);
        }
    }
}
