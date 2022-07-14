using Microsoft.AspNetCore.Mvc;
using Sesc.Application.ApplicationServices.Queries.Contracts;
using Sesc.MeuSesc.Api.Habilitacao.Base;
using Sesc.MeuSesc.Api.Habilitacao.Base.Filters;

namespace Sesc.MeuSesc.Api.Habilitacao.Controllers.V1
{
    [ApiExceptionFilter]
    public class CidadeController : BaseController
    {
        private readonly ICidadeQueryServiceApplication _serviceApplication;

        public CidadeController(
            ICidadeQueryServiceApplication serviceApplication
        ) {
            _serviceApplication = serviceApplication;
        }

        [HttpGet("api/v1/cidades")]
        public IActionResult GetCidades([FromQuery(Name = "uf")] string uf)
        {
            var result = _serviceApplication.GetCidadesByUf(uf);

            return Ok(result);
        }

        [HttpGet("api/v1/cidades/lista-municipios-responsaveis")]
        public IActionResult GetListaMunicipiosResponsaveis()
        {
            var result = _serviceApplication.GetListaMunicipiosResponsaveis();

            return Ok(result);
        }
    }
}
