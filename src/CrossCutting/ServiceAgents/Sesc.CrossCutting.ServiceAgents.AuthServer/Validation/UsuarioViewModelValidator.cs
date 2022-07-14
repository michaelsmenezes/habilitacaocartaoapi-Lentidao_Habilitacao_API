using FluentValidation;
using Sesc.CrossCutting.ServiceAgents.AuthServer.ViewModel;
using Sesc.CrossCutting.Validation.BaseValidation;
using Sesc.CrossCutting.Validation.CommonValidation;
using System;

namespace Sesc.CrossCutting.ServiceAgents.AuthServer.Validation
{
    public class UsuarioViewModelValidator : ValidatorBase<UsuarioViewModel>
    {
        public override void SetRules()
        {
            RuleFor(u => u.Cpf)
                  .NotEmpty().WithMessage("Por favor, informe o CPF.")
                  .Custom((cpf, context) =>
                  {
                      if (!CpfValidation.IsCpf(cpf))
                      {
                          context.AddFailure("Informe um CPF válido.");
                      }
                  });

            RuleFor(u => u.Email)
                 .NotEmpty().WithMessage("Por favor, informe o email.")
                 .EmailAddress().WithMessage("Por favor, informe um e-mail válido.");

            RuleFor(u => u.DataNascimento)
                 .NotEmpty().WithMessage("Por favor, informe a data de nascimento.")
                 .Must(dataNascimento => DateTime.Parse(dataNascimento) < DateTime.Now.AddYears(-18))
                    .WithMessage("Para prosseguir com seu cadastro é necessário ser maior de idade.");

            RuleFor(u => u.Senha)
                .NotEmpty().WithMessage("Por favor, informe a senha.");

            RuleFor(u => u.ConfirmarSenha)
                .NotEmpty().WithMessage("Por favor, confirme a senha.")
                .Equal(u => u.Senha)
                .WithMessage("As senhas são incompatíveis.");

        }
    }
}
