using FluentValidation;
using Sesc.CrossCutting.ServiceAgents.AuthServer.ViewModel;
using Sesc.CrossCutting.Validation.BaseValidation;

namespace Sesc.Domain.Habilitacao.Validator
{
    public class ChangeEmailViewModelValidator : ValidatorBase<ChangeEmailViewModel>
    {
        public override void SetRules()
        {
            RuleFor(e => e.NovoEmail)
                 .NotEmpty().WithMessage("Por favor, informe o nove email.")
                 .EmailAddress().WithMessage("Por favor, informe um novo email válido.");

            RuleFor(e => e.ConfirmNovoEmail)
                .NotEmpty().WithMessage("Por favor, confirme o novo email.")
                .EmailAddress().WithMessage("O email confirmado é inválido.")
                .Equal(e => e.NovoEmail)
                .WithMessage("Os emails são incompatíveis.");
        }
    }
}
