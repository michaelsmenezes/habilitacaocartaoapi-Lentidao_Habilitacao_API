using System.ComponentModel.DataAnnotations;

namespace Sesc.Domain.Habilitacao.Enum
{
    public enum SolicitacaoPlataformaEnum
    {
        [Display(Name = "Não Classificado")]
        NaoClassificado = 0,

        [Display(Name = "Mobile")]
        Mobile = 1,
    }
}
