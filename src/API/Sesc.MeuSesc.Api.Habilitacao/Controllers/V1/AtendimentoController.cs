using Microsoft.AspNetCore.Mvc;
using Sesc.Application.ApplicationServices.Commands.Contracts;
using Sesc.Domain.Habilitacao.ViewModel;
using Sesc.MeuSesc.Api.Habilitacao.Base;
using Sesc.MeuSesc.Api.Habilitacao.Base.Filters;
using System;
using System.Threading.Tasks;

namespace Sesc.MeuSesc.Api.Habilitacao.Controllers.V1
{
    [ApiExceptionFilter]
    public class AtendimentoController : BaseController
    {
        private readonly IAtendimentoCommandServiceApplication _commandAtendimento;

        public AtendimentoController(
            IAtendimentoCommandServiceApplication commandAtendimento
        )
        {
            _commandAtendimento = commandAtendimento;
        }

        [HttpPut("api/v1/atendimento/associar-atendente")]
        public async Task<IActionResult> AssociarAtendente([FromBody] SolicitacaoAtendimentoViewModel SolicitacaoAtendimentoViewModel)
        {

            var result = await _commandAtendimento.AssociarAtendente(SolicitacaoAtendimentoViewModel);

            return Ok(result);
        }

        [HttpPut("api/v1/atendimento/finalizar")]
        public async Task<IActionResult> Finalizar([FromBody] AtendimentoFinalizarViewModel AtendimentoFinalizarViewModel)
        {
            var result = await _commandAtendimento.Finalizar(AtendimentoFinalizarViewModel);

            return Ok(result);
        }

        [HttpPost("api/v1/atendimento/reenviar-email")]
        public async Task<IActionResult> ReenviarEmail([FromBody] int solicitacaoId)
        {
            var result = await _commandAtendimento.ReenviarEmail(solicitacaoId);

            return Ok(result);
        }
    }
}
