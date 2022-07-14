using AutoMapper;
using Sesc.Application.ApplicationServices.Commands.Contracts;
using Sesc.CrossCutting.ServiceAgents.AuthServer.Services.Contracts;
using Sesc.CrossCutting.ServiceAgents.Clientela.Services.Contracts;
using Sesc.CrossCutting.Validation.BaseException;
using Sesc.Domain.Habilitacao.Dto;
using Sesc.Domain.Habilitacao.Entities;
using Sesc.Domain.Habilitacao.Enum;
using Sesc.Domain.Habilitacao.Repositories.Contracts;
using Sesc.Domain.Habilitacao.Services.Contracts;
using Sesc.Domain.Habilitacao.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sesc.Application.ApplicationServices.Commands
{
    public class SolicitacaoCommandServiceApplication : ISolicitacaoCommandServiceApplication
    {
        private readonly IPessoaScaService _pessoaScaService;
        private readonly ISolicitacaoService _solicitacaoService;
        private readonly IUserAuthenticatedAuthService _userAuthenticatedAuthService;
        private readonly IMapper _mapper;
        private readonly ICidadeRepository _cidadeRepository;

        public SolicitacaoCommandServiceApplication(
            IPessoaScaService pessoaScaService,
            ISolicitacaoService solicitacaoService,
            IUserAuthenticatedAuthService userAuthenticatedAuthService,
            ICidadeRepository cidadeRepository,
            IMapper mapper
        )
        {
            _pessoaScaService = pessoaScaService;
            _solicitacaoService = solicitacaoService;
            _cidadeRepository = cidadeRepository;
            _userAuthenticatedAuthService = userAuthenticatedAuthService;
            _mapper = mapper;
        }

        public SolicitacaoViewModel Salvar(SolicitacaoViewModel solicitacaoViewModel)
        {
            try
            {
                var solicitacao = _mapper.Map<Solicitacao>(solicitacaoViewModel);

                SolicitacaoViewModel response = null;

                if (solicitacao != null && solicitacao.Titular != null)
                {
                    response = solicitacao.Id > 0
                        ? Alterar(solicitacao)
                        : Incluir(solicitacao);
                }
                else
                {
                    throw new Exception("Ocorreu um erro interno, por favor verifique se todos os dados informados estão digitados corretamente.");
                }

                return response;
            }
            catch (ApiException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                if (e.InnerException is ApiException) throw e.InnerException;
                if (e.InnerException != null)
                {
                    ContentSingleton.AddMessage(e.InnerException.Message);
                    ContentSingleton.Dispatch();
                }

                ContentSingleton.AddMessage(e.Message);
                ContentSingleton.Dispatch();
            }

            return solicitacaoViewModel;
        }

        private SolicitacaoViewModel Incluir(Solicitacao solicitacao)
        {

            var user = _userAuthenticatedAuthService.GetUserAuthenticated();

            var pessoaSca = _pessoaScaService.GetPessoa(user.CpfCnpj);

            if (pessoaSca != null)
            {
                throw new Exception("O CPF informado já possui um Cartão Sesc Válido, verifique suas solicitações em aberto.");
            }

            solicitacao.Cpf = user.CpfCnpj;
            solicitacao.DataRegistro = DateTime.Now;
            solicitacao.Situacao = SolicitacaoSituacaoEnum.cadastro;
            solicitacao.Titular.ContatoId = null;
            solicitacao.Titular.Contato = null;
            solicitacao.Titular.InformacaoProfissionalId = null;
            solicitacao.Titular.InformacaoProfissional = null;

            var solicitacaoDto = _solicitacaoService.Incluir(solicitacao).Result;

            return _mapper.Map<SolicitacaoViewModel>(solicitacaoDto);
        }

        private SolicitacaoViewModel Alterar(Solicitacao solicitacaoNew)
        {
            var solicitacaoAtual = _solicitacaoService.GetById(solicitacaoNew.Id).Result;

            if (solicitacaoAtual == null)
            {
                throw new Exception("Ocorreu um erro durante esta operação, por favor, tente reiniciar a operação.");
            }

            solicitacaoNew.ChangeSolicitacao(solicitacaoNew);
            solicitacaoAtual.Titular.ChangeTitular(solicitacaoNew.Titular);

            FillContato(solicitacaoAtual.Titular, solicitacaoNew.Titular.Contato);
            FillInformacaoProfissional(solicitacaoAtual.Titular, solicitacaoNew.Titular.InformacaoProfissional);
            AdicionarDependente(solicitacaoAtual.Titular, solicitacaoNew.Titular.Dependentes);

            var solicitacaoDto = _solicitacaoService.Alterar(solicitacaoAtual).Result;
            return _mapper.Map<SolicitacaoViewModel>(solicitacaoDto);
        }

        private void FillContato(Titular titularAtual, Contato contatoNew)
        {
            if (contatoNew == null ||
                String.IsNullOrEmpty(contatoNew.Cep) ||
                contatoNew.Cidade == null ||
                contatoNew.Cidade.Estado == null
            )
            {
                titularAtual.ContatoId = null;
                titularAtual.Contato = null;

                return;
            }

            Cidade cidadeEstado = null;

            if (!string.IsNullOrEmpty(contatoNew.Cidade.CodigoIBGE))
            {
                cidadeEstado = _cidadeRepository
                    .FindByInclude(
                        c => c.CodigoIBGE == contatoNew.Cidade.CodigoIBGE,
                        e => e.Estado
                    )
                    .FirstOrDefault();
            }

            if (titularAtual.Contato != null && titularAtual.Contato.Id > 0)
            {
                contatoNew.Cep = contatoNew.Cep.Replace("-", "");
                contatoNew.CidadeId = cidadeEstado?.Id;
                titularAtual.Email = contatoNew?.Email ?? titularAtual.Email;
                titularAtual.Contato.ChangeContato(contatoNew);
            }
            else
            {
                titularAtual.Contato = new Contato
                {
                    Cep = contatoNew.Cep.Replace("-", ""),
                    Logradouro = contatoNew.Logradouro,
                    Complemento = contatoNew.Complemento,
                    Numero = contatoNew.Numero,
                    Bairro = contatoNew.Bairro,
                    CidadeId = cidadeEstado.Id,
                    TelefonePrincipal = contatoNew.TelefonePrincipal,
                    TelefoneSecundario = contatoNew.TelefoneSecundario,
                    Email = contatoNew?.Email ?? titularAtual.Email
            };
            }
        }

        private void FillInformacaoProfissional(
            Titular titularAtual,
            InformacaoProfissional informacaoProfissionalNew
        )
        {
            if (informacaoProfissionalNew == null ||
                String.IsNullOrEmpty(informacaoProfissionalNew.CNPJ)
            )
            {
                titularAtual.InformacaoProfissionalId = null;
                titularAtual.InformacaoProfissional = null;

                return;
            }

            if (titularAtual.InformacaoProfissional != null && titularAtual.InformacaoProfissional.Id > 0)
            {
                informacaoProfissionalNew.CNPJ = informacaoProfissionalNew.CNPJ.Replace("-", "").Replace("/", "").Replace(".", "");
                titularAtual.InformacaoProfissional.ChangeInformacaoProfissional(informacaoProfissionalNew);
            }
            else
            {
                titularAtual.InformacaoProfissional = new InformacaoProfissional
                {
                    CNPJ = informacaoProfissionalNew.CNPJ,
                    NomeEmpresa = informacaoProfissionalNew.NomeEmpresa,
                    DataAdmissao = informacaoProfissionalNew.DataAdmissao,
                    DataDemissao = informacaoProfissionalNew.DataDemissao,
                    Ocupacao = informacaoProfissionalNew.Ocupacao,
                    NumeroCTPS = informacaoProfissionalNew.NumeroCTPS,
                    SerieCTPS = informacaoProfissionalNew.SerieCTPS,
                    Renda = informacaoProfissionalNew.Renda
                };
            }
        }

        private void AdicionarDependente(Titular titularAtual, ICollection<Dependente> dependentesNew)
        {
            if (dependentesNew == null || dependentesNew.Count <= 0)
            {
                titularAtual.Dependentes = null;
                return;
            }

            dependentesNew.ToList().ForEach(dependente =>
            {
                if (dependente.Id == 0)
                {
                    dependente.Cpf = dependente.Cpf.Replace(".", "").Replace("-", "");

                    var pessoaSca = _pessoaScaService.GetPessoa(dependente.Cpf);

                    if (pessoaSca != null && pessoaSca.dtvencto >= DateTime.Now)
                    {
                        throw new Exception("O CPF informado já possui um Cartão Sesc Válido, só poderá adicionar como dependente após o vencimento do Cartão.");
                    }

                    if (titularAtual.Dependentes.Any(x => x.Cpf == dependente.Cpf))
                    {
                        throw new Exception("O CPF informado já consta na sua lista de dependentes.");
                    }

                    dependente.Id = 0;
                    dependente.TitularId = titularAtual.Id;
                    dependente.Titular = titularAtual;
                    dependente.Acao = AcaoEnum.Inclusao;
                    dependente.Email = (String.IsNullOrWhiteSpace(dependente.Email) ? titularAtual.Email : dependente.Email);
                    
                    titularAtual.Dependentes.Add(dependente);
                }
            });
        }

        public SolicitacaoViewModel Cancelar(SolicitacaoViewModel solicitacaoVw)
        {
            try
            {
                var solicitacaoDto = _mapper.Map<SolicitacaoDto>(solicitacaoVw);

                solicitacaoDto = _solicitacaoService.Cancelar(solicitacaoDto).Result;

                if (
                    solicitacaoDto.Titular.Contato != null &&
                    !string.IsNullOrEmpty(solicitacaoDto.Titular.Contato.Email)
                )
                {
                    _solicitacaoService.EnviarEmailConfirmacao((int)solicitacaoVw.Id);
                }

                return _mapper.Map<SolicitacaoViewModel>(solicitacaoDto);
            }
            catch (ApiException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                if (e.InnerException is ApiException) throw e.InnerException;
                if (e.InnerException != null)
                {
                    ContentSingleton.AddMessage(e.InnerException.Message);
                    ContentSingleton.Dispatch();
                }

                ContentSingleton.AddMessage(e.Message);
                ContentSingleton.Dispatch();

                return null;
            }
        }

        public SolicitacaoViewModel FinalizarCadastro(SolicitacaoViewModel solicitacaoViewModel)
        {
            try
            {
                var solicitacaoDto = _mapper.Map<SolicitacaoDto>(solicitacaoViewModel);

                _solicitacaoService.FinalizarCadastro(solicitacaoDto);

                _solicitacaoService.EnviarEmailConfirmacao((int)solicitacaoViewModel.Id);

                return solicitacaoViewModel;
            }
            catch (ApiException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                if (e.InnerException is ApiException) throw e.InnerException;
                if (e.InnerException != null)
                {
                    ContentSingleton.AddMessage(e.InnerException.Message);
                    ContentSingleton.Dispatch();
                }

                ContentSingleton.AddMessage(e.Message);
                ContentSingleton.Dispatch();
            }

            return solicitacaoViewModel;
        }

        public SolicitacaoViewModel NovaSolicitacaoAlterarDependentes()
        {
            try
            {
                var solicitacaoDto = _solicitacaoService.CriarNovaSolicitacaoAlterarDependentes();

                return _mapper.Map<SolicitacaoViewModel>(solicitacaoDto);
            }
            catch (ApiException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                if (e.InnerException is ApiException) throw e.InnerException;
                if (e.InnerException != null)
                {
                    ContentSingleton.AddMessage(e.InnerException.Message);
                    ContentSingleton.Dispatch();
                }

                ContentSingleton.AddMessage(e.Message);
                ContentSingleton.Dispatch();
            }

            return null;
        }

        public SolicitacaoViewModel NovaSolicitacaRenovarCartao()
        {
            try
            {
                var solicitacaoDto = _solicitacaoService.CriarNovaSolicitacaoRenovarCartao();

                return _mapper.Map<SolicitacaoViewModel>(solicitacaoDto);
            }
            catch (ApiException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                if (e.InnerException is ApiException) throw e.InnerException;
                if (e.InnerException != null)
                {
                    ContentSingleton.AddMessage(e.InnerException.Message);
                    ContentSingleton.Dispatch();
                }

                ContentSingleton.AddMessage(e.Message);
                ContentSingleton.Dispatch();
            }

            return null;
        }

        public SolicitacaoViewModel NovaSolicitacaoMudancaCategoria()
        {
            try
            {
                var solicitacaoDto = _solicitacaoService.CriarNovaSolicitacaoMudancaCategoria();

                return _mapper.Map<SolicitacaoViewModel>(solicitacaoDto);
            }
            catch (ApiException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                if (e.InnerException is ApiException) throw e.InnerException;
                if (e.InnerException != null)
                {
                    ContentSingleton.AddMessage(e.InnerException.Message);
                    ContentSingleton.Dispatch();
                }

                ContentSingleton.AddMessage(e.Message);
                ContentSingleton.Dispatch();
            }

            return null;
        }
    }
}