using System.ComponentModel.DataAnnotations;

namespace Sesc.Domain.Habilitacao.Enum
{
    public enum DocumentoSituacaoEnum
    {
        [Display(Name = "Aprovado")]
        aprovado = 1,
        [Display(Name = "Reprovado")]
        reprovado = 2,
        [Display(Name = "Aguardando")]
        aguardando = 0
    }
}
