using Sesc.SharedKernel.Authentication.Services.Base.Contracts;
using Sesc.MeuSesc.SharedKernel.Authentication.Models;
using Sesc.MeuSesc.SharedKernel.Authentication.Repository.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sesc.SharedKernel.Authentication.Services.Base
{
    public abstract class BaseUserAuthService : IBaseUserAuthService
    {
        protected readonly IUserAuthRepository _userAuthRepository;

        public BaseUserAuthService(
           IUserAuthRepository userAuthRepository
           )
        {
            _userAuthRepository = userAuthRepository;
        }

        //Examplo
        public async Task<bool> RegisterAdministrador(string email, List<string> removeRoles, string roleAdmin)
        {
            var user = await RegisterUserByEmail(email, roleAdmin, removeRoles);

            return user.IsSuccessStatusCode;
        }

        private async Task<UserAuth> RegisterUserByEmail(string email, string role, List<string> removeRoles = null)
        {
            var user = await _userAuthRepository.FindByEmail(email);

            if (!user.IsSuccessStatusCode)
            {
                return new UserAuth
                {
                    IsSuccessStatusCode = false
                };
            }

            if (!IsInRole(user, role.ToString()))
            {
                user.Roles.Add(new RoleAuth { RoleId = role });
            }

            if (removeRoles != null && removeRoles.Count > 0)
            {
                user.Roles.RemoveAll(r => removeRoles.Any(rr => r.RoleId == rr));
            }

            return await _userAuthRepository.UpdateUser(email, user);
        }

        private bool IsInRole(UserAuth user, string role)
        {
            return user.Roles.Any(r => r.RoleId == role);
        }
    }
}
