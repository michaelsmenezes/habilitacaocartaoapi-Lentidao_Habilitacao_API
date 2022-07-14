using System.ComponentModel.DataAnnotations;

namespace Sesc.Domain.Habilitacao.Enum
{
    public enum AcaoEnum
    {
        [Display(Name = "Inclusão")]
        Inclusao = 1,

        [Display(Name = "Exclusão")]
        Exclusao = 2,

        [Display(Name = "Renovação")]
        Renovacao = 3,

        [Display(Name = "Não Renovar")]
        NaoRenovar = 4,

        [Display(Name = "Sem Alteração")]
        SemAlteracao = 5
    }
}
