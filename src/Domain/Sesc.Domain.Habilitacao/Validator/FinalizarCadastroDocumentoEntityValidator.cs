using FluentValidation;
using Sesc.Domain.Habilitacao.Entities;
using Sesc.MeuSesc.SharedKernel.Infrastructure.Validators;
using System;

namespace Sesc.Domain.Habilitacao.Validator
{
    public class FinalizarCadastroDocumentoEntityValidator : Validator<Documento>
    {
        public override void SetRules()
        {
            RuleFor(t => t.Id)
                 .NotEmpty().WithMessage("Documento não encontrado.");

            RuleFor(t => t.Tipo)
                 .NotEmpty().WithMessage("O tipo do documento não pode ser vazio.")
                 .IsInEnum().WithMessage("O tipo do documento não foi reconhecido como válido.");

            RuleFor(t => t.Extensao)
                 .NotEmpty().WithMessage("A extensão do documento não foi encontrada.");

            RuleFor(t => t.Url)
                 .NotEmpty().WithMessage(m => "O documento "+ m.Nome +" ainda não foi enviado para o servidor, tente enviá-lo novamente por favor.");

            RuleFor(t => t.MimeType)
                 .NotEmpty().WithMessage("O tipo do cabeçalho do documento não foi identificado.");

            RuleFor(t => t.Nome)
                 .NotEmpty().WithMessage("O nome do documento não foi identificado.");

            RuleFor(t => t.DataRegistro)
                 .NotEmpty().WithMessage("A data de registro do documento deve ser informada.");

            RuleFor(t => t.Situacao)
                 .IsInEnum().WithMessage("A situação do documento é inválida.");
        }
    }
}
