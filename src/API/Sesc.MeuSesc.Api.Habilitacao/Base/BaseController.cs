using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Sesc.MeuSesc.Api.Habilitacao.Base.Filters;

namespace Sesc.MeuSesc.Api.Habilitacao.Base
{
    [ApiExceptionFilter]
    //[Authorize]
    [EnableCors("CorsPolicy")]
    public class BaseController : Controller
    {
    }
}
