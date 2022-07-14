using FluentValidation;
using Sesc.Domain.Habilitacao.Entities;
using Sesc.Domain.Habilitacao.Enum;
using Sesc.MeuSesc.SharedKernel.Infrastructure.Validators;
using System;

namespace Sesc.Domain.Habilitacao.Validator
{
    public class FinalizarTitularAlteracaoDependenteEntityValidator : Validator<Titular>
    {
        public override void SetRules()
        {
            RuleFor(t => t.Id)
                 .NotEmpty().WithMessage("Titular da solicitação não encontrado. Por favor, tente atualizar sua página.");
        }
    }
}
