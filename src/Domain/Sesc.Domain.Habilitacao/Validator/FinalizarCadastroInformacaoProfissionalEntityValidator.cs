using FluentValidation;
using Sesc.Domain.Habilitacao.Entities;
using Sesc.MeuSesc.SharedKernel.Infrastructure.Validators;
using System;

namespace Sesc.Domain.Habilitacao.Validator
{
    public class FinalizarCadastroInformacaoProfissionalEntityValidator : Validator<InformacaoProfissional>
    {
        public override void SetRules()
        {
            RuleFor(c => c.CNPJ)
                 .NotEmpty().WithMessage("O CNPJ não pode ser vazio.");

            RuleFor(c => c.NomeEmpresa)
                 .NotEmpty().WithMessage("O Nome da Empresa não pode ser vazio.");

            RuleFor(c => c.DataAdmissao)
                 .NotEmpty().WithMessage("A Data de Adminissão não pode ser vazia.");

            RuleFor(c => c.Ocupacao)
                 .NotEmpty().WithMessage("A ocupação não pode ser vazia.");

            RuleFor(c => c.Renda)
                 .NotEmpty().WithMessage("A Renda deve ser informada.");
        }
    }
}
