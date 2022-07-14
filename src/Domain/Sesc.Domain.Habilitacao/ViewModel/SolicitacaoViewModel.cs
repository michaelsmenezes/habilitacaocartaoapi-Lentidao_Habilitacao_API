using Sesc.Domain.Habilitacao.Enum;
using Sesc.MeuSesc.SharedKernel.Infrastructure.ViewModel;
using System;
using System.Collections.Generic;

namespace Sesc.Domain.Habilitacao.ViewModel
{
    public class SolicitacaoViewModel : ViewModelBase
    {
        public int? Id { get; set; }
        public string Cpf { get; set; }
        public SolicitacaoTipoEnum Tipo { get; set; }
        public DateTime? DataRegistro { get; set; }
        public SolicitacaoSituacaoEnum? Situacao { get; set; }
        public SolicitacaoCategoriaEnum? Categoria { get; set; }
        public TitularViewModel Titular { get; set; }
        public ICollection<AtendimentoViewModel> Atendimentos { get; set; }
    }
}
