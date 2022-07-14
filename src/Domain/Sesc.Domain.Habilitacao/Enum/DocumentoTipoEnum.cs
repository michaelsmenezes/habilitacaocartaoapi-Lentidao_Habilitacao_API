using System.ComponentModel.DataAnnotations;

namespace Sesc.Domain.Habilitacao.Enum
{
    public enum QtdDocumentosObrigatorios { titular = 8, conjuge = 5, dependenteNaoConjuge = 4, depedenteFilhoEnteadoNeto = 5 }

    public enum DocumentoTipoEnum
    {
        [Display(Name = "Foto")]
        foto = 1,

        [Display(Name = "CPF")]
        cpf = 2,

        [Display(Name = "Documento de Identificação - Frente")]
        documentoIdentificacaoFrente = 3,

        [Display(Name = "Carteira de Trabalho - Nº Serie")]
        ctpsSerie = 4,

        [Display(Name = "Carteira de Trabalho - CNPJ")]
        ctpsCnpj = 5,

        [Display(Name = "Comprovante de Endereço")]
        comprovanteendereco = 6,

        [Display(Name = "Comprovante de Renda")]
        comprovanteRenda = 7,

        [Display(Name = "Documento Conjuge")]
        comprovanteUniao = 8,

        [Display(Name = "Documento de Identificação - Verso")]
        documentoIdentificacaoVerso = 9,

        [Display(Name = "Declaracao Escolar")]
        declaracaoEscolar = 10,
    }
}
