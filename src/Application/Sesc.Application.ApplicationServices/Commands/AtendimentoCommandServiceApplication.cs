using AutoMapper;
using Sesc.Application.ApplicationServices.Commands.Contracts;
using Sesc.CrossCutting.Validation.BaseException;
using Sesc.Domain.Habilitacao.Entities;
using Sesc.Domain.Habilitacao.Enum;
using Sesc.Domain.Habilitacao.Services.Contracts;
using Sesc.Domain.Habilitacao.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sesc.Application.ApplicationServices.Commands
{
    public class AtendimentoCommandServiceApplication : IAtendimentoCommandServiceApplication
    {
        private readonly IAtendimentoService _atendimentoService;
        private readonly ISolicitacaoService _solicitacaoService;
        private readonly IDocumentoService _documentoService;
        private readonly IMapper _mapper;

        public AtendimentoCommandServiceApplication(
            IAtendimentoService atendimentoService,
            ISolicitacaoService solicitacaoService,
            IDocumentoService documentoService,
            IMapper mapper
        )
        {
            _atendimentoService = atendimentoService;
            _solicitacaoService = solicitacaoService;
            _documentoService = documentoService;
            _mapper = mapper;
        }

        public async Task<RetornoAtendimentoViewModel> AssociarAtendente(SolicitacaoAtendimentoViewModel SolicitacaoAtendimentoViewModel)
        {
            try
            {
                var solicitacao = await _solicitacaoService.GetById(SolicitacaoAtendimentoViewModel.SolicitacaoId);

                var list = new List<SolicitacaoSituacaoEnum> {
                    SolicitacaoSituacaoEnum.aprovada,
                    SolicitacaoSituacaoEnum.cancelada,
                    SolicitacaoSituacaoEnum.reprovada
                };

                if (list.Contains(solicitacao.Situacao))
                {
                    return new RetornoAtendimentoViewModel
                    {
                        Status = false,
                        Mensagem = "Atendimento finalizado não pode ser alterado!"
                    };
                }

                var atendimento = new Atendimento();

                if (SolicitacaoAtendimentoViewModel.Status)
                {
                    var atedimentosNaoFinalizados = solicitacao.Atendimentos?.Where(x => x.DataHoraFim == null);

                    if (atedimentosNaoFinalizados.Count() > 0)
                        throw new Exception("Você não pode pegar essa solicitação porque ela ja possui um atendente.");

                    // Altera o situação da solicitação para em análise
                    solicitacao.Situacao = SolicitacaoSituacaoEnum.analise;

                    // Registra o Atendimento
                    atendimento.Id = 0;
                    atendimento.Nome = SolicitacaoAtendimentoViewModel.NomeAtendente;
                    atendimento.Usuario = SolicitacaoAtendimentoViewModel.EmailAtendente;
                    atendimento.SolicitacaoId = SolicitacaoAtendimentoViewModel.SolicitacaoId;
                    atendimento.SituacaoSolicitacao = SolicitacaoSituacaoEnum.analise;
                    atendimento.DataHoraInicio = DateTime.Now;
                    atendimento.DataHoraFim = null;
                }
                else
                {
                    var historicoAtendimentos = _atendimentoService
                        .FindBy(x => x.SolicitacaoId == SolicitacaoAtendimentoViewModel.SolicitacaoId)
                        .Select(x => x)
                        .OrderByDescending(o => o.Id)
                        .ToList();
                    atendimento = historicoAtendimentos.First();


                    // Verifica o atendimento ja havia outra situação diferente de analise
                    var atendimentoValidacao = historicoAtendimentos
                        .Select(x => x)
                        .Where(x => x.SituacaoSolicitacao != SolicitacaoSituacaoEnum.analise)
                        .OrderByDescending(x => x.Id);

                    if (atendimentoValidacao.Count() > 0)
                    {
                        solicitacao.Situacao = atendimentoValidacao.First().SituacaoSolicitacao;
                        atendimento.SituacaoSolicitacao = atendimentoValidacao.First().SituacaoSolicitacao;
                    }
                    else
                    {
                        solicitacao.Situacao = SolicitacaoSituacaoEnum.aguardando;
                        atendimento.SituacaoSolicitacao = SolicitacaoSituacaoEnum.aguardando;
                    }

                    atendimento.DataHoraFim = DateTime.Now;
                }

                // Persiste os dados alterados no banco
                await _solicitacaoService.Alterar(solicitacao);
                await _atendimentoService.Salvar(atendimento);
            }
            catch (Exception e)
            {
                if (e.InnerException is ApiException) throw e.InnerException;

                ContentSingleton.AddMessage(e.Message);
                ContentSingleton.Dispatch();
            }

            return new RetornoAtendimentoViewModel
            {
                Status = true,
                Mensagem = "Atribuido com sucesso!"
            };
        }


        public async Task<RetornoAtendimentoViewModel> Finalizar(AtendimentoFinalizarViewModel AtendimentoFinalizarViewModel)
        {
            try
            {
                //Busca a solicitacao
                var solicitacao = await _solicitacaoService.GetById(AtendimentoFinalizarViewModel.SolicitacaoId);

                // Valida Reprovação dos documentos
                if (AtendimentoFinalizarViewModel.Situacao == SolicitacaoSituacaoEnum.aguardandoretorno)
                {
                    var validaDocumentosReprovados = 0;

                    var titularDocsReprovados = solicitacao.Titular.Documentos.Where(d => d.Situacao == DocumentoSituacaoEnum.reprovado).ToList();
                    if (titularDocsReprovados.Count() > 0)
                    {
                        foreach (var rep in titularDocsReprovados)
                        {
                            var titularReprovadosByTipo = solicitacao.Titular.Documentos.Where(d => d.Tipo == rep.Tipo).OrderByDescending(o => o.Id);
                            if (titularReprovadosByTipo.Count() > 1)
                            {
                                if (titularReprovadosByTipo.First().Situacao == DocumentoSituacaoEnum.reprovado)
                                {
                                    validaDocumentosReprovados += 1;
                                }
                            }
                            else if (titularReprovadosByTipo.Count() == 1)
                            {
                                validaDocumentosReprovados += 1;
                            }
                        }
                    }

                    if (solicitacao.Titular.ResponsavelId > 0)
                    {
                        var responsavelDocsReprovados = solicitacao.Titular.Responsavel.Documentos.Where(d => d.Situacao == DocumentoSituacaoEnum.reprovado);
                        if (responsavelDocsReprovados.Count() > 0)
                        {
                            foreach (var rep in responsavelDocsReprovados)
                            {
                                var responsavelReprovadosByTipo = solicitacao.Titular.Responsavel.Documentos.Where(d => d.Tipo == rep.Tipo).OrderByDescending(o => o.Id);
                                if (responsavelReprovadosByTipo.Count() > 1)
                                {
                                    if (responsavelReprovadosByTipo.First().Situacao == DocumentoSituacaoEnum.reprovado)
                                    {
                                        validaDocumentosReprovados += 1;
                                    }
                                }
                                else if (responsavelReprovadosByTipo.Count() == 1)
                                {
                                    validaDocumentosReprovados += 1;
                                }

                            }
                        }
                    }

                    if (solicitacao.Titular.Dependentes.Count() > 0)
                    {
                        foreach (var dependente in solicitacao.Titular.Dependentes)
                        {
                            if ((
                                    solicitacao.Tipo != SolicitacaoTipoEnum.alteracaodependente &&
                                    solicitacao.Tipo != SolicitacaoTipoEnum.renovacao
                                ) ||
                                (
                                    solicitacao.Tipo == SolicitacaoTipoEnum.alteracaodependente &&
                                    dependente.Acao != AcaoEnum.Exclusao
                                ) || (
                                    solicitacao.Tipo == SolicitacaoTipoEnum.renovacao &&
                                    dependente.Acao != AcaoEnum.Exclusao &&
                                    dependente.Acao != AcaoEnum.NaoRenovar &&
                                    dependente.Acao != AcaoEnum.SemAlteracao
                                )
                            )
                            {

                                var dependenteDocsReprovados = dependente.Documentos.Where(d => d.Situacao == DocumentoSituacaoEnum.reprovado);
                                if (dependenteDocsReprovados.Count() > 0)
                                {
                                    foreach (var rep in dependenteDocsReprovados)
                                    {
                                        var dependenteReprovadosByTipo = dependente.Documentos.Where(d => d.Tipo == rep.Tipo).OrderByDescending(o => o.Id);
                                        if (dependenteReprovadosByTipo.Count() > 1)
                                        {
                                            if (dependenteReprovadosByTipo.First().Situacao == DocumentoSituacaoEnum.reprovado)
                                            {
                                                validaDocumentosReprovados += 1;
                                            }
                                        }
                                        else if (dependenteReprovadosByTipo.Count() == 1)
                                        {
                                            validaDocumentosReprovados += 1;
                                        }

                                    }
                                }

                                validaDocumentosReprovados += dependente.Documentos.Where(d => d.Situacao == DocumentoSituacaoEnum.reprovado).Count();
                            }
                        }
                    }

                    if (validaDocumentosReprovados <= 0)
                    {
                        return new RetornoAtendimentoViewModel
                        {
                            Status = false,
                            Mensagem = "Você não pode colocar a solicitação com status aguardando retorno do cliente sem ter pelo menos um documento reprovado!"
                        };
                    }
                }

                // Valida os documentos em caso de aprovação da solicitação
                var documentosAprovados = true;
                if (AtendimentoFinalizarViewModel.Situacao == SolicitacaoSituacaoEnum.aprovada)
                {
                    if (solicitacao.Tipo != SolicitacaoTipoEnum.alteracaodependente)
                    {
                        var docsTitular = solicitacao.Titular.Documentos.Where(d => d.Situacao == DocumentoSituacaoEnum.aprovado)
                            .Select(d => new { Tipo = d.Tipo })
                            .Distinct()
                            .ToList();

                        var titularVencido = false;

                        if (solicitacao.Titular?.DataVencimento != null)
                        {
                            var dataVencimento = new DateTime(((DateTime)solicitacao.Titular.DataVencimento).Year, ((DateTime)solicitacao.Titular.DataVencimento).Month, 1);
                            titularVencido = dataVencimento <= DateTime.Now;
                        }

                        if (titularVencido && docsTitular.Count() != (int)QtdDocumentosObrigatorios.titular) documentosAprovados = false;
                    }

                    if (solicitacao.Titular.ResponsavelId > 0)
                    {
                        foreach (var documentoResponsavel in solicitacao.Titular.Responsavel.Documentos)
                        {
                            if (documentoResponsavel.Situacao != DocumentoSituacaoEnum.aprovado)
                                documentosAprovados = false;
                        }
                    }

                    if (solicitacao.Titular.Dependentes.Count() > 0)
                    {
                        foreach (var dependente in solicitacao.Titular.Dependentes)
                        {
                            if ((
                                    solicitacao.Tipo != SolicitacaoTipoEnum.alteracaodependente &&
                                    solicitacao.Tipo != SolicitacaoTipoEnum.renovacao &&
                                    solicitacao.Tipo != SolicitacaoTipoEnum.mudancacategoria
                                ) ||
                                (
                                    solicitacao.Tipo == SolicitacaoTipoEnum.alteracaodependente &&
                                    dependente.Acao != AcaoEnum.Exclusao &&
                                    dependente.Acao != AcaoEnum.SemAlteracao &&
                                    dependente.Acao != AcaoEnum.NaoRenovar
                                ) || (
                                    solicitacao.Tipo == SolicitacaoTipoEnum.renovacao &&
                                    dependente.Acao != AcaoEnum.Exclusao &&
                                    dependente.Acao != AcaoEnum.NaoRenovar &&
                                    dependente.Acao != AcaoEnum.SemAlteracao
                                ) || (
                                    solicitacao.Tipo == SolicitacaoTipoEnum.mudancacategoria &&
                                    dependente.Acao != AcaoEnum.Exclusao &&
                                    dependente.Acao != AcaoEnum.NaoRenovar &&
                                    dependente.Acao != AcaoEnum.SemAlteracao
                                )
                            )
                            {
                                var docsDependente = dependente.Documentos.Where(d => d.Situacao == DocumentoSituacaoEnum.aprovado)
                                    .Select(d => new { Tipo = d.Tipo })
                                    .Distinct()
                                    .ToList();


                                var idade = DateTime.Now.Year - dependente.DataNascimento.Year;

                                var conjuge = (dependente.Parentesco == ParentescoEnum.conjuge);
                                var padrastro_madastra = (dependente.Parentesco == ParentescoEnum.padrasto || dependente.Parentesco == ParentescoEnum.madrasta);
                                var envio_declaracao = (
                                                            (
                                                                dependente.Parentesco == ParentescoEnum.filho ||
                                                                dependente.Parentesco == ParentescoEnum.Enteado ||
                                                                dependente.Parentesco == ParentescoEnum.neto
                                                            ) && idade >= 21
                                                        );

                                if (conjuge || padrastro_madastra)
                                {
                                    if (docsDependente.Count() != (int)QtdDocumentosObrigatorios.conjuge) documentosAprovados = false;
                                }

                                if (envio_declaracao)
                                {
                                    if (docsDependente.Count() != (int)QtdDocumentosObrigatorios.depedenteFilhoEnteadoNeto) documentosAprovados = false;
                                }

                                if (!conjuge && !padrastro_madastra && !envio_declaracao) {
                                    if (docsDependente.Count() != (int)QtdDocumentosObrigatorios.dependenteNaoConjuge) documentosAprovados = false;
                                }
                            }
                        }
                    }
                }

                if (!documentosAprovados)
                {
                    return new RetornoAtendimentoViewModel
                    {
                        Status = false,
                        Mensagem = "Você não pode finalizar uma solicitação sem ter pelo menos um documento de cada tipo aprovado!"
                    };
                }

                if (String.IsNullOrEmpty(AtendimentoFinalizarViewModel.Observacao))
                {
                    return new RetornoAtendimentoViewModel
                    {
                        Status = false,
                        Mensagem = "Necessário colocar uma observação para finalizar o atendimento!"
                    };
                }

                //Busca o atendimento e finaliza
                var historicoAtendimentos = solicitacao.Atendimentos
                    .OrderByDescending(o => o.Id)
                    .ToList();

                var atendimento = historicoAtendimentos.First();
                atendimento.SituacaoSolicitacao = AtendimentoFinalizarViewModel.Situacao;
                atendimento.DataHoraFim = DateTime.Now;
                atendimento.Observacao = AtendimentoFinalizarViewModel.Observacao;
                await _atendimentoService.Salvar(atendimento);

                //Busca a solicitacao e altera a situacao
                solicitacao.Situacao = AtendimentoFinalizarViewModel.Situacao;

                await _solicitacaoService.Alterar(solicitacao);

                _solicitacaoService.EnviarEmailConfirmacao(solicitacao.Id);
            }
            catch (Exception e)
            {
                if (e.InnerException is ApiException) throw e.InnerException;

                ContentSingleton.AddMessage(e.Message);
                ContentSingleton.Dispatch();
            }

            return new RetornoAtendimentoViewModel
            {
                Status = true,
                Mensagem = "Atendimento finalizado com sucesso!"
            };
        }
        public async Task<bool> ReenviarEmail(int solicitacaoId)
        {
            try
            {
                _solicitacaoService.EnviarEmailConfirmacao(solicitacaoId);
            }
            catch (Exception e)
            {
                if (e.InnerException is ApiException) throw e.InnerException;

                ContentSingleton.AddMessage(e.Message);
                ContentSingleton.Dispatch();
            }

            return true;
        }

    }
}