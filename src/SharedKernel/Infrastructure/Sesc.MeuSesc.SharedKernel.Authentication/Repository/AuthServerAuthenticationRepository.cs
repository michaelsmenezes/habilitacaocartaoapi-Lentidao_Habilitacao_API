using Sesc.MeuSesc.SharedKernel.Authentication.Models;
using Sesc.MeuSesc.SharedKernel.Authentication.Repository.Contracts;

namespace Sesc.MeuSesc.SharedKernel.Authentication.Repository
{
    public class AuthServerAuthenticationRepository : IAuthServerAuthenticationRepository
    {
        protected readonly ITokenAuthRepository _tokenAuthRepository;
        protected TokenAuth _token;
        public AuthServerAuthenticationRepository(ITokenAuthRepository tokenAuthRepository)
        {
            _tokenAuthRepository = tokenAuthRepository;
        }

        public TokenAuth GetToken()
        {
            if (_token == null)
            {
                return (_token = _tokenAuthRepository.GetTokenByClientCredentials());
            }

            return _token;
        }
    }
}
