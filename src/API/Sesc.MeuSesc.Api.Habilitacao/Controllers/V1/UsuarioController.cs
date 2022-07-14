using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sesc.Application.ApplicationServices.Commands.Contracts;
using Sesc.CrossCutting.ServiceAgents.AuthServer.Dto;
using Sesc.CrossCutting.ServiceAgents.AuthServer.Services.Contracts;
using Sesc.CrossCutting.ServiceAgents.AuthServer.ViewModel;
using Sesc.Domain.Habilitacao.Services.Contracts;
using Sesc.MeuSesc.Api.Habilitacao.Base;
using Sesc.MeuSesc.Api.Habilitacao.Base.Filters;

namespace Sesc.MeuSesc.Api.Habilitacao.Controllers.V1
{
    [ApiExceptionFilter]
    public class UsuarioController : BaseController
    {
        private readonly IUsuarioService _service;
        private readonly IUsuarioCommandServiceApplication _serviceApplication;
        private readonly IUserAuthenticatedAuthService _userAuthenticatedAuthService;

        public UsuarioController(
            IUsuarioService service, 
            IUserAuthenticatedAuthService userAuthenticatedAuthService,
            IUsuarioCommandServiceApplication serviceApplication
        ) {
            _service = service;
            _userAuthenticatedAuthService = userAuthenticatedAuthService;
            _serviceApplication = serviceApplication;
        }

        [AllowAnonymous]
        [HttpPost("api/v1/usuario/salvar")]
        public JsonResult PostSalvarUsuario(UsuarioViewModel usuarioViewModel)
        {
            var result = _serviceApplication.Cadastrar(usuarioViewModel);

            return Json(result);
        }

        [HttpPost("api/v1/usuario/change-password")]
        public JsonResult PutChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            return Json(_service.ChangePassword(changePasswordViewModel));
        }

        [HttpGet("api/v1/usuario/image")]
        public JsonResult GetImageUsuario()
        {
            return Json(_service.GetImageUsuario());
        }

        [AllowAnonymous]
        [HttpPost("api/v1/usuario/forgot-password")]
        public JsonResult PostForgotPassword(string cpf)
        {
            return Json(_service.ForgotPassword(cpf));
        }

        [AllowAnonymous]
        [HttpPut("api/v1/usuario/reset-password")]
        public JsonResult PutResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            return Json(_service.ResetPassword(resetPasswordViewModel));
        }

        [HttpGet("api/v1/usuario/usuario-autenticado")]
        public JsonResult GetUsuarioAutenticado()
        {
            var user = _userAuthenticatedAuthService.GetUserAuthenticated();

            if (user != null)
            {
                return Json(new UserAutenticatedDTO
                {
                    Email = user.Email,
                    Cpf = user.CpfCnpj
                });
            }

            return Json(null);
        }
    }
}
