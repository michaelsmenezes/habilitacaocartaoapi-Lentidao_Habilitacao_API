using Microsoft.EntityFrameworkCore;
using Sesc.Domain.Habilitacao.Dto;
using Sesc.Domain.Habilitacao.Entities;
using Sesc.Domain.Habilitacao.Enum;
using Sesc.Domain.Habilitacao.Repositories.Contracts;
using Sesc.MeuSesc.Infrastructure.EntityFramework.Base;
using Sesc.MeuSesc.Infrastructure.EntityFramework.Context;
using Sesc.MeuSesc.SharedKernel.Infrastructure.DataContext;
using Sesc.MeuSesc.SharedKernel.Infrastructure.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Sesc.MeuSesc.Infrastructure.EntityFramework.Persistence.Repositories
{
    public class SolicitacaoRepository : Repository<Solicitacao>, ISolicitacaoRepository
    {
        public SolicitacaoRepository(
            IDataContext context,
            IServiceProvider provider
        ) : base(context)
        {
            _context = context;
        }

        public Solicitacao GetById(int id, bool noTrack = false)
        {
            var solicitacao = _dbSet.Select(x => x)
                .Include(i => i.Titular)
                .Include(i => i.Titular.Dependentes)
                .ThenInclude(t => t.Documentos)
                .Include(i => i.Titular.Responsavel)
                .Include(i => i.Titular.Contato)
                .Include(i => i.Titular.Contato.Cidade).ThenInclude( c => c.CidadeResponsavel)
                .Include(i => i.Titular.Contato.Cidade.Estado)
                //.Include(i => i.Titular.Contato.Cidade.CidadeResponsavel)
                .Include(i => i.Titular.InformacaoProfissional)
                .Include(i => i.Titular.Documentos)
                .Include(i => i.Atendimentos)
                .Where(s => s.Id == id);

            return noTrack == false
                ? solicitacao.FirstOrDefault()
                : solicitacao.AsNoTracking().FirstOrDefault();
        }

        public IList<Solicitacao> GetSolicitacoesByCpf(string cpf)
        {
            return _dbSet.Select(x => x)
                .Include(i => i.Titular)
                .Include(i => i.Titular.Dependentes)
                .ThenInclude(t => t.Documentos)
                .Include(i => i.Titular.Responsavel)
                .Include(i => i.Titular.Contato)
                .Include(i => i.Titular.Contato.Cidade)
                .Include(i => i.Titular.Contato.Cidade.Estado)
                .Include(i => i.Titular.InformacaoProfissional)
                .Include(i => i.Titular.Documentos)
                .Include(i => i.Atendimentos)
                .Where(s => s.Cpf == cpf)
                .OrderByDescending(d => d.Id)
                .ToList();
        }


        public PagedResult<Solicitacao> GetSolicitacoesAll(int page, int pageSize, Expression<Func<Solicitacao, bool>> filter = null)
        {
            var result = this.Select(
                    page,
                    pageSize,
                    filter,
                    x => x.OrderBy(d => d.DataEnvio).ThenBy(d => d.Id),
                    //includes
                    p => p.Titular,
                    p => p.Titular.Dependentes,
                    p => p.Titular.Responsavel,
                    p => p.Titular.Contato,
                    p => p.Titular.Contato.Cidade.CidadeResponsavel,
                    p => p.Titular.Contato.Cidade.Estado,
                    p => p.Titular.InformacaoProfissional,
                    p => p.Titular.Documentos,
                    p => p.Atendimentos);
            return result;
        }

        public IList<Solicitacao> GetSolicitacoesAll()
        {
            var solicitacoes = this.GetAll()
                .ToList();

            return solicitacoes;
        }

        public Solicitacao Incluir(Solicitacao solicitacao)
        {
            SescContext context = (SescContext)_context;

            context.Add(solicitacao);
            context.SaveChanges();

            return solicitacao;
        }

        public Solicitacao Alterar(Solicitacao solicitacao)
        {
            SescContext context = (SescContext)_context;

            context.Update(solicitacao);
            context.SaveChanges();

            return solicitacao;
        }

        public Solicitacao GetCadastrandoByCpf(string cpf)
        {
            var solicitacao = this.GetAllInclude(p =>
                    p.Titular,
                    p => p.Titular.Dependentes,
                    p => p.Titular.Responsavel,
                    p => p.Titular.Contato,
                    p => p.Titular.Contato.Cidade,
                    p => p.Titular.Contato.Cidade.Estado,
                    p => p.Titular.InformacaoProfissional
                )
                .Where(s => s.Titular.Cpf == cpf)
                .Where(s => s.Situacao == SolicitacaoSituacaoEnum.cadastro || s.Situacao == SolicitacaoSituacaoEnum.aguardandoretorno)
                .FirstOrDefault();

            return solicitacao;
        }
    }
}
