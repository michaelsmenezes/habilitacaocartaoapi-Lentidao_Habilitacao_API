using Sesc.Domain.Habilitacao.Enum;

namespace Sesc.Domain.Habilitacao.ViewModel
{
    public class AtendimentoFinalizarViewModel
    {
        public int SolicitacaoId { get; set; }
        public SolicitacaoSituacaoEnum Situacao { get; set; }
        public string Observacao { get; set; }
    }
}