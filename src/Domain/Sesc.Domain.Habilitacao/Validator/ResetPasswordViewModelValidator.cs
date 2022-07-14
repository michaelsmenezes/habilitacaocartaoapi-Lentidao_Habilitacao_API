using FluentValidation;
using Sesc.CrossCutting.ServiceAgents.AuthServer.ViewModel;
using Sesc.CrossCutting.Validation.BaseValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sesc.Domain.Habilitacao.Validator
{
    public class ResetPasswordViewModelValidator : ValidatorBase<ResetPasswordViewModel>
    {
        public override void SetRules()
        {
            RuleFor(r => r.Code)
                 .NotEmpty().WithMessage("O código para alteração de email não foi informado.");

            RuleFor(r => r.Cpf)
                .NotEmpty().WithMessage("O cpf não foi informado.");

            RuleFor(r => r.novaSenha)
               .NotEmpty().WithMessage("Por favor, informe a nova senha.");

            RuleFor(r => r.confirmNovaSenha)
                .NotEmpty().WithMessage("Por favor, confirme a nova senha.")
                .Equal(r => r.novaSenha)
                .WithMessage("As senhas são incompatíveis.");
        }
    }
}
