using Sesc.Domain.Habilitacao.Enum;
using System;

namespace Sesc.Domain.Habilitacao.ViewModel
{
    public class AtendimentoViewModel
    {
        public int Id { get; set; }
        public int SolicitacaoId { get; set; }
        public SolicitacaoViewModel Solicitacao { get; set; }
        public string Nome { get; set; }
        public string Usuario { get; set; }
        public SolicitacaoSituacaoEnum SituacaoSolicitacao { get; set; }
        public string Observacao { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public DateTime? DataHoraFim { get; set; }
    }
}