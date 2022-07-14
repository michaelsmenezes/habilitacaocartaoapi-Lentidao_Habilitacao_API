using Sesc.Domain.Habilitacao.Enum;
using Sesc.Domain.Habilitacao.ViewModel;

namespace Sesc.Application.ApplicationServices.Commands.Contracts
{
    public interface IDependenteCommandServiceApplication
    {
        void CancelarInclusaoDependente(int idSolicitacao, DependenteViewModel dependenteViewModel);
        void RemoverDependenteAtivo(int idSolicitacao, string cpf);
        void SetarRenovacaoDependente(int solicitacaoId, int id, AcaoEnum acao);
        void RemoverRenovacaoDependente(int idSolicitacao, int dependenteId);
    }
}
