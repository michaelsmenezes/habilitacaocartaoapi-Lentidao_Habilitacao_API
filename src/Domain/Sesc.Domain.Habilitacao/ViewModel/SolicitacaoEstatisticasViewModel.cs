using Sesc.MeuSesc.SharedKernel.Infrastructure.ViewModel;

namespace Sesc.Domain.Habilitacao.ViewModel
{
    public class SolicitacaoEstatisticasViewModel : ViewModelBase
    {
        public int QtdNovas { get; set; }
        public int PorcentagemNovas { get; set; }

        public int QtdEmValidacao { get; set; }
        public int PorcentagemEmValidacao { get; set; }

        public int QtdEmRetornoSolicitante { get; set; }
        public int PorcentagemEmRetornoSolicitante { get; set; }

        public int QtdRetornadoSolicitante { get; set; }
        public int PorcentagemRetornadoSolicitante { get; set; }

        public int QtdAprovadas { get; set; }
        public int PorcentagemAprovadas { get; set; }

        public int QtdReprovadas { get; set; }
        public int PorcentagemReprovadas { get; set; }

        public int QtdCanceladas { get; set; }
        public int PorcentagemCanceladas { get; set; }
    }
}
