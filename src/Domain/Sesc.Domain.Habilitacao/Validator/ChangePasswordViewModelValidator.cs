using FluentValidation;
using Sesc.CrossCutting.ServiceAgents.AuthServer.ViewModel;
using Sesc.CrossCutting.Validation.BaseValidation;

namespace Sesc.Domain.Habilitacao.Validator
{
    public class ChangePasswordViewModelValidator : ValidatorBase<ChangePasswordViewModel>
    {
        public override void SetRules()
        {
            RuleFor(c => c.SenhaAtual)
                .NotEmpty().WithMessage("Por favor, informe a senha atual.");
            
            RuleFor(c => c.NovaSenha)
                .NotEmpty().WithMessage("Por favor, informe a nova senha.");

            RuleFor(c => c.ConfirmNovaSenha)
                .NotEmpty().WithMessage("Por favor, confirme a nova senha.")
                .Equal(c => c.NovaSenha)
                .WithMessage("As senhas são incompatíveis.");
        }
    }
}
