using Sesc.Domain.Habilitacao.Entities;
using Sesc.Domain.Habilitacao.Enum;
using System;

namespace Sesc.Domain.Habilitacao.ViewModel
{
    public class SolicitacaoAtendimentoViewModel
    {
        public int SolicitacaoId { get; set; }
        public bool Status { get; set; }
        public string NomeAtendente { get; set; }
        public string EmailAtendente { get; set; }
    }
}
