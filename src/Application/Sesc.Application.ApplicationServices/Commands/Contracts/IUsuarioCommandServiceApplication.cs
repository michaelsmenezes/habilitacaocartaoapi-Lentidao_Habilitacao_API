using Sesc.CrossCutting.ServiceAgents.AuthServer.Dto;
using Sesc.CrossCutting.ServiceAgents.AuthServer.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sesc.Application.ApplicationServices.Commands.Contracts
{
    public interface IUsuarioCommandServiceApplication
    {
        AuthUserAutenticateDTO GetToken(UserAuthViewModel user);
        UsuarioCadastradoDTO Cadastrar(UsuarioViewModel usuarioViewModel);
    }
}
