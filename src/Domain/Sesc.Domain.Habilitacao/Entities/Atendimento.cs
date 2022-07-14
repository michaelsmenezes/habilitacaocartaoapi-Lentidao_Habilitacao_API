using Sesc.Domain.Habilitacao.Enum;
using Sesc.MeuSesc.SharedKernel.Infrastructure.Entities;
using System;

namespace Sesc.Domain.Habilitacao.Entities
{
    public class Atendimento : EntityBase
    {
        public int SolicitacaoId { get; set; }
        public Solicitacao Solicitacao { get; set; }
        public string Nome { get; set; }
        public string Usuario { get; set; }
        public SolicitacaoSituacaoEnum SituacaoSolicitacao { get; set; }
        public string Observacao { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public DateTime? DataHoraFim { get; set; }
    }
}
