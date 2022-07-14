using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sesc.Application.ApplicationServices.Commands.Contracts;
using Sesc.Application.ApplicationServices.ViewModel;
using Sesc.MeuSesc.Api.Habilitacao.Base;
using Sesc.MeuSesc.Api.Habilitacao.Base.Filters;
using Sesc.MeuSesc.SharedKernel.Authentication.Models;
using System.Threading.Tasks;

namespace Sesc.MeuSesc.Api.Habilitacao.Controllers.V1
{
    [AllowAnonymous]
    [ApiExceptionFilter]
    public class AuthController : BaseController
    {
        private readonly IAuthCommandServiceApplication _commandService;

        public AuthController(IAuthCommandServiceApplication commandService)
        {
            _commandService = commandService;
        }

        [HttpPost("api/v1/auth")]
        public IActionResult Login([FromBody] AuthorizationCodeAuth codeAuth)
        {
            return Ok(_commandService.GetTokenByAuthorizationCode(codeAuth));
        }

        [HttpGet("api/v1/auth/user/{cpf}")]
        public async Task<IActionResult> GetUserByCpf(string cpf)
        {
            var result = await _commandService.GetUserByCpf(cpf);

            return Ok(result);
        }

        [HttpGet("api/v1/auth/user-by-email/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var result = await _commandService.GetUserByEmail(email);

            return Ok(result);
        }

        [HttpPut("api/v1/auth/update-user")]
        public async Task<IActionResult> UpdateUser([FromBody] ChangeEmailViewModel changeEmail)
        {
            var result = await _commandService.UpdateUser(changeEmail);

            return Ok(result);
        }

        [HttpPost("api/v1/auth/refresh-token")]
        public IActionResult RefreshToken([FromBody] RefreshTokenViewModel refreshTokenViewModel)
        {
            return Ok(_commandService.GetByRefreshToken(refreshTokenViewModel));
        }
    }
}
