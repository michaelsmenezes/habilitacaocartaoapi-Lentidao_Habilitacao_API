using AutoMapper;
using Sesc.Application.ApplicationServices.Commands.Contracts;
using Sesc.CrossCutting.ServiceAgents.AuthServer.Services.Contracts;
using Sesc.CrossCutting.ServiceAgents.Clientela.Services.Contracts;
using Sesc.CrossCutting.Validation.BaseException;
using Sesc.Domain.Habilitacao.Enum;
using Sesc.Domain.Habilitacao.Services.Contracts;
using Sesc.Domain.Habilitacao.ViewModel;
using System;

namespace Sesc.Application.ApplicationServices.Commands
{
    public class DependenteCommandServiceApplication : IDependenteCommandServiceApplication
    {
        protected readonly IPessoaScaService _pessoaScaService;
        protected readonly IDependenteService _dependenteService;
        protected readonly ISolicitacaoService _solicitacaoService;
        private readonly IUserAuthenticatedAuthService _userAuthenticatedAuthService;
        private readonly IMapper _mapper;

        public DependenteCommandServiceApplication(
            IPessoaScaService pessoaScaService,
            IDependenteService dependenteService,
            ISolicitacaoService solicitacaoService,
            IUserAuthenticatedAuthService userAuthenticatedAuthService,
            IMapper mapper
        ) {
            _pessoaScaService = pessoaScaService;
            _dependenteService = dependenteService;
            _solicitacaoService = solicitacaoService;
            _userAuthenticatedAuthService = userAuthenticatedAuthService;
            _mapper = mapper;
        }

        public void CancelarInclusaoDependente(int idSolicitacao, DependenteViewModel dependenteViewModel)
        {
            try
            {
                var dependente = _dependenteService.GetById((int)dependenteViewModel.Id).Result;

                // check situation of Solicitacao, to allow or not alter this one
                var solicitacao = _solicitacaoService.GetById(idSolicitacao).Result;

                if (solicitacao.Situacao != SolicitacaoSituacaoEnum.cadastro &&
                    solicitacao.Situacao != SolicitacaoSituacaoEnum.aguardando &&
                    solicitacao.Situacao != SolicitacaoSituacaoEnum.aguardandoretorno
                ) {
                    throw new Exception("A sua solicitação já está em analise e não pode ser alterada.");
                }

                if (dependente.Acao == AcaoEnum.Inclusao)
                {
                    _dependenteService.Deletar(dependente);
                }
                else
                {
                    dependente.Acao = dependente.Acao == AcaoEnum.Exclusao ? AcaoEnum.SemAlteracao : AcaoEnum.Exclusao;
                    _dependenteService.Alterar(dependente);
                }
            }
            catch (ApiException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                if (e.InnerException != null) throw e.InnerException;

                ContentSingleton.AddMessage(e.Message);
                ContentSingleton.Dispatch();
            }
        }

        public void RemoverRenovacaoDependente(int idSolicitacao, int dependenteId)
        {
            try
            {
                _solicitacaoService.RemoverRenovacaoDependente(idSolicitacao, dependenteId);
            }
            catch (ApiException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                if (e.InnerException != null) throw e.InnerException;

                ContentSingleton.AddMessage(e.Message);
                ContentSingleton.Dispatch();
            }
        }

        public void RemoverDependenteAtivo(int idSolicitacao, string cpf)
        {
            try
            {
                _solicitacaoService.IncluirDependente(idSolicitacao, cpf);
            }
            catch (ApiException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                if (e.InnerException != null) throw e.InnerException;

                ContentSingleton.AddMessage(e.Message);
                ContentSingleton.Dispatch();
            }
        }

        public void SetarRenovacaoDependente(int solicitacaoId, int id, AcaoEnum acao)
        {
            try
            {
                _solicitacaoService.SetarRenovacaoDependente(solicitacaoId, id, acao);
            }
            catch (ApiException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                if (e.InnerException != null) throw e.InnerException;

                ContentSingleton.AddMessage(e.Message);
                ContentSingleton.Dispatch();
            }
        }
    }
}