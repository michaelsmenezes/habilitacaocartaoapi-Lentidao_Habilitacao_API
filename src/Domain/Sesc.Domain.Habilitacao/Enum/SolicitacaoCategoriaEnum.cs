using System.ComponentModel.DataAnnotations;

namespace Sesc.Domain.Habilitacao.Enum
{
    public enum SolicitacaoCategoriaEnum
    {
        [Display(Name = "Trabalhador do Comércio")]
        Comerciario = 1,

        [Display(Name = "Dependente de Trabalhador do Comércio")]
        DependenteComerciario = 2,

        [Display(Name = "Público Geral")]
        PublicoGeral = 4,
    }
}
