using System.ComponentModel.DataAnnotations;

namespace Sesc.Domain.Habilitacao.Enum
{
    public enum ParentescoEnum
    {
        [Display(Name = "Cônjuge ou companheiro de união estável")]
        conjuge = 1,

        [Display(Name = "Filho(a)")]
        filho = 2,

        [Display(Name = "Enteado(a)")]
        Enteado = 3,

        [Display(Name = "Pai")]
        pai = 4,

        [Display(Name = "Mãe")]
        mae = 5,

        [Display(Name = "Avós")]
        avos = 6,

        [Display(Name = "Neto")]
        neto = 7,

        [Display(Name = "Pessoa sob guarda")]
        pessoaSobGuarda = 8,

        [Display(Name = "Padrasto")]
        padrasto = 9,

        [Display(Name = "Madrasta")]
        madrasta = 10,

        [Display(Name = "Tutor")]
        tutor = 11,

        [Display(Name = "Viúvo(a)")]
        viuvo = 12,

        [Display(Name = "Irmão(a)")]
        irmao = 13

    }
}
