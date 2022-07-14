using Sesc.Domain.Habilitacao.ViewModel;
using System.Threading.Tasks;

namespace Sesc.Application.ApplicationServices.Commands.Contracts
{
    public interface IAtendimentoCommandServiceApplication
    {
        Task<RetornoAtendimentoViewModel> AssociarAtendente(SolicitacaoAtendimentoViewModel SolicitacaoAtendimentoViewModel);
        Task<RetornoAtendimentoViewModel> Finalizar(AtendimentoFinalizarViewModel AtendimentoFinalizarViewModel);
        Task<bool> ReenviarEmail(int solicitacaoId);
    }
}
