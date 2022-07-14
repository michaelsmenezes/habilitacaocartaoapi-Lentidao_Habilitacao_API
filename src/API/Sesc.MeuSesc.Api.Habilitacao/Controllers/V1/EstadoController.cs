using Microsoft.AspNetCore.Mvc;
using Sesc.Application.ApplicationServices.Queries.Contracts;
using Sesc.MeuSesc.Api.Habilitacao.Base;
using Sesc.MeuSesc.Api.Habilitacao.Base.Filters;

namespace Sesc.MeuSesc.Api.Habilitacao.Controllers.V1
{
    [ApiExceptionFilter]
    public class EstadoController : BaseController
    {
        private readonly IEstadoQueryServiceApplication _serviceApplication;

        public EstadoController(
            IEstadoQueryServiceApplication serviceApplication
        ) {
            _serviceApplication = serviceApplication;
        }

        [HttpGet("api/v1/estados")]
        public IActionResult GetEstados()
        {
            var result = _serviceApplication.GetEstados();

            return Ok(result);
        }
    }
}
