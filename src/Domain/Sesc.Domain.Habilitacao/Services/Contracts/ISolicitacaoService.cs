using Sesc.CrossCutting.ServiceAgents.Clientela.Dto;
using Sesc.Domain.Habilitacao.Dto;
using Sesc.Domain.Habilitacao.Entities;
using Sesc.Domain.Habilitacao.Enum;
using Sesc.MeuSesc.SharedKernel.Infrastructure.Pagination;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sesc.Domain.Habilitacao.Services.Contracts
{
    public interface ISolicitacaoService
    {
        Task<IList<SolicitacaoDto>> GetSolicitacoesByCpf(string Cpf);
        Task<Solicitacao> GetById(int id, bool noTrack = false);
        Task<Solicitacao> GetCadastrandoByCpf(string cpf);
        Task<SolicitacaoDto> Incluir(Solicitacao solicitacao);
        Task<SolicitacaoDto> Alterar(Solicitacao solicitacao);
        Task<SolicitacaoDto> Cancelar(SolicitacaoDto solicitacao);
        Task<PagedResult<Solicitacao>> GetSolicitacoesAll(SolicitacaoFiltrosDto SolicitacaoFiltrosVW);
        Task<IList<SolicitacaoDto>> GetSolicitacoesAll();
        Task<Solicitacao> GetByTitular(Titular titular);
        void IncluirDependente(int idSolicitacao, string cpf);
        void SetarRenovacaoDependente(int solicitacaoId, int id, AcaoEnum acao);
        void FinalizarCadastro(SolicitacaoDto solicitacaoNew);
        void EnviarEmailConfirmacao(int id);
        SolicitacaoDto CriarNovaSolicitacaoAlterarDependentes();
        SolicitacaoDto CriarNovaSolicitacaoRenovarCartao();
        SolicitacaoDto CriarNovaSolicitacaoMudancaCategoria();
        bool HasSolicitacaoTipoEmAndamento(SolicitacaoTipoEnum tipo);
        void RemoverRenovacaoDependente(int solicitacaoId, int dependenteId);
    }
}
