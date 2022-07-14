using Sesc.Domain.Habilitacao.Dto;
using Sesc.Domain.Habilitacao.Entities;
using Sesc.Domain.Habilitacao.ViewModel;
using Sesc.MeuSesc.SharedKernel.Infrastructure.Pagination;
using System.Collections.Generic;

namespace Sesc.Application.ApplicationServices.Queries.Contracts
{
    public interface ISolicitacaoQueryServiceApplication : IQueryServiceApplication
    {
        IList<SolicitacaoViewModel> GetSolicitacoesPessoaLogada();
        SolicitacaoViewModel GetById(int id);
        SolicitacaoViewModel GetPessoaCadastrando(string cpf);
        PagedResult<Solicitacao> GetSolicitacoesAll(SolicitacaoFiltrosViewModel solicitacaoFiltrosVW);
        SolicitacaoEstatisticasViewModel GetSolicitacoesEstatisticas(SolicitacaoFiltrosViewModel solicitacaoFiltrosVW);
    }
}
