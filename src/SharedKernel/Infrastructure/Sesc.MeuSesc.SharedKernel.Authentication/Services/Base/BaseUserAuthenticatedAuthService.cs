using System.Security.Claims;
using System.Linq;
using Sesc.SharedKernel.Authentication.Services.Base.Contracts;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using Sesc.MeuSesc.SharedKernel.Authentication.Enums;
using Sesc.MeuSesc.SharedKernel.Authentication.Models;

namespace Sesc.SharedKernel.Authentication.Services.Base
{
    public abstract class BaseUserAuthenticatedAuthService : IBaseUserAuthenticatedAuthService
    {
        protected readonly ClaimsPrincipal _claimsPrincipal;

        public BaseUserAuthenticatedAuthService(
            IHttpContextAccessor httpContextAccessor
        ) {
            if(httpContextAccessor.HttpContext != null)
            {
                _claimsPrincipal = httpContextAccessor.HttpContext.User;
            }
        }

        public virtual Claim GetClaimByType(string claimType)
        {
            return _claimsPrincipal.FindFirst(claimType);
        }

        public virtual bool IsInRole(string role)
        {
            return _claimsPrincipal.IsInRole(role);
        }

        public virtual bool HasClaim(string claimType, string claimValue)
        {
            return _claimsPrincipal.HasClaim(claimType, claimValue);
        }

        public bool ExistsClaim(string claimType)
        {
            return _claimsPrincipal.HasClaim(c => c.Type == claimType);
        }

        public virtual string GetEmailByToken(string accessToken)
        {
            var token = new JwtSecurityTokenHandler().ReadJwtToken(accessToken);
            return token.Claims.Where(c => c.Type == BaseEClaimsAuth.Email).Select(c => c.Value).FirstOrDefault();
        }
        public abstract UserAuth GetUserAuthenticated();

        public abstract bool IsAdmin();
    }
}
