using System.ComponentModel.DataAnnotations;

namespace Sesc.Domain.Habilitacao.Enum
{
    public enum SolicitacaoSituacaoEnum
    {
        [Display(Name = "Não Informada")]
        naoinformada = 0,

        [Display(Name = "Em Cadastro")]
        cadastro = 1,

        [Display(Name = "Aguardando Análise")]
        aguardando = 2,

        [Display(Name = "Em Análise")]
        analise = 3,

        [Display(Name = "Aguardando Retorno Cliente")]
        aguardandoretorno = 4,

        [Display(Name = "Reprovada")]
        reprovada = 5,

        [Display(Name = "Aprovada")]
        aprovada = 6,

        [Display(Name = "Cancelada")]
        cancelada = 7,

        [Display(Name = "Retornado Cliente")]
        retornado = 8
    }
}
