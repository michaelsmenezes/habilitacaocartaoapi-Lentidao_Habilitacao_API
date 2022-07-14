using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sesc.SharedKernel.Authentication.Services.Base.Contracts
{
    public interface IBaseUserAuthService
    {
        Task<bool> RegisterAdministrador(string email, List<string> removeRoles, string roleAdmin);
    }
}
