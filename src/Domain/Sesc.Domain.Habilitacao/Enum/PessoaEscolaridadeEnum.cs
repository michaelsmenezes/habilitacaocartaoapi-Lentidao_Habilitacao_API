using System.ComponentModel.DataAnnotations;

namespace Sesc.Domain.Habilitacao.Enum
{
    public enum PessoaEscolaridadeEnum
    {
        [Display(Name = "Não Informado")]
        NaoInformado = 0,

        [Display(Name = "Sem escolaridade")]
        SemEscolaridade = 1,

        [Display(Name = "Analfabeto")]
        Analfabeto = 2,

        [Display(Name = "Alfabetizado")]
        Alfabetizado = 3,

        [Display(Name = "Ensino fundamental completo")]
        EnsinoFundamentalCompleto = 4,

        [Display(Name = "Ensino fundamental incompleto")]
        EnsinoFundamentalIncompleto = 5,

        [Display(Name = "Ensino médio completo")]
        EnsinoMedioCompleto = 6,

        [Display(Name = "Ensino médio incompleto")]
        EnsinoMedioIncompleto = 7,

        [Display(Name = "Ensino superior completo")]
        EnsinoSuperiorCompleto = 8,

        [Display(Name = "Ensino superior incompleto")]
        EnsinoSuperiorIncompleto = 9,

        [Display(Name = "Pós - graduação completa")]
        PosGraduacaoCompleta = 10,

        [Display(Name = "Pós - graduação incompleta")]
        PosGraduacaoIncompleta = 11,

        [Display(Name = "Mestrado")]
        Mestrado = 12,

        [Display(Name = "Ensino Técnico")]
        Tecnico = 13,
    }
}
