using FluentValidation;
using Sesc.Domain.Habilitacao.Entities;
using Sesc.Domain.Habilitacao.Enum;
using Sesc.MeuSesc.SharedKernel.Infrastructure.Validators;
using System;

namespace Sesc.Domain.Habilitacao.Validator
{
    public class FinalizarCadastroTitularEntityValidator : Validator<Titular>
    {
        public override void SetRules()
        {
            RuleFor(t => t.Id)
                 .NotEmpty().WithMessage("Titular da solicitação não encontrado. Por favor, tente atualizar sua página.");

            RuleFor(t => t.Nome)
                 .NotEmpty().WithMessage("O nome não pode ser vazio.");

            RuleFor(t => t.Sexo)
                 .NotEmpty().WithMessage("O sexo não pode ser vazio.");

            RuleFor(t => t.DataNascimento)
                .NotEmpty().WithMessage("A data de nascimento não pode ser vazia.")
                .Must(dataNascimento => {
                    var today = DateTime.Today;
                    var age = today.Year - dataNascimento.Year;
                    if (dataNascimento.Date > today.AddYears(-age)) age--;

                    return age >= 14;
                })
                .WithMessage("A idade mínima para o Titular é 14 anos.");

            RuleFor(t => t.Cpf)
                 .NotEmpty().WithMessage("O cpf não pode ser vazio.");

            RuleFor(t => t.NomeMae)
                 .NotEmpty().WithMessage("O nome da mãe não pode ser vazio.");

            RuleFor(t => t.Escolaridade)
                 .NotEmpty().WithMessage("A escolaridade não pode ser vazia.")
                 .IsInEnum().WithMessage("Escolaridade inválida.");

            RuleFor(t => t.EstadoCivil)
                 .NotEmpty().WithMessage("O estado civil não pode ser vazio.")
                 .IsInEnum().WithMessage("Estado civil inválido.");

            RuleFor(t => t.Nacionalidade)
                 .NotEmpty().WithMessage("A nacionalidade não pode ser vazia.");

            RuleFor(t => t.Naturalidade)
                 .NotEmpty().WithMessage("A naturalidade não pode ser vazia.");

            RuleFor(t => t.TipoDocumento)
                 .NotEmpty().WithMessage("O tipo do documento não pode ser vazio.")
                 .IsInEnum().WithMessage("Tipo de documento enviado não foi solicitado.");

            RuleFor(t => t.Numero)
                 .NotEmpty().WithMessage("O número não pode ser vazio.");

            RuleFor(t => t.OrgaoEmissor)
                 .NotEmpty().WithMessage("O orgão emissor não pode ser vazio.");

            RuleFor(t => t.DataEmissao)
                 .NotEmpty().WithMessage("A data de emissão não pode ser vazia.");

            RuleFor(s => s.Contato)
                .SetValidator(new FinalizarCadastroContatoEntityValidator());

            RuleFor(s => s.InformacaoProfissional)
                .SetValidator(new FinalizarCadastroInformacaoProfissionalEntityValidator());

            RuleForEach(s => s.Documentos)
                .SetValidator(new FinalizarCadastroDocumentoEntityValidator());
        }
    }
}
