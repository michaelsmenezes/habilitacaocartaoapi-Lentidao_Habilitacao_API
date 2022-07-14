using Sesc.MeuSesc.SharedKernel.Authentication.Models;
using System.Security.Claims;

namespace Sesc.SharedKernel.Authentication.Services.Base.Contracts
{
    public interface IBaseUserAuthenticatedAuthService
    {
        Claim GetClaimByType(string claimType);
        bool IsInRole(string role);
        bool HasClaim(string claimType, string claimValue);
        bool ExistsClaim(string claimType);
        string GetEmailByToken(string accessToken);
        UserAuth GetUserAuthenticated();
    }
}
