using Microsoft.AspNetCore.Http;
using Sesc.Domain.Habilitacao.ViewModel;

namespace Sesc.Application.ApplicationServices.Commands.Contracts
{
    public interface ISolicitacaoCommandServiceApplication
    {
        SolicitacaoViewModel Salvar(SolicitacaoViewModel solicitacaoViewModel);
        SolicitacaoViewModel Cancelar(SolicitacaoViewModel solicitacaoViewModel);
        SolicitacaoViewModel FinalizarCadastro(SolicitacaoViewModel solicitacaoViewModel);
        SolicitacaoViewModel NovaSolicitacaoAlterarDependentes();
        SolicitacaoViewModel NovaSolicitacaRenovarCartao();
        SolicitacaoViewModel NovaSolicitacaoMudancaCategoria();
    }
}
