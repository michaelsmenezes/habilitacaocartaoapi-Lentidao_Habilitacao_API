using AutoMapper;
using Sesc.Application.ApplicationServices.Queries.Contracts;
using Sesc.CrossCutting.ServiceAgents.AuthServer.Services.Contracts;
using Sesc.CrossCutting.ServiceAgents.Clientela.Services.Contracts;
using Sesc.Domain.Habilitacao.Dto;
using Sesc.Domain.Habilitacao.Entities;
using Sesc.Domain.Habilitacao.Enum;
using Sesc.Domain.Habilitacao.Services.Contracts;
using Sesc.Domain.Habilitacao.ViewModel;
using Sesc.MeuSesc.SharedKernel.Infrastructure.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sesc.Application.ApplicationServices.Queries
{
    public class SolicitacaoQueryServiceApplication : ISolicitacaoQueryServiceApplication
    {
        protected readonly IEnderecoScaService _enderecoScaService;
        private readonly IUserAuthenticatedAuthService _userAuthenticatedAuthService;
        protected readonly IPessoaScaService _pessoaScaService;
        protected readonly ISolicitacaoService _solicitacaoService;
        private readonly IMapper _mapper;

        public SolicitacaoQueryServiceApplication(
            IEnderecoScaService enderecoScaService,
            IUserAuthenticatedAuthService userAuthenticatedAuthService,
            IPessoaScaService pessoaScaService,
            ISolicitacaoService solicitacaoService,
            IMapper mapper
        )
        {
            _enderecoScaService = enderecoScaService;
            _pessoaScaService = pessoaScaService;
            _userAuthenticatedAuthService = userAuthenticatedAuthService;
            _solicitacaoService = solicitacaoService;
            _mapper = mapper;
        }

        public SolicitacaoViewModel GetById(int id)
        {

            var solicitacao = _solicitacaoService.GetById(id).Result;

            if (!_userAuthenticatedAuthService.HasPermissionByCpf(solicitacao.Cpf))
            {
                throw new Exception("O usuário logado deve ser dono da solicitação.");
            }

            return _mapper.Map<SolicitacaoViewModel>(solicitacao);
        }

        public SolicitacaoViewModel GetPessoaCadastrando(string cpf)
        {
            var solicitacao = _solicitacaoService.GetCadastrandoByCpf(cpf).Result;

            var user = _userAuthenticatedAuthService.GetUserAuthenticated();

            if (solicitacao.Cpf != user.CpfCnpj)
            {
                throw new Exception("O usuário logado deve ser dono da solicitação.");
            }

            return _mapper.Map<SolicitacaoViewModel>(solicitacao);
        }

        public IList<SolicitacaoViewModel> GetSolicitacoesPessoaLogada()
        {
            var user = _userAuthenticatedAuthService.GetUserAuthenticated();
            var solicitacoes = _solicitacaoService.GetSolicitacoesByCpf(user.CpfCnpj).Result;

            return _mapper.Map<IList<SolicitacaoViewModel>>(solicitacoes);
        }


        public PagedResult<Solicitacao> GetSolicitacoesAll(SolicitacaoFiltrosViewModel solicitacaoFiltrosVW)
        {
            var solicitacaoFiltrosDto = _mapper.Map<SolicitacaoFiltrosDto>(solicitacaoFiltrosVW);

            return _solicitacaoService.GetSolicitacoesAll(solicitacaoFiltrosDto).Result;

        }

        public SolicitacaoEstatisticasViewModel GetSolicitacoesEstatisticas(SolicitacaoFiltrosViewModel solicitacaoFiltrosVW)
        {
            solicitacaoFiltrosVW.Page = "1";
            solicitacaoFiltrosVW.PageSize = "999999";
            var solicitacoes = this.GetSolicitacoesAll(solicitacaoFiltrosVW).Items;

            var solicitacoesReaisCount = solicitacoes.Count();

            SolicitacaoEstatisticasViewModel viewModelSolicitacoesEstatisticas = new SolicitacaoEstatisticasViewModel();
            
            viewModelSolicitacoesEstatisticas.QtdNovas = solicitacoes
                .Where(x => x.Situacao == SolicitacaoSituacaoEnum.aguardando)
                .Count();
            viewModelSolicitacoesEstatisticas.QtdEmValidacao = solicitacoes
                .Where(x => x.Situacao == SolicitacaoSituacaoEnum.analise)
                .Count();
            viewModelSolicitacoesEstatisticas.QtdEmRetornoSolicitante = solicitacoes
                .Where(x => x.Situacao == SolicitacaoSituacaoEnum.aguardandoretorno)
                .Count();
            viewModelSolicitacoesEstatisticas.QtdRetornadoSolicitante = solicitacoes
                .Where(x => x.Situacao == SolicitacaoSituacaoEnum.retornado)
                .Count();
            viewModelSolicitacoesEstatisticas.QtdAprovadas = solicitacoes
                .Where(x => x.Situacao == SolicitacaoSituacaoEnum.aprovada)
                .Count();
            viewModelSolicitacoesEstatisticas.QtdReprovadas = solicitacoes
                .Where(x => x.Situacao == SolicitacaoSituacaoEnum.reprovada)
                .Count();
            viewModelSolicitacoesEstatisticas.QtdCanceladas = solicitacoes
                .Where(x => x.Situacao == SolicitacaoSituacaoEnum.cancelada)
                .Count();

            viewModelSolicitacoesEstatisticas.PorcentagemNovas = viewModelSolicitacoesEstatisticas.QtdNovas <= 0 ? 0 : viewModelSolicitacoesEstatisticas.QtdNovas * 100 / solicitacoesReaisCount;
            viewModelSolicitacoesEstatisticas.PorcentagemEmValidacao = viewModelSolicitacoesEstatisticas.QtdEmValidacao <= 0 ? 0 : viewModelSolicitacoesEstatisticas.QtdEmValidacao * 100 / solicitacoesReaisCount;
            viewModelSolicitacoesEstatisticas.PorcentagemEmRetornoSolicitante = viewModelSolicitacoesEstatisticas.QtdEmRetornoSolicitante <= 0 ? 0 : viewModelSolicitacoesEstatisticas.QtdEmRetornoSolicitante * 100 / solicitacoesReaisCount;
            viewModelSolicitacoesEstatisticas.PorcentagemRetornadoSolicitante = viewModelSolicitacoesEstatisticas.QtdRetornadoSolicitante <= 0 ? 0 : viewModelSolicitacoesEstatisticas.QtdRetornadoSolicitante * 100 / solicitacoesReaisCount;
            viewModelSolicitacoesEstatisticas.PorcentagemAprovadas = viewModelSolicitacoesEstatisticas.QtdAprovadas <= 0 ? 0 : viewModelSolicitacoesEstatisticas.QtdAprovadas * 100 / solicitacoesReaisCount;
            viewModelSolicitacoesEstatisticas.PorcentagemReprovadas = viewModelSolicitacoesEstatisticas.QtdReprovadas <= 0 ? 0 : viewModelSolicitacoesEstatisticas.QtdReprovadas * 100 / solicitacoesReaisCount;
            viewModelSolicitacoesEstatisticas.PorcentagemCanceladas = viewModelSolicitacoesEstatisticas.QtdCanceladas <= 0 ? 0 : viewModelSolicitacoesEstatisticas.QtdCanceladas * 100 / solicitacoesReaisCount;

            return viewModelSolicitacoesEstatisticas;
        }
    }
}
