using FluentValidation;
using Sesc.Domain.Habilitacao.Entities;
using Sesc.MeuSesc.SharedKernel.Infrastructure.Validators;
using System;

namespace Sesc.Domain.Habilitacao.Validator
{
    public class FinalizarCadastroContatoEntityValidator : Validator<Contato>
    {
        public override void SetRules()
        {
            RuleFor(c => c.Logradouro)
                 .NotEmpty().WithMessage("O lograouro não pode ser vazio.");

            RuleFor(c => c.Numero)
                 .NotEmpty().WithMessage("O número não pode ser vazio.");

            RuleFor(c => c.Cep)
                 .NotEmpty().WithMessage("O CEP não pode ser vazio.");

            RuleFor(c => c.Cidade)
                 .NotNull().WithMessage("A cidade não pode ser vazia.");

            RuleFor(c => c.TelefonePrincipal)
                 .NotEmpty().WithMessage("O Telefone Principal não pode ser vazio.");

            RuleFor(c => c.Email)
                 .NotEmpty().WithMessage("O Email não pode ser vazio.")
                 .EmailAddress().WithMessage("E-mail inválido.");
        }
    }
}
