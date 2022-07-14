using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sesc.Application.ApplicationServices.Commands.Contracts;
using Sesc.Application.ApplicationServices.Queries.Contracts;
using Sesc.Domain.Habilitacao.ViewModel;
using Sesc.MeuSesc.Api.Habilitacao.Base;
using Sesc.MeuSesc.Api.Habilitacao.Base.Filters;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Sesc.MeuSesc.Api.Habilitacao.Controllers.V1
{
    [ApiExceptionFilter]
    public class DocumentoController : BaseController
    {
        private IDocumentoQueryServiceApplication _serviceApplicationQueryDocumento;
        private IDocumentoCommandServiceApplication _serviceApplicationCommandDocumento;

        public DocumentoController(
            IDocumentoQueryServiceApplication queryDocumento,
            IDocumentoCommandServiceApplication commandDocumento
        )
        {
            _serviceApplicationQueryDocumento = queryDocumento;
            _serviceApplicationCommandDocumento = commandDocumento;
        }

        [HttpPut("api/v1/Documento/alterar-situacao")]
        public IActionResult PostSalvar([FromBody] DocumentoViewModel documentoVW)
        {
            var result = _serviceApplicationCommandDocumento.AlteraSituacao(documentoVW);

            return Ok(result);
        }

        [HttpGet("api/v1/Documento/Download/{Id}")]
        public async Task<IActionResult> Download(int Id)
        {
            var documentoDownload = await _serviceApplicationQueryDocumento.Download(Id);

            return Ok(documentoDownload);
        }
        
    }
}
