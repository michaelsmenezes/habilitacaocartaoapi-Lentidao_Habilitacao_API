using Microsoft.AspNetCore.Http;
using Sesc.CrossCutting.ServiceAgents.AuthServer.Enum;
using Sesc.CrossCutting.ServiceAgents.AuthServer.Services.Contracts;
using Sesc.MeuSesc.SharedKernel.Authentication.Enums;
using Sesc.MeuSesc.SharedKernel.Authentication.Models;
using Sesc.SharedKernel.Authentication.Services.Base;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace Sesc.CrossCutting.ServiceAgents.AuthServer.Services
{
    public class UserAuthenticatedAuthService : BaseUserAuthenticatedAuthService, IUserAuthenticatedAuthService
    {
        private readonly IUserService _userService;

        public UserAuthenticatedAuthService(
            IHttpContextAccessor httpContextAccessor, 
            IUserService userService
        ) : base(httpContextAccessor) {
            _userService = userService;
        }

        public string GetCpfByToken(string accessToken)
        {
            var token = new JwtSecurityTokenHandler().ReadJwtToken(accessToken);
            return token.Claims.Where(c => c.Type == ClaimsAuth.Cpf).Select(c => c.Value).FirstOrDefault();
        }

        public string GetMatriculaByToken(string accessToken)
        {
            var token = new JwtSecurityTokenHandler().ReadJwtToken(accessToken);
            return token.Claims.Where(c => c.Type == ClaimsAuth.Comerciario).Select(c => c.Value).FirstOrDefault();
        }

        public override UserAuth GetUserAuthenticated()
        {
            Claim cpf = GetClaimByType(BaseEClaimsAuth.Cpf);

            return _userService.FindByCpf(cpf.Value).Result;
        }

        public bool HasAuthorization(string cpf)
        {
            var cpfAuth = GetClaimByType(BaseEClaimsAuth.Cpf);

            return (cpfAuth != null && cpf == cpfAuth.Value) || IsAdmin();
        }

        public bool HasScope(string scope)
        {
            var scopes = _claimsPrincipal.FindAll("scope");
            return scopes.Any(s => s.Value == scope);
        }

        public bool HasPermissionByCpf(string cpf)
        {
            var claim = GetClaimByType(ClaimsAuth.Cpf);
            return HasClietApiAuthorized() || (claim != null && claim.Value == cpf);
        }

        private bool HasClietApiAuthorized()
        {
            return HasScope(ScopeAuth.Admin);
        }

        public override bool IsAdmin()
        {
            return IsInRole(ERoleAuth.Admin) || HasClietApiAuthorized();
        }
    }
}
