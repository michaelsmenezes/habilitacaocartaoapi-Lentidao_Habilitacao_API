using AutoMapper;
using Microsoft.AspNetCore.Http;
using Sesc.Application.ApplicationServices.Commands.Contracts;
using Sesc.CrossCutting.ServiceAgents.AuthServer.Services.Contracts;
using Sesc.CrossCutting.Validation.BaseException;
using Sesc.Domain.Habilitacao.Entities;
using Sesc.Domain.Habilitacao.Enum;
using Sesc.Domain.Habilitacao.Services.Contracts;
using Sesc.Domain.Habilitacao.ViewModel;
using System;
using System.IO;
using System.Linq;

namespace Sesc.Application.ApplicationServices.Commands
{
    public class DocumentoCommandServiceApplication : IDocumentoCommandServiceApplication
    {
        private readonly IDocumentoService _documentoService;
        private readonly ISolicitacaoService _solicitacaoService;
        private readonly IUserAuthenticatedAuthService _userAuthenticatedAuthService;
        private readonly IPessoaService _pessoaService;
        private readonly IMapper _mapper;

        public DocumentoCommandServiceApplication(
            IDocumentoService documentoService,
            ISolicitacaoService solicitacaoService,
            IPessoaService pessoaService,
            IUserAuthenticatedAuthService userAuthenticatedAuthService,
            IMapper mapper
        ) {
            _documentoService = documentoService;
            _solicitacaoService = solicitacaoService;
            _userAuthenticatedAuthService = userAuthenticatedAuthService;
            _pessoaService = pessoaService;
            _mapper = mapper;
        }

        public bool AlteraSituacao(DocumentoViewModel documentoViewModel)
        {
            try
            {
                var documento = _mapper.Map<Documento>(documentoViewModel);

                if (documento != null)
                {
                    Alterar(documento);
                }
                else
                {
                    throw new Exception("Ocorreu um erro interno, por favor verifique se todos os dados informados estão digitados corretamente.");
                }

            } catch (Exception e)
            {
                if (e.InnerException is ApiException) throw e.InnerException;

                ContentSingleton.AddMessage(e.Message);
                ContentSingleton.Dispatch();
            }

            return true;
        }

        private DocumentoViewModel Alterar(Documento documentoNew)
        {
            var documentoAtual = _documentoService.GetById(documentoNew.Id).Result;

            if (documentoAtual == null)
            {
                throw new Exception("Ocorreu um erro durante esta operação, por favor, tente reiniciar a operação.");
            }

            documentoAtual.ChangeDocumento(documentoNew);
            var documentoDto = _documentoService.Alterar(documentoAtual).Result;

            return _mapper.Map<DocumentoViewModel>(documentoDto);
        }

        public void SalvarArquivoUsuarioLogado(UploadDocumentoViewModel documento)
        {
            try
            {
                var solicitacao = _solicitacaoService.GetById(documento.Id).Result;

                var user = _userAuthenticatedAuthService.GetUserAuthenticated();

                if (solicitacao.Cpf != user.CpfCnpj)
                {
                    throw new Exception("Este usuário não é dono da solicitação!");
                }

                var pessoa = GetPessoaInSolicitacao(solicitacao, documento.PessoaId);
                var DocumentoAtual = pessoa.Documentos.Where(t => t.Tipo == documento.TipoDocumento)
                    .LastOrDefault();

                if (DocumentoAtual != null && DocumentoAtual.Situacao == DocumentoSituacaoEnum.aguardando)
                {
                    _documentoService.Deletar(DocumentoAtual);
                }
                
                _documentoService.Incluir(pessoa, documento).Wait();
            }
            catch (Exception e)
            {
                if (e.InnerException is ApiException) throw e.InnerException;

                ContentSingleton.AddMessage(e.Message);
                ContentSingleton.Dispatch();
            }
        }

        public Pessoa GetPessoaInSolicitacao(Solicitacao solicitacao, int pessoaId)
        {
            if (solicitacao.Titular.Id == pessoaId)
            {
                return solicitacao.Titular;
            }
            else if (
               solicitacao.Titular.Dependentes.Count > 0 &&
               solicitacao.Titular.Dependentes.Any(d => d.Id == pessoaId)
            ) {
                return solicitacao.Titular.Dependentes.Where(d => d.Id == pessoaId)
                    .FirstOrDefault();
            }
            else if (
                solicitacao.Titular.Responsavel != null &&
                solicitacao.Titular.Responsavel.Id == pessoaId
            ) {
                return solicitacao.Titular.Responsavel;
            }
            else
            {
                throw new Exception("A pessoa informada não pertence à esta solicitação.");
            }
        }
    }
}