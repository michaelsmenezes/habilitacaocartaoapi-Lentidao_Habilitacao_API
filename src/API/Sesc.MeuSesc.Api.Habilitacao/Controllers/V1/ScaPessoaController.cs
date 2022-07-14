using Microsoft.AspNetCore.Mvc;
using Sesc.Application.ApplicationServices.Queries.Contracts;
using Sesc.CrossCutting.ServiceAgents.AuthServer.Services.Contracts;
using Sesc.MeuSesc.Api.Habilitacao.Base;
using Sesc.MeuSesc.Api.Habilitacao.Base.Filters;
using System;

namespace Sesc.MeuSesc.Api.Habilitacao.Controllers.V1
{
    [ApiExceptionFilter]
    public class ScaPessoaController : BaseController
    {
        private readonly IPessoaScaQueryServiceApplication _serviceApplicationQueryPessoaSca;
        private readonly IUserAuthenticatedAuthService _userAuthenticatedAuthService;

        public ScaPessoaController(
            IPessoaScaQueryServiceApplication querySolicitacao,
            IUserAuthenticatedAuthService userAuthenticatedAuthService
        )
        {
            _serviceApplicationQueryPessoaSca = querySolicitacao;
            _userAuthenticatedAuthService = userAuthenticatedAuthService;
        }

        [HttpGet("api/v1/sca/pessoa/{cpf}")]
        public IActionResult GetPessoaScaByCpf(string cpf)
        {
            var result = _serviceApplicationQueryPessoaSca.GetByCpf(cpf);

            return Ok(result);
        }

        [HttpGet("api/v1/sca/pessoa/dependentes-usuario-logado")]
        public IActionResult GetDependentesUsuarioLogado()
        {
            var result = _serviceApplicationQueryPessoaSca.GetDependentesUsuarioLogado();

            return Ok(result);
        }
        
        [HttpGet("api/v1/sca/pessoa/logada")]
        public IActionResult GetPessoaScaLogada()
        {
            var user = _userAuthenticatedAuthService.GetUserAuthenticated();
            var result = _serviceApplicationQueryPessoaSca.GetByCpf(user.CpfCnpj);

            return Ok(result);
        }
    }
}
