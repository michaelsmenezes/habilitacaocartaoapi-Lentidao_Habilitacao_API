using Microsoft.Extensions.Options;
using Sesc.CrossCutting.Config;
using Sesc.CrossCutting.ServiceAgents.AuthServer.Dto;
using Sesc.CrossCutting.ServiceAgents.AuthServer.Services.Contracts;
using Sesc.CrossCutting.ServiceAgents.AuthServer.Validation;
using Sesc.CrossCutting.ServiceAgents.AuthServer.ViewModel;
using Sesc.CrossCutting.ServiceAgents.Clientela.Services.Contracts;
using Sesc.CrossCutting.Validation.BaseException;
using Sesc.CrossCutting.Validation.BaseValidation;
using Sesc.Domain.Habilitacao.Entities;
using Sesc.Domain.Habilitacao.Repositories.Contracts;
using Sesc.Domain.Habilitacao.Services.Contracts;
using Sesc.Domain.Habilitacao.Validator;
using Sesc.MeuSesc.SharedKernel.Authentication.Models;
using System;
using System.Collections.Generic;
using System.Net;

namespace Sesc.Domain.Habilitacao.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUserService _userAuthService;
        private readonly ValidatorBase<UsuarioViewModel> _validator;
        private readonly IUserAuthenticatedAuthService _userAuthenticatedAuthService;
        private readonly IPessoaService _pessoaService;
        private readonly IPessoaScaService _pessoaScaService;
        private readonly ApiConfig _apiConfig;

        public UsuarioService(
            IUserService userAuthService,
            IUserAuthenticatedAuthService userAuthenticatedAuthService,
            IPessoaService pessoaService,
            IPessoaScaService pessoaScaService,
            IOptions<ApiConfig> options
        ) {
            _userAuthService = userAuthService;
            _validator = (ValidatorBase<UsuarioViewModel>)Activator.CreateInstance(typeof(UsuarioViewModelValidator));
            _userAuthenticatedAuthService = userAuthenticatedAuthService;
            _apiConfig = options.Value;
            _pessoaService = pessoaService;
            _pessoaScaService = pessoaScaService;
        }

        public UsuarioCadastradoDTO Cadastrar(UsuarioViewModel usuarioViewModel)
        {
            _validator.DoValidate(usuarioViewModel);

            UserCreatedDTO userCreated = RegistarUsuario(usuarioViewModel);

            if (userCreated.UserAuth.IsSuccessStatusCode)
            {
                return new UsuarioCadastradoDTO
                {
                    UserCreated = userCreated,
                    Success = true
                };
            }

            return null;
        }

        public string GetImageUsuario()
        {
            var user = _userAuthenticatedAuthService.GetUserAuthenticated();

            if (user != null)
            {
                var foto = _pessoaService.GetFotoByCpf(user.CpfCnpj).Result;

                return foto;
            }

            return null;
        }

        public string GetImageUsuario(string cpf)
        {
            try
            {
                if (cpf != null)
                {
                    var image = _pessoaService.GetFotoByCpf(cpf).Result;

                    return image;
                }
            } catch(Exception e) { }

            return null;
        }

        public bool ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            var validator = (ValidatorBase<ChangePasswordViewModel>)Activator.CreateInstance(typeof(ChangePasswordViewModelValidator));
            validator.DoValidate(changePasswordViewModel);

            var usuario = _userAuthenticatedAuthService.GetUserAuthenticated();

            //var pessoa = _pessoaFisicaService.FindByCpf(usuario.CpfCnpj);

            if (null == null)
            {
                ContentSingleton.AddMessage("Ops, não conseguimos localizar a pessoa autenticada.");
                ContentSingleton.Dispatch();
            }

            var response = _userAuthService.ChangePassword(new ChangePassword
            {
                //Cpf = pessoa.Cpf,
                Email = usuario.Email,
                Password = changePasswordViewModel.NovaSenha,
                ConfirmPassword = changePasswordViewModel.ConfirmNovaSenha,
                OldPassword = changePasswordViewModel.SenhaAtual
            }).Result;

            if (!response.RequestSuccess && response.Erros != null)
            {
                foreach (string error in response.Erros)
                {
                    ContentSingleton.AddMessage(error);
                }

                ContentSingleton.Dispatch();
            }

            return true;
        }

        public ForgotPasswordDTO ForgotPassword(string cpf)
        {
            var user = _userAuthService.FindByCpf(cpf).Result;

            if (user.CpfCnpj == null)
            {
                ContentSingleton.AddMessage("Ops, não conseguimos localizar um usuário com o cpf informado. Por favor faça um novo cadastro!");
                ContentSingleton.Dispatch();
            }

            var changeCode = _userAuthService.RequestResetPassword(user).Result;

            if (changeCode.Code == null)
            {
                ContentSingleton.AddMessage("Ops, ocorreu um erro no processo. Por favor tente novamente mais tarde.");
                ContentSingleton.Dispatch();
            }

            if (!EnviarEmailRequestResetPassword(user.Email, user.CpfCnpj, changeCode.Code))
            {
                ContentSingleton.AddMessage("Ops, ocorreu um erro no processo. Por favor tente novamente mais tarde.");
                ContentSingleton.Dispatch();
            }

            return new ForgotPasswordDTO
            {
                Success = true,
                Email = user.Email
            };
        }

        public bool ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            var validator = (ValidatorBase<ResetPasswordViewModel>)Activator.CreateInstance(typeof(ResetPasswordViewModelValidator));
            validator.DoValidate(resetPasswordViewModel);

            var user = _userAuthService.FindByCpf(resetPasswordViewModel.Cpf).Result;

            if (user == null)
            {
                ContentSingleton.AddMessage("Ops, não conseguimos localizar um usuário com o cpf informado");
                ContentSingleton.Dispatch();
            }

            var response = _userAuthService.ResetPassword(new UserResetPassword
            {
                User = new UserAuth
                {
                    Email = user.Email,
                    CpfCnpj = user.CpfCnpj,
                    Password = resetPasswordViewModel.novaSenha,
                    ConfirmPassword = resetPasswordViewModel.confirmNovaSenha
                },
                Code = resetPasswordViewModel.Code
            }).Result;

            if (!response.RequestSuccess)
            {
                foreach (string error in response.Erros)
                {
                    ContentSingleton.AddMessage(error);
                }

                ContentSingleton.Dispatch();
            }

            return true;
        }

        private bool EnviarEmailRequestResetPassword(string email, string cpf, string code)
        {
            throw new NotImplementedException("Todo");
            /*
            var template = _notificacaoEmailTemplateRepository.GetNotificacaoTemplateByIdentificador("alterar-senha");

            if (template == null)
            {
                return false;
            }

            var chavesMsg = new Dictionary<string, string>
            {
                {"link",  _apiConfig.Matricula + "/Account/ResetPassword?cpf=" + cpf + "&code=" + WebUtility.UrlEncode(code)}
            };

            return _notificationService.EnviarEmail(email, template.AssuntoModelo, template.TextoModelo, chavesMsg);*/
        }

        private UserCreatedDTO RegistarUsuario(UsuarioViewModel usuarioViewModel)
        {
            UserAuth user = FindUser(usuarioViewModel);

            if (user == null || !user.IsSuccessStatusCode)
            {
                UserCreated UserCreated = _userAuthService.Create(new UserAuth
                {
                    CpfCnpj = usuarioViewModel.Cpf,
                    Password = usuarioViewModel.Senha,
                    ConfirmPassword = usuarioViewModel.ConfirmarSenha,
                    Email = usuarioViewModel.Email
                }).Result;

                if (!UserCreated.UserAuth.IsSuccessStatusCode)
                {
                    ContentSingleton.AddMessage("Ops, ocorreu um erro ao cadastrar o usuário. Por favor, tente novamento mais tarde.");
                    ContentSingleton.Dispatch();
                }

                return new UserCreatedDTO
                {
                    UserAuth = UserCreated.UserAuth,
                    Code = UserCreated.Code
                };
            }

            ContentSingleton.AddMessage("Ops, ocorreu um erro ao tentar cadastrar este usuário, talvez você já possua usuário cadastrado em outros serviços do SESC, caso não se lembre, tente o recurso Esqueci Minha Senha.");
            ContentSingleton.Dispatch();

            return null;
        }

        private UserAuth FindUser(UsuarioViewModel usuarioViewModel)
        {
            return _userAuthService.FindByCpf(usuarioViewModel.Cpf).Result;
        }
    }
}
