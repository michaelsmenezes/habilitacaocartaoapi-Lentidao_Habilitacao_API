using Sesc.Domain.Habilitacao.Dto;
using Sesc.Domain.Habilitacao.Entities;
using Sesc.MeuSesc.SharedKernel.Infrastructure.Pagination;
using Sesc.MeuSesc.SharedKernel.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Sesc.Domain.Habilitacao.Repositories.Contracts
{
    public interface ISolicitacaoRepository : IRepository<Solicitacao>
    {
        Solicitacao GetById(int id, bool noTrack = false);
        IList<Solicitacao> GetSolicitacoesByCpf(string cpf);
        PagedResult<Solicitacao> GetSolicitacoesAll(int page, int pageSize, Expression<Func<Solicitacao, bool>> filter = null);
        IList<Solicitacao> GetSolicitacoesAll();
        Solicitacao Incluir(Solicitacao solicitacao);
        Solicitacao Alterar(Solicitacao solicitacao);
        Solicitacao GetCadastrandoByCpf(string cpf);
    }
}
