using Sesc.CrossCutting.ServiceAgents.AuthServer.Services.Contracts;
using Sesc.MeuSesc.SharedKernel.Authentication.Models;
using Sesc.MeuSesc.SharedKernel.Authentication.Repository.Contracts;
using Sesc.SharedKernel.Authentication.Services.Base;
using Sesc.SharedKernel.Authentication.ViewModel;

namespace Sesc.CrossCutting.ServiceAgents.AuthServer.Services
{
    public class TokenAuthService : BaseTokenAuthService, ITokenAuthService
    {
        public TokenAuthService(ITokenAuthRepository tokenAuthRepository) : base(tokenAuthRepository)
        {
        }

        public TokenAuthViewModel GetTokenByAuthorizationCode(AuthorizationCodeAuth code)
        {
            return _tokenAuthRepository.GetTokenByAuthorizationCode(code);
        }

        public TokenAuth GetTokenByClientCredentials()
        {
            return _tokenAuthRepository.GetTokenByClientCredentials();
        }

        public TokenAuthViewModel RefreshToken(string refreshToken)
        {
            return _tokenAuthRepository.GetRefreshedToken(refreshToken);
        }
    }
}
