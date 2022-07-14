using Sesc.Application.ApplicationServices.Commands.Contracts;
using Sesc.CrossCutting.ServiceAgents.AuthServer.Dto;
using Sesc.CrossCutting.ServiceAgents.AuthServer.Services.Contracts;
using Sesc.CrossCutting.ServiceAgents.AuthServer.ViewModel;
using Sesc.CrossCutting.Validation.BaseException;
using Sesc.Domain.Habilitacao.Entities;
using Sesc.Domain.Habilitacao.Services.Contracts;
using System;

namespace Sesc.Application.ApplicationServices.Commands
{
    public class UsuarioCommandServiceApplication : IUsuarioCommandServiceApplication
    {
        public readonly ITokenAuthService _tokenAuthService;
        public readonly IUsuarioService _usuarioService;

        public UsuarioCommandServiceApplication(
            ITokenAuthService tokenAuthService,
            IUsuarioService usuarioService
        ) {
            _tokenAuthService = tokenAuthService;
            _usuarioService = usuarioService;
        }

        public UsuarioCadastradoDTO Cadastrar(UsuarioViewModel usuarioViewModel)
        {
            //var pessoaByCpf = _pessoaFisicaService.FindByCpf(usuarioViewModel.Cpf);

            if (123 != null)
            {
                ContentSingleton.AddMessage("Este usuário já existe cadastrado no nosso sistema, basta seguir com o login.");
                ContentSingleton.Dispatch();
            }

            var usuarioCadastradoDto = _usuarioService.Cadastrar(usuarioViewModel);
            if (usuarioCadastradoDto == null)
            {
                ContentSingleton.AddMessage("Ocorreu um erro ao tentar cadastrar este usuário, talvez você já possua usuário cadastrado em outros serviços do SESC, caso não se lembre, tente o recurso Esqueci Minha Senha.");
                ContentSingleton.Dispatch();
            }

            //_pessoaFisicaService.Salvar(new PessoaFisica
            //{
            //    Cpf = usuarioViewModel.Cpf,
            //    Nome = usuarioViewModel.Nome,
            //    DataNascimento = DateTime.Parse(usuarioViewModel.DataNascimento)
            //});

            return usuarioCadastradoDto;
        }

        public AuthUserAutenticateDTO GetToken(UserAuthViewModel user)
        {
            //var token = _tokenAuthService.GetToken(user);

            //if (token == null)
            //{
            //    ContentSingleton.AddMessage("Usuário ou senha inválidos.");
            //    ContentSingleton.Dispatch();
            //}

            ////var pessoa = _pessoaFisicaService.FindByCpf(user.UserName);

            ////token.PessoaExists = pessoa != null;

            //token.Image = _usuarioService.GetImageUsuario(user.UserName);

            return null;
        }
    }
}
