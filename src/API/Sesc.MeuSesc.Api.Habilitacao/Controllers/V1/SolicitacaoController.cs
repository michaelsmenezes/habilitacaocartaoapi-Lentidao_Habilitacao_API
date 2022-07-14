using Microsoft.AspNetCore.Mvc;
using Sesc.Application.ApplicationServices.Commands.Contracts;
using Sesc.Application.ApplicationServices.Queries.Contracts;
using Sesc.Domain.Habilitacao.Enum;
using Sesc.Domain.Habilitacao.ViewModel;
using Sesc.MeuSesc.Api.Habilitacao.Base;
using Sesc.MeuSesc.Api.Habilitacao.Base.Filters;

namespace Sesc.MeuSesc.Api.Habilitacao.Controllers.V1
{
    [ApiExceptionFilter]
    public class SolicitacaoController : BaseController
    {
        private readonly ISolicitacaoQueryServiceApplication _serviceApplicationQuerySolicitacao;
        private readonly ISolicitacaoCommandServiceApplication _serviceApplicationCommandSolicitacao;
        private readonly IDependenteCommandServiceApplication _serviceApplicationCommandDependente;
        private readonly IDocumentoCommandServiceApplication _serviceApplicationCommandDocumento;

        public SolicitacaoController(
            ISolicitacaoQueryServiceApplication querySolicitacao,
            ISolicitacaoCommandServiceApplication commandSolicitacao,
            IDependenteCommandServiceApplication serviceApplicationCommandDependente,
            IDocumentoCommandServiceApplication serviceApplicationCommandDocumento
        )
        {
            _serviceApplicationQuerySolicitacao = querySolicitacao;
            _serviceApplicationCommandSolicitacao = commandSolicitacao;
            _serviceApplicationCommandDependente = serviceApplicationCommandDependente;
            _serviceApplicationCommandDocumento = serviceApplicationCommandDocumento;
        }

        [HttpGet("api/v1/solicitacoes-pessoa-logada")]
        public IActionResult GetSolicitacoesPessoaLogada()
        {
            var result = _serviceApplicationQuerySolicitacao.GetSolicitacoesPessoaLogada();

            return Ok(result);
        }

        [HttpGet("api/v1/solicitacao/{id}")]
        public IActionResult Get(int id)
        {
            var result = _serviceApplicationQuerySolicitacao.GetById(id);

            return Ok(result);
        }

        [HttpGet("api/v1/solicitacao-backoffice/{id}")]
        public IActionResult GetBackOffice(int id)
        {
            var result = _serviceApplicationQuerySolicitacao.GetById(id);

            return Ok(result);
        }

        [HttpPost("api/v1/solicitacao")]
        public IActionResult PostSalvar([FromBody] SolicitacaoViewModel solicitacaoVW)
        {
            var result = _serviceApplicationCommandSolicitacao.Salvar(solicitacaoVW);

            return Ok(result);
        }


        [HttpGet("api/v1/solicitacao/all/")]
        public JsonResult GetSolicitacoesAll([FromQuery] SolicitacaoFiltrosViewModel solicitacaoFiltrosVW)
        {
            var result = _serviceApplicationQuerySolicitacao.GetSolicitacoesAll(solicitacaoFiltrosVW);
            return Json(result);
        }

        [HttpGet("api/v1/solicitacao/estatisticas/")]
        public JsonResult GetSolicitacoesEstatisticas([FromQuery] SolicitacaoFiltrosViewModel solicitacaoFiltrosVW)
        {
            var result = _serviceApplicationQuerySolicitacao.GetSolicitacoesEstatisticas(solicitacaoFiltrosVW);

            return Json(result);
        }

        [HttpGet("api/v1/solicitacao/get-pessoa-cadastrando/{cpf}")]
        public IActionResult GetCadastrandoByCpf(string cpf)
        {
            var result = _serviceApplicationQuerySolicitacao.GetPessoaCadastrando(cpf);

            return Ok(result);
        }

        [HttpPut("api/v1/solicitacao/cancelar")]
        public IActionResult Cancelar([FromBody] SolicitacaoViewModel solicitacaoVW)
        {
            var result = _serviceApplicationCommandSolicitacao.Cancelar(solicitacaoVW);

            return Ok(result);
        }

        [HttpPut("api/v1/solicitacao/{id}/cancelar-dependente")]
        public IActionResult CancelarInclusaoDependente(int id, [FromBody] DependenteViewModel dependenteVw)
        {
            _serviceApplicationCommandDependente.CancelarInclusaoDependente(id, dependenteVw);

            return Ok(null);
        }

        [HttpPost("api/v1/solicitacao/{id}/documento")]
        public IActionResult PostDocumento(UploadDocumentoViewModel documento)
        {
            _serviceApplicationCommandDocumento.SalvarArquivoUsuarioLogado(documento);

            return Ok(null);
        }

        [HttpPost("api/v1/solicitacao/finalizar-cadastro")]
        public IActionResult FinalizarCadastro([FromBody] SolicitacaoViewModel solicitacaoVW)
        {
            var result = _serviceApplicationCommandSolicitacao.FinalizarCadastro(solicitacaoVW);

            return Ok(result);
        }

        [HttpPost("api/v1/solicitacao/alterar-dependentes")]
        public IActionResult NovaSolicitacaoAlterarDependentes()
        {
            var result = _serviceApplicationCommandSolicitacao.NovaSolicitacaoAlterarDependentes();

            return Ok(result);
        }

        [HttpPost("api/v1/solicitacao/renovar-cartao")]
        public IActionResult NovaSolicitacaRenovarCartao()
        {
            var result = _serviceApplicationCommandSolicitacao.NovaSolicitacaRenovarCartao();

            return Ok(result);
        }

        [HttpPost("api/v1/solicitacao/mudanca-categoria")]
        public IActionResult NovaSolicitacaoMudancaCategoria()
        {
            var result = _serviceApplicationCommandSolicitacao.NovaSolicitacaoMudancaCategoria();

            return Ok(result);
        }

        [HttpPut("api/v1/solicitacao/{solicitacaoId}/remove-renovacao-dependente")]
        public IActionResult RemoverRenovacaoDependente(int solicitacaoId, int dependenteId)
        {
            _serviceApplicationCommandDependente.RemoverRenovacaoDependente(solicitacaoId, dependenteId);

            return Ok(null);
        }

        [HttpPut("api/v1/solicitacao/{id}/remove-dependente-ativo")]
        public IActionResult RemoverDependenteAtivo(int id, string cpf)
        {
            _serviceApplicationCommandDependente.RemoverDependenteAtivo(id, cpf);

            return Ok(null);
        }

        [HttpPut("api/v1/solicitacao/{solicitacaoId}/setar-renovacao-dependente")]
        public IActionResult SetarRenovacaoDependente(int solicitacaoId, int id, AcaoEnum acao)
        {
            _serviceApplicationCommandDependente.SetarRenovacaoDependente(solicitacaoId, id, acao);

            return Ok(null);
        }
    }
}
