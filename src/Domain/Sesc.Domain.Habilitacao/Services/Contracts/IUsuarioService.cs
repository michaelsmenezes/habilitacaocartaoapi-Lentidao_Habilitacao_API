using Sesc.CrossCutting.ServiceAgents.AuthServer.Dto;
using Sesc.CrossCutting.ServiceAgents.AuthServer.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sesc.Domain.Habilitacao.Services.Contracts
{
    public interface IUsuarioService
    {
        UsuarioCadastradoDTO Cadastrar(UsuarioViewModel usuarioViewModel);
        bool ChangePassword(ChangePasswordViewModel changePasswordViewModel);
        ForgotPasswordDTO ForgotPassword(string cpf);
        bool ResetPassword(ResetPasswordViewModel resetPasswordViewModel);
        string GetImageUsuario();
        string GetImageUsuario(string cpf);
    }
}
