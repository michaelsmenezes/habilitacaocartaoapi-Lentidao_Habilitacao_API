using FluentValidation;
using Sesc.Domain.Habilitacao.Entities;
using Sesc.Domain.Habilitacao.Enum;
using Sesc.MeuSesc.SharedKernel.Infrastructure.Validators;
using System;

namespace Sesc.Domain.Habilitacao.Validator
{
    public class FinalizarCadastroDependenteEntityValidator : Validator<Dependente>
    {
        public override void SetRules()
        {
            RuleFor(t => t.Id)
                 .NotEmpty().WithMessage("Dependente não encontrado. Por favor, tente atualizar sua página.");

            RuleFor(t => t.Nome)
                 .NotEmpty().WithMessage("O nome do dependente pode ser vazio.");

            RuleFor(t => t.Sexo)
                 .NotEmpty().WithMessage("O sexo do dependente não pode ser vazio.");

            RuleFor(t => t.DataNascimento)
                .NotEmpty().WithMessage("A data de nascimento do dependente não pode ser vazia.");

            RuleFor(t => t.Cpf)
                 .NotEmpty().WithMessage("O CPF do dependente não pode ser vazio.");

            RuleFor(t => t.NomeMae)
                 .NotEmpty().WithMessage("O nome da mãe do dependente não pode ser vazio.");

            RuleFor(t => t.Parentesco)
                 .NotEmpty().WithMessage("O parentesco do dependente não pode ser vazio.")
                 .IsInEnum().WithMessage("Parentesco do dependente está inválido.");

            RuleFor(t => t.Escolaridade)
                 .NotEmpty().WithMessage("A escolaridade do dependente não pode ser vazia.")
                 .IsInEnum().WithMessage("Escolaridade do dependente está inválida.");

            RuleFor(t => t.EstadoCivil)
                 .NotEmpty().WithMessage("O estado civil do dependente não pode ser vazio.")
                 .IsInEnum().WithMessage("Estado civil do dependente está inválido.");

            RuleFor(t => t.Nacionalidade)
                 .NotEmpty().WithMessage("A nacionalidade do dependente não pode ser vazia.");

            RuleFor(t => t.Naturalidade)
                 .NotEmpty().WithMessage("A naturalidade do dependente não pode ser vazia.");

            RuleFor(t => t.TipoDocumento)
                 .NotEmpty().WithMessage("O tipo do documento do dependente não pode ser vazio.")
                 .IsInEnum().WithMessage("Tipo de documento do dependente enviado não foi solicitado.");

            RuleFor(t => t.Numero)
                 .NotEmpty().WithMessage("O número do documento do dependente não pode ser vazio.");

            RuleFor(t => t.OrgaoEmissor)
                 .NotEmpty().WithMessage("O orgão emissor do documetno do dependente não pode ser vazio.");

            RuleFor(t => t.DataEmissao)
                 .NotEmpty().WithMessage("A data de emissão do documento do dependente não pode ser vazia.");

            RuleForEach(t => t.Documentos)
                .SetValidator(new FinalizarCadastroDocumentoEntityValidator());
        }
    }
}
