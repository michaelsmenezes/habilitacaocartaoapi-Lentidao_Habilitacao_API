using Sesc.CrossCutting.ServiceAgents.AuthServer.Services.Contracts;
using Sesc.MeuSesc.SharedKernel.Authentication.Repository.Contracts;
using Sesc.SharedKernel.Authentication.Services.Base;

namespace Sesc.CrossCutting.ServiceAgents.AuthServer.Services
{
    public class UserService : BaseUserService, IUserService
    {
        public UserService(IUserAuthRepository userAuthRepository) : base(userAuthRepository)
        {
        }
    }
}
