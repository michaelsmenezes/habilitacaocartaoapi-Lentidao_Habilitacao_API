using FluentValidation;
using Sesc.Domain.Habilitacao.Entities;
using Sesc.MeuSesc.SharedKernel.Infrastructure.Validators;

namespace Sesc.Domain.Habilitacao.Validator
{
    public class ChangeSolicitacaoEntityValidator : Validator<Solicitacao>
    {
        public override void SetRules()
        {
            RuleFor(s => s.Id)
                 .NotEmpty().WithMessage("Solicitação não encontrada. Por favor, tente atualizar sua página.");

            RuleFor(s => s.Cpf)
                 .NotEmpty().WithMessage("O cpf não pode ser vazio.");

            RuleFor(s => s.Situacao)
                 .NotEmpty().WithMessage("A situação não pode ser vazia.");

            RuleFor(s => s.Tipo)
                 .NotEmpty().WithMessage("O tipo não pode ser vazio.");

            RuleFor(s => s.Categoria)
                 .NotEmpty().WithMessage("A categoria não pode ser vazia.");

            RuleFor(s => s.DataRegistro)
                 .NotEmpty().WithMessage("A data do registro não pode ser vazia.");

            RuleFor(s => s.Titular)
                .SetValidator(new ChangeTitularEntityValidator());
        }
    }
}
