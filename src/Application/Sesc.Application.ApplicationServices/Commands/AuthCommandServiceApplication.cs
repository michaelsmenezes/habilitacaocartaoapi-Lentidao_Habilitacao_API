using Sesc.Application.ApplicationServices.Commands.Contracts;
using Sesc.Application.ApplicationServices.ViewModel;
using Sesc.CrossCutting.ServiceAgents.AuthServer.Services.Contracts;
using Sesc.CrossCutting.ServiceAgents.Clientela.Enums;
using Sesc.CrossCutting.ServiceAgents.Clientela.Services.Contracts;
using Sesc.MeuSesc.SharedKernel.Authentication.Models;
using Sesc.SharedKernel.Authentication.ViewModel;
using System;
using System.Threading.Tasks;

namespace Sesc.Application.ApplicationServices.Commands
{
    public class AuthCommandServiceApplication : IAuthCommandServiceApplication
    {
        private readonly ITokenAuthService _tokenAtuthService;
        private readonly IUserAuthenticatedAuthService _userAuthenticatedAuthService;
        private readonly IUserAuthService _userAuthService;
        private readonly IPessoaScaService _pessoaScaService;

        public AuthCommandServiceApplication(
            ITokenAuthService tokenAtuthService,
            IUserAuthenticatedAuthService userAuthenticatedAuthService,
            IUserAuthService userAuthService,
            IPessoaScaService pessoaScaService
        )
        {
            _tokenAtuthService = tokenAtuthService;
            _userAuthenticatedAuthService = userAuthenticatedAuthService;
            _userAuthService = userAuthService;
            _pessoaScaService = pessoaScaService;
        }

        public TokenAuthViewModel GetTokenByAuthorizationCode(AuthorizationCodeAuth code)
        {
            var auth = _tokenAtuthService.GetTokenByAuthorizationCode(code);

            if (auth != null)
            {
                auth = InitPessoaSca(auth).Result;
            }

            return auth;
        }

        private async Task<TokenAuthViewModel> InitPessoaSca(TokenAuthViewModel auth)
        {
            var cpf = _userAuthenticatedAuthService.GetCpfByToken(auth.AccessToken);

            var pessoaSca = _pessoaScaService.GetPessoa(cpf);

            if (pessoaSca != null)
            {
                if (CategoriaSca.DescricaoCategoriaSca(pessoaSca.cdcategori) == "Trabalhador do Comércio")
                {
                    if (_userAuthenticatedAuthService.GetMatriculaByToken(auth.AccessToken) == null)
                    {
                        await _userAuthService.AddCleanComerciario(cpf, pessoaSca.formataMatriculaSemMascara);
                        auth = RefreshInitClaim(auth);
                    }
                }
                else
                {
                    await _userAuthService.RemoveCleanComerciario(cpf, pessoaSca.formataMatriculaSemMascara);
                    auth = RefreshInitClaim(auth);
                }

                auth.foto = pessoaSca.foto;
            }

            return auth;
        }

        private TokenAuthViewModel RefreshInitClaim(TokenAuthViewModel auth)
        {
            return _tokenAtuthService.GetRefreshedToken(auth.RefreshToken);
        }

        public async Task<UserAuth> GetUserByCpf(string cpf)
        {
            var result = await _userAuthService.GetUserByCpf(cpf);

            return result;
        }

        public async Task<UserAuth> GetUserByEmail(string email)
        {
            var result = await _userAuthService.GetUserByEmail(email);

            return result;
        }
        
        public async Task<string> UpdateUser(ChangeEmailViewModel changeEmailViewModel)
        {

            var user = await _userAuthService.GetUserByCpf(changeEmailViewModel.Cpf);
            user.Email = changeEmailViewModel.Email;
            user.EmailConfirmed = true;

            var result = await _userAuthService.UpdateUser(user.CpfCnpj, user);

            if (!result.IsSuccessStatusCode)
            {
                return null;
            }

            var email = await _userAuthService.RequestResetByEmail(user.Email);

            return "E-mail alterado com sucesso";
        }

        public TokenAuthViewModel GetByRefreshToken(RefreshTokenViewModel refreshTokenViewModel)
        {
            return _tokenAtuthService.GetRefreshedToken(refreshTokenViewModel.RefreshToken);
        }
    }
}
