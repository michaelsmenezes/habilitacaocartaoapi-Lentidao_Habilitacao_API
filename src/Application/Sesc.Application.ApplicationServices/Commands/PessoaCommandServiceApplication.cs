using Sesc.Application.ApplicationServices.Commands.Contracts;
using Sesc.CrossCutting.ServiceAgents.AuthServer.Services.Contracts;

namespace Sesc.Application.ApplicationServices.Commands
{
    public class PessoaCommandServiceApplication : IPessoaCommandServiceApplication
    {
        private readonly IUserAuthenticatedAuthService _userAuthenticatedAuthService;

        public PessoaCommandServiceApplication(
            IUserAuthenticatedAuthService userAuthenticatedAuthService
        )
        {
            _userAuthenticatedAuthService = userAuthenticatedAuthService;
        }

    }
}