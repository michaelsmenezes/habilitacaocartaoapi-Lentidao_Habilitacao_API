using System.ComponentModel.DataAnnotations;

namespace Sesc.Domain.Habilitacao.Enum
{
    public enum SolicitacaoTipoEnum
    {
        [Display(Name = "Não Informado")]
        naoinformado = 0,
        [Display(Name = "Nova")]
        nova = 1,
        [Display(Name = "Renovação")]
        renovacao = 2,
        [Display(Name = "Alteração de Dependente")]
        alteracaodependente = 3,
        [Display(Name = "Mudança de Categoria")]
        mudancacategoria = 4,
    }
}
