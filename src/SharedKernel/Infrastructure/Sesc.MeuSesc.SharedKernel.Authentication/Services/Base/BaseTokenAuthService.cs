using Sesc.SharedKernel.Authentication.Services.Base.Contracts;
using Sesc.SharedKernel.Authentication.ViewModel;
using Sesc.MeuSesc.SharedKernel.Authentication.Models;
using Sesc.MeuSesc.SharedKernel.Authentication.Repository.Contracts;
using System.Threading.Tasks;

namespace Sesc.SharedKernel.Authentication.Services.Base
{
    public abstract class BaseTokenAuthService : IBaseTokenAuthService
    {
        protected readonly ITokenAuthRepository _tokenAuthRepository;

        protected BaseTokenAuthService(ITokenAuthRepository tokenAuthRepository)
        {
            _tokenAuthRepository = tokenAuthRepository;
        }

        public virtual TokenAuthViewModel GetToken(UserAuth user)
        {
            return _tokenAuthRepository.GetToken(user);
        }

        public virtual TokenAuthViewModel GetRefreshedToken(string refreshToken)
        {
            return _tokenAuthRepository.GetRefreshedToken(refreshToken);
        }

        public async Task<IntrospectResult> Introspect(string token)
        {
            return await _tokenAuthRepository.Introspect(token);
        }
    }
}
