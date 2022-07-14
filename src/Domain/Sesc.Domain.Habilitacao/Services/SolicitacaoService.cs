using Sesc.Domain.Habilitacao.Dto;
using Sesc.Domain.Habilitacao.Services.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sesc.Domain.Habilitacao.Repositories.Contracts;
using Sesc.Domain.Habilitacao.Entities;
using AutoMapper;
using Sesc.MeuSesc.SharedKernel.Infrastructure.Pagination;
using Sesc.MeuSesc.SharedKernel.Application.Services;
using Sesc.MeuSesc.SharedKernel.Infrastructure.UnitOfWork;
using Sesc.Domain.Habilitacao.Validator;
using System;
using System.Linq.Expressions;
using Sesc.Domain.Habilitacao.Enum;
using System.Linq;
using Sesc.CrossCutting.Notification.Services.Contracts;
using Sesc.CrossCutting.ServiceAgents.Clientela.Dto;
using Sesc.CrossCutting.ServiceAgents.AntiCorruption.Sca2Habilitacao;
using Sesc.CrossCutting.ServiceAgents.AuthServer.Services.Contracts;
using Sesc.CrossCutting.ServiceAgents.Clientela.Services.Contracts;
using Sesc.Domain.Habilitacao.Helpers;
using System.Reflection;
using Microsoft.AspNetCore.Http;

namespace Sesc.Domain.Habilitacao.Services
{
    public class SolicitacaoService : Service<Solicitacao, ISolicitacaoRepository>, ISolicitacaoService
    {
        private readonly IPessoaScaService _pessoaScaService;
        private readonly IEmpresaScaService _empresaScaService;
        private readonly IEnderecoScaService _enderecoScaService;
        private readonly INotificacaoTemplateRepository _notificacaoEmailTemplateRepository;
        private readonly INotificationService _notificationService;
        private readonly IUserAuthenticatedAuthService _userAuthenticatedAuthService;
        private readonly ICidadeRepository _cidadeRepository;
        private readonly IDependenteRepository _dependenteRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDocumentoService _documentoService;

        public SolicitacaoService(
            IUnitOfWork unitOfWork,
            ISolicitacaoRepository repository,
            IPessoaScaService pessoaScaService,
            IEmpresaScaService empresaScaService,
            IDocumentoService documentoService,
            INotificacaoTemplateRepository notificacaoEmailTemplateRepository,
            IUserAuthenticatedAuthService userAuthenticatedAuthService,
            INotificationService notificationService,
            IEnderecoScaService enderecoScaService,
            ICidadeRepository cidadeRepository,
            IDependenteRepository dependenteRepository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor
        ) : base(unitOfWork, repository, mapper)
        {
            _notificacaoEmailTemplateRepository = notificacaoEmailTemplateRepository;
            _userAuthenticatedAuthService = userAuthenticatedAuthService;
            _notificationService = notificationService;
            _pessoaScaService = pessoaScaService;
            _empresaScaService = empresaScaService;
            _enderecoScaService = enderecoScaService;
            _cidadeRepository = cidadeRepository;
            _dependenteRepository = dependenteRepository;
            _httpContextAccessor = httpContextAccessor;
            _documentoService = documentoService;
        }

        public async Task<Solicitacao> GetById(int id, bool noTrack = false)
        {
            return _repository.GetById(id, noTrack);
        }

        public async Task<Solicitacao> GetCadastrandoByCpf(string cpf)
        {
            return _repository.GetCadastrandoByCpf(cpf);
        }

        public async Task<IList<SolicitacaoDto>> GetSolicitacoesByCpf(string cpf)
        {
            var solicitacoes = (List<Solicitacao>)_repository.GetSolicitacoesByCpf(cpf);

            return _mapper.Map<IList<SolicitacaoDto>>(solicitacoes);
        }

        public async Task<Solicitacao> GetByTitular(Titular titular)
        {
            if (titular == null) return null;

            return _repository.FindBy(s => s.Titular.Id == titular.Id)
                .FirstOrDefault();
        }

        public async Task<SolicitacaoDto> Incluir(Solicitacao solicitacao)
        {
            if (HasSolicitacaoTipoEmAndamento(SolicitacaoTipoEnum.nova))
            {
                throw new Exception("Você já possui uma solicitação em andamento, verifique na listagem das solicitações.");
            }

            Valid<RegistrarSolicitacaoEntityValidator, Solicitacao>.Dispatch(solicitacao);

            Sanitize<Solicitacao>(solicitacao);

            _repository.Incluir(solicitacao);

            return _mapper.Map<SolicitacaoDto>(solicitacao);
        }

        public void IncluirDependente(int idSolicitacao, string cpf)
        {
            try
            {
                var solicitacao = GetById(idSolicitacao).Result;

                if (solicitacao == null)
                {
                    throw new Exception("Nenhum dependente enviado ou solicitação ativa não encontrada.");
                }

                if (solicitacao.Situacao != SolicitacaoSituacaoEnum.cadastro &&
                    solicitacao.Situacao != SolicitacaoSituacaoEnum.aguardando &&
                    solicitacao.Situacao != SolicitacaoSituacaoEnum.aguardandoretorno
                )
                {
                    throw new Exception("Esta solicitação já está em analise e não pode mais ser alterada.");
                }

                var pessoaSca = _pessoaScaService.GetPessoa(cpf);

                if (pessoaSca == null)
                {
                    throw new Exception("Usuário não encontrado na base de dependentes ativos.");
                }

                var dependenteDto = GetDependenteDtoFromPessoaSca(pessoaSca);
                dependenteDto.Acao = AcaoEnum.Exclusao;

                var dependente = _mapper.Map<Dependente>(dependenteDto);
                dependente.ContatoId = null;
                dependente.InformacaoProfissionalId = null;

                if (solicitacao.Titular.Dependentes.Any(x => x.Cpf == dependente.Cpf))
                {
                    throw new Exception("O Dependente informado já consta na sua lista de dependentes.");
                }

                dependente.Id = 0;
                dependente.TitularId = solicitacao.Titular.Id;
                dependente.Titular = solicitacao.Titular;

                solicitacao.Titular.Dependentes.Add(dependente);

                Sanitize<Solicitacao>(solicitacao);

                _repository.Alterar(solicitacao);
            }
            catch (Exception e)
            {
                if (e.InnerException != null) throw e.InnerException;
                throw e;
            }
        }

        public void SetarRenovacaoDependente(int solicitacaoId, int id, AcaoEnum acao)
        {
            try
            {
                if (acao != AcaoEnum.NaoRenovar && acao != AcaoEnum.Renovacao && acao != AcaoEnum.SemAlteracao)
                {
                    throw new Exception("Esta operação é inválida.");
                }

                var solicitacao = GetById(solicitacaoId).Result;
                if (solicitacao == null)
                {
                    throw new Exception("Nenhum dependente enviado ou solicitação ativa não encontrada.");
                }

                if (solicitacao.Situacao != SolicitacaoSituacaoEnum.cadastro &&
                    solicitacao.Situacao != SolicitacaoSituacaoEnum.aguardando &&
                    solicitacao.Situacao != SolicitacaoSituacaoEnum.aguardandoretorno
                )
                {
                    throw new Exception("Esta solicitação já está em analise e não pode mais ser alterada.");
                }

                if (!solicitacao.Titular.Dependentes.Any(x => x.Id == id))
                {
                    throw new Exception("Usuário não encontrado na base de dependentes.");
                }

                solicitacao.Titular.Dependentes.Where(x => x.Id == id).First().Acao = acao;

                if (acao == AcaoEnum.NaoRenovar)
                {
                    var documentos = solicitacao.Titular.Dependentes.Where(x => x.Id == id).First().Documentos.ToList();

                    foreach (var doc in documentos)
                    {
                        _documentoService.Delete(doc);
                    }
                }

                Sanitize<Solicitacao>(solicitacao);

                _repository.Alterar(solicitacao);
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                    throw e.InnerException;

                throw e;
            }
        }

        private DependenteDto GetDependenteDtoFromPessoaSca(PessoaScaDto pessoaScaDto)
        {
            int parentesco = PessoaAntiCorruption.ParentescoFromSca2Habilitacao(
                String.IsNullOrWhiteSpace(pessoaScaDto.dsparentsc)
                    ? 0
                    : int.Parse(pessoaScaDto.dsparentsc?.Trim())
            );

            var dependenteDto = new DependenteDto();
            dependenteDto.Parentesco = (ParentescoEnum)parentesco;
            dependenteDto.Id = 0;
            dependenteDto.Nome = pessoaScaDto.nmcliente?.Trim();
            dependenteDto.Sexo = PessoaAntiCorruption.SexoFromSca2Habilitacao(
                int.Parse(pessoaScaDto.cdsexo?.Trim())
            );
            dependenteDto.Escolaridade = (PessoaEscolaridadeEnum)PessoaAntiCorruption
                .EscolaridadeFromSca2Habilitacao(pessoaScaDto.cdnivel);
            dependenteDto.EstadoCivil = (PessoaEstadoCivilEnum)PessoaAntiCorruption
                .EstadoCivilFromSca2Habilitacao(pessoaScaDto.cdestcivil);
            dependenteDto.NomeSocial = pessoaScaDto.nmsocial?.Trim();
            dependenteDto.DataNascimento = pessoaScaDto.dtnascimen;
            dependenteDto.Cpf = string.IsNullOrEmpty(pessoaScaDto.nucpf?.Trim())
                ? "00000000000"
                : pessoaScaDto.nucpf?.Trim();
            dependenteDto.Email = pessoaScaDto.email?.Trim();
            dependenteDto.NomeMae = pessoaScaDto.nmmae?.Trim();
            dependenteDto.ContatoId = 0;
            dependenteDto.Contato = null;
            dependenteDto.InformacaoProfissionalId = 0;
            dependenteDto.InformacaoProfissional = null;
            dependenteDto.UltimaSerie = pessoaScaDto.nuultserie?.Trim();
            dependenteDto.Nacionalidade = pessoaScaDto.dsnacional?.Trim();
            dependenteDto.Naturalidade = pessoaScaDto.dsnatural?.Trim();
            dependenteDto.TipoDocumento = PessoaTipoDocumentoEnum.rg;
            dependenteDto.Numero = string.IsNullOrEmpty(pessoaScaDto.nureggeral?.Trim())
                ? "VERIFICAR"
                : pessoaScaDto.nureggeral?.Trim();
            dependenteDto.DataEmissao = pessoaScaDto.dtemirg == null ? DateTime.Now : pessoaScaDto.dtemirg;
            dependenteDto.DataVencimento = pessoaScaDto.dtvencto;
            dependenteDto.OrgaoEmissor = string.IsNullOrEmpty(pessoaScaDto.idorgemirg?.Trim())
                ? "VERIFICAR"
                : pessoaScaDto.idorgemirg?.Trim();
            dependenteDto.Documentos = null;

            return dependenteDto;
        }

        public async Task<SolicitacaoDto> Alterar(Solicitacao solicitacao)
        {
            try
            {
                Valid<ChangeSolicitacaoEntityValidator, Solicitacao>.Dispatch(solicitacao);

                Sanitize<Solicitacao>(solicitacao);

                _repository.Alterar(solicitacao);

                return _mapper.Map<SolicitacaoDto>(solicitacao);
            }
            catch (Exception e)
            {
                if (e.InnerException != null) throw e.InnerException;
                throw e;
            }
        }

        public async Task<SolicitacaoDto> Cancelar(SolicitacaoDto solicitacaoDto)
        {
            try
            {
                if (solicitacaoDto == null) throw new Exception("Solicitação não encontrada.");

                var user = _userAuthenticatedAuthService.GetUserAuthenticated();

                var solicitacao = GetById((int)solicitacaoDto.Id).Result;

                if (solicitacao == null || solicitacao.Cpf != user.CpfCnpj)
                {
                    throw new Exception("Esta solicitação não foi criada pelo mesmo usuário que está logado.");
                }

                if (solicitacao.Situacao != SolicitacaoSituacaoEnum.cadastro &&
                    solicitacao.Situacao != SolicitacaoSituacaoEnum.aguardando &&
                    solicitacao.Situacao != SolicitacaoSituacaoEnum.aguardandoretorno
                )
                {
                    throw new Exception("Esta solicitação já passou da etapa de cadastro e não pode mais ser deletada.");
                }

                solicitacao.Situacao = SolicitacaoSituacaoEnum.cancelada;

                _repository.Alterar(solicitacao);

                return _mapper.Map<SolicitacaoDto>(solicitacao);
            }
            catch (Exception e)
            {
                if (e.InnerException != null) throw e.InnerException;
                throw e;
            }
        }

        public async Task<PagedResult<Solicitacao>> GetSolicitacoesAll(SolicitacaoFiltrosDto SolicitacaoFiltrosDto)
        {
            int page = SolicitacaoFiltrosDto.Page != null ? Int32.Parse(SolicitacaoFiltrosDto.Page) : 1;
            int pageSize = SolicitacaoFiltrosDto.PageSize != null ? Int32.Parse(SolicitacaoFiltrosDto.PageSize) : 20;

            System.Enum.TryParse(SolicitacaoFiltrosDto.Situacao, out SolicitacaoSituacaoEnum solicitacaoSituacaoEnum);
            System.Enum.TryParse(SolicitacaoFiltrosDto.Tipo, out SolicitacaoTipoEnum solicitacaoTipoEnum);

            Expression<Func<Solicitacao, bool>> filter = d =>
                   (String.IsNullOrWhiteSpace(SolicitacaoFiltrosDto.Cpf) || d.Cpf.ToString() == SolicitacaoFiltrosDto.Cpf)
                && (String.IsNullOrWhiteSpace(SolicitacaoFiltrosDto.Nome) || d.Titular.Nome.ToUpper().Contains(SolicitacaoFiltrosDto.Nome.ToUpper()))
                && (String.IsNullOrWhiteSpace(SolicitacaoFiltrosDto.Tipo) || d.Tipo == solicitacaoTipoEnum)
                && (String.IsNullOrWhiteSpace(SolicitacaoFiltrosDto.MunicipioResponsavel) || ((d.Titular.Contato.Cidade.CidadeResponsavelId == null && d.Titular.Contato.CidadeId == Convert.ToInt32(SolicitacaoFiltrosDto.MunicipioResponsavel)) || (d.Titular.Contato.Cidade.CidadeResponsavelId == Convert.ToInt32(SolicitacaoFiltrosDto.MunicipioResponsavel))))
                && (!SolicitacaoFiltrosDto.Situacoes.Any() || SolicitacaoFiltrosDto.Situacoes.Contains((int)d.Situacao))
                && d.Situacao != SolicitacaoSituacaoEnum.cadastro;

            //Expression<Func<Solicitacao, bool>> filter = d =>
            //  (d.Titular.Nome.ToUpper().Contains("MICHAEL SULIVAN MATIAS DE MENEZES"))
            //&& d.Situacao != SolicitacaoSituacaoEnum.cadastro;
            //d.Situacao == SolicitacaoSituacaoEnum.retornado;

            return _repository.GetSolicitacoesAll(page, pageSize, filter);
        }

        public async Task<IList<SolicitacaoDto>> GetSolicitacoesAll()
        {
            var solicitacoes = (List<Solicitacao>)_repository.GetSolicitacoesAll();

            IList<SolicitacaoDto> solicitacoesDto = new List<SolicitacaoDto>();
            solicitacoes.ForEach(solicitacao =>
            {
                var solicitacaoDto = _mapper.Map<SolicitacaoDto>(solicitacao);

                solicitacoesDto.Add(solicitacaoDto);
            });

            return solicitacoesDto;
        }

        public void EnviarEmailConfirmacao(int id)
        {
            var solicitacao = _repository.GetById(id);
            var nomeResponsavel = (solicitacao.Titular.ResponsavelId > 0 ? solicitacao.Titular.Responsavel.Nome : solicitacao.Titular.Nome);

            var observacoes = "<strong>Informa&ccedil;&otilde;es do atendente</strong><br>";

            if (solicitacao.Atendimentos.Count > 0)
            {
                observacoes += "<p>" + solicitacao.Atendimentos.OrderByDescending(o => o.Id).First().Observacao + "</p>";
            }

            switch (solicitacao.Situacao)
            {
                case SolicitacaoSituacaoEnum.aguardando:
                case SolicitacaoSituacaoEnum.retornado:
                    var templateNova = _notificacaoEmailTemplateRepository.GetNotificacaoTemplateByIdentificador("solicitacao-enviada");

                    if (templateNova != null)
                    {
                        var chavesMsg = new Dictionary<string, string>
                            {
                                {"data-hora", DateTime.Now.ToString("d")},
                                {"status", solicitacao.Situacao.ToString()},
                                {"numero-solicitacao", solicitacao.Id.ToString().PadLeft(6, '0')},
                                {"nome-responsavel", nomeResponsavel }
                            };
                        _notificationService.EnviarEmail(solicitacao.Titular.Contato.Email, templateNova.AssuntoModelo, templateNova.TextoModelo, chavesMsg);
                    }
                    break;
                case SolicitacaoSituacaoEnum.aguardandoretorno:
                    var templatePendente = _notificacaoEmailTemplateRepository.GetNotificacaoTemplateByIdentificador("solicitacao-pendente");
                    if (templatePendente != null)
                    {
                        var chavesMsg = new Dictionary<string, string>
                            {
                                {"data-hora", DateTime.Now.ToString("d")},
                                {"status",  solicitacao.Situacao.ToString()},
                                {"informacoes-atendente", observacoes},
                                {"nome-responsavel", nomeResponsavel},
                            };
                        _notificationService.EnviarEmail(solicitacao.Titular.Contato.Email, templatePendente.AssuntoModelo, templatePendente.TextoModelo, chavesMsg);
                    }
                    break;
                case SolicitacaoSituacaoEnum.aprovada:
                    var templateFinalizada = _notificacaoEmailTemplateRepository.GetNotificacaoTemplateByIdentificador("solicitacao-aprovada");
                    if (templateFinalizada != null)
                    {
                        var chavesMsg = new Dictionary<string, string>
                            {
                                {"data-hora", DateTime.Now.ToString("d")},
                                {"nome-responsavel", nomeResponsavel},
                                {"status", solicitacao.Situacao.ToString()},
                                {"numero-solicitacao", solicitacao.Id.ToString().PadLeft(6, '0')},
                                {"informacoes-atendente", observacoes},
                            };
                        _notificationService.EnviarEmail(solicitacao.Titular.Contato.Email, templateFinalizada.AssuntoModelo, templateFinalizada.TextoModelo, chavesMsg);
                    }
                    break;
                case SolicitacaoSituacaoEnum.cancelada:
                case SolicitacaoSituacaoEnum.reprovada:
                    var templateCancelada = _notificacaoEmailTemplateRepository.GetNotificacaoTemplateByIdentificador("solicitacao-reprovada");
                    if (templateCancelada != null)
                    {
                        var chavesMsg = new Dictionary<string, string>
                            {
                                {"data-hora", DateTime.Now.ToString("d")},
                                {"status", solicitacao.Situacao.ToString()},
                                {"nome-responsavel", nomeResponsavel},
                                {"informacoes-atendente", observacoes},
                            };
                        _notificationService.EnviarEmail(solicitacao.Titular.Contato.Email, templateCancelada.AssuntoModelo, templateCancelada.TextoModelo, chavesMsg);
                    }
                    break;
                default:
                    break;
            }
        }

        public void FinalizarCadastro(SolicitacaoDto solicitacaoNew)
        {
            if (solicitacaoNew == null) throw new Exception("Solicitação não encontrada!");

            var user = _userAuthenticatedAuthService.GetUserAuthenticated();

            var solicitacao = GetById((int)solicitacaoNew.Id, true).Result;

            if (solicitacao == null || solicitacao.Titular.Cpf != user.CpfCnpj)
            {
                throw new Exception("Esta solicitação não foi criada pelo mesmo usuário que está logado.");
            }

            if (solicitacao.Situacao != SolicitacaoSituacaoEnum.cadastro &&
                solicitacao.Situacao != SolicitacaoSituacaoEnum.aguardando &&
                solicitacao.Situacao != SolicitacaoSituacaoEnum.aguardandoretorno
            )
            {
                throw new Exception("Esta solicitação não pode ser mais alterada pois já está em analise.");
            }

            var pessoaSca = _pessoaScaService.GetPessoa(solicitacao.Titular.Cpf);

            if (solicitacao.Tipo == SolicitacaoTipoEnum.nova && pessoaSca != null)
            {
                throw new Exception("Este titular já possui cartão SESC.");
            }
            else if (solicitacao.Tipo != SolicitacaoTipoEnum.nova && pessoaSca == null)
            {
                throw new Exception("Este titular não possui cartão SESC.");
            }

            var solicitacaoToSave = _mapper.Map<Solicitacao>(solicitacaoNew);

            if (solicitacaoToSave == null) throw new Exception("Solicitação não encontrada ou inválida!");

            if (solicitacaoToSave.Tipo == SolicitacaoTipoEnum.alteracaodependente)
            {
                Valid<FinalizarSolicitacaoAlteracaoDependentesEntityValidator, Solicitacao>.Dispatch(solicitacaoToSave);
            }
            else
            {
                Valid<FinalizarCadastroSolicitacaoEntityValidator, Solicitacao>.Dispatch(solicitacaoToSave);
            }

            if (solicitacaoToSave.Titular.Dependentes.Count != solicitacao.Titular.Dependentes.Count)
            {
                throw new Exception("A quantidade de dependentes informada durante a finalização não corresponde com a quantidade cadastrada.");
            }

            var dataVencimento = solicitacao.Titular.DataVencimento;

            if (solicitacaoToSave.Tipo == SolicitacaoTipoEnum.nova
                && !IsQuantityOfDocumentsEqual(solicitacao.Titular.Documentos, QtdDocumentosObrigatorios.titular)
            )
            {
                throw new Exception("Ainda faltam documentos do titular a serem enviados.");
            }
            else if (solicitacaoToSave.Tipo == SolicitacaoTipoEnum.renovacao
              && new DateTime(((DateTime)dataVencimento).Year, ((DateTime)dataVencimento).Month, 1) <= DateTime.Now
              && !IsQuantityOfDocumentsEqual(solicitacao.Titular.Documentos, QtdDocumentosObrigatorios.titular))
            {
                throw new Exception("Ainda faltam documentos do titular a serem enviados.");
            }

            if (solicitacao.Titular.Dependentes != null && solicitacao.Titular.Dependentes.Count > 0)
                TryCheckDependentes(solicitacao.Titular.Dependentes);

            //Se for retorno cliente muda situacao para retornado
            solicitacao.Situacao = (solicitacaoNew.Situacao == SolicitacaoSituacaoEnum.aguardandoretorno) ? SolicitacaoSituacaoEnum.retornado : SolicitacaoSituacaoEnum.aguardando;
            solicitacao.Plataforma = DeviceHelper.IsMobile(_httpContextAccessor) == true
                ? SolicitacaoPlataformaEnum.Mobile
                : SolicitacaoPlataformaEnum.NaoClassificado;

            if (solicitacaoToSave.Tipo != SolicitacaoTipoEnum.alteracaodependente)
            {
                Valid<ChangeSolicitacaoEntityValidator, Solicitacao>.Dispatch(solicitacao);
            }

            solicitacao.Enviar();

            Sanitize<Solicitacao>(solicitacao);

            _repository.Alterar(solicitacao);
        }

        private void TryCheckDependentes(ICollection<Dependente> dependentes)
        {
            dependentes.ToList().ForEach(dependente =>
            {
                TryValidateDependente(dependente, (AcaoEnum)dependente.Acao);
            });
        }

        private void TryValidateDependente(Dependente dependente, AcaoEnum acao)
        {
            if (acao == AcaoEnum.Inclusao || acao == AcaoEnum.Renovacao)
            {
                Valid<FinalizarCadastroDependenteEntityValidator, Dependente>.Dispatch(dependente);
                TryCheckQuantityOfDocumentsDependentes(dependente);
            }
            else if (dependente.Id == 0)
            {
                throw new Exception("Dependente a ser excluído não foi identificado.");
            }
        }

        private void TryCheckQuantityOfDocumentsDependentes(
            Dependente dependente
        )
        {

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

            if (conjuge && !IsQuantityOfDocumentsEqual(dependente.Documentos, QtdDocumentosObrigatorios.conjuge))
            {
                throw new Exception("Envie todos os documentos do conjuge dependente.");
            }

            if (padrastro_madastra && !IsQuantityOfDocumentsEqual(dependente.Documentos, QtdDocumentosObrigatorios.conjuge))
            {
                throw new Exception("Envie todos os documentos do(a) padrasto/madastra.");
            }

            if (envio_declaracao && !IsQuantityOfDocumentsEqual(dependente.Documentos, QtdDocumentosObrigatorios.depedenteFilhoEnteadoNeto))
            {
                throw new Exception("Ainda faltam documentos do dependente (filho, neto ou enteado) a serem enviados.");
            }

            if ((!conjuge && !envio_declaracao && !padrastro_madastra) && !IsQuantityOfDocumentsEqual(dependente.Documentos, QtdDocumentosObrigatorios.dependenteNaoConjuge))
            {
                throw new Exception("Ainda faltam documentos do dependente a serem enviados.");
            }
            
        }

        private bool IsQuantityOfDocumentsEqual(ICollection<Documento> documentos, QtdDocumentosObrigatorios qtdRequired)
        {
            var docs = documentos.GroupBy(d => d.Tipo).Select(x => x.First()).ToList();
            return docs != null && docs.Count == (int)qtdRequired;
        }

        public SolicitacaoDto CriarNovaSolicitacaoAlterarDependentes()
        {
            var user = _userAuthenticatedAuthService.GetUserAuthenticated();
            var pessoaScaDto = _pessoaScaService.GetPessoa(user.CpfCnpj);

            if (pessoaScaDto == null)
            {
                throw new Exception("Antes de solicitar a alteração de dependentes é necessário possuir um cartão SESC ativo.");
            }

            if (pessoaScaDto.dtvencto < DateTime.Now)
            {
                throw new Exception("Este cartão SESC está vencido, é necessario realizar a renovação do mesmo.");
            }

            int categoria = PessoaAntiCorruption.CategoriaFromSca2Habilitacao(pessoaScaDto.cdcategori);

            if (categoria != (int)SolicitacaoCategoriaEnum.Comerciario)
            {
                throw new Exception("A alteração de dependentes está disponível apenas para a categoria de trabalhador do comercio.");
            }

            if (HasSolicitacaoTipoEmAndamento(SolicitacaoTipoEnum.alteracaodependente))
            {
                throw new Exception("Você já possui uma solicitação de alteração de dependentes em andamento, verifique na listagem das solicitações.");
            }

            var solicitacao = _repository.FindByInclude(
                s => s.Cpf == user.CpfCnpj &&
                    s.Tipo == SolicitacaoTipoEnum.alteracaodependente &&
                    s.Situacao == SolicitacaoSituacaoEnum.cadastro,
                t => t.Titular
            ).FirstOrDefault();

            solicitacao = solicitacao == null
                ? MakeNewSolicitacaoFromSca(SolicitacaoTipoEnum.alteracaodependente, pessoaScaDto)
                : solicitacao;

            return _mapper.Map<SolicitacaoDto>(solicitacao);
        }

        private Solicitacao MakeNewSolicitacaoFromSca(
            SolicitacaoTipoEnum tipo,
            PessoaScaDto pessoaScaDto,
            IList<PessoaScaDto> dependentesScaDto = null
        )
        {
            var user = _userAuthenticatedAuthService.GetUserAuthenticated();

            var empresaSca = string.IsNullOrEmpty(pessoaScaDto.nucgccei?.Trim())
                ? new EmpresaScaDto()
                : _empresaScaService.GetEmpresa(pessoaScaDto.nucgccei.Trim());

            var enderecoSca = _enderecoScaService.GetEndereco(user.CpfCnpj);
            var cidade = new Cidade();

            if(enderecoSca != null)
            {
                cidade = _cidadeRepository.FindBy(
                            c => StringHelper.RemoveDiacritics(c.Descricao).ToLower().Trim() == StringHelper.RemoveDiacritics(enderecoSca.dsmunicip).ToLower().Trim()
                            && c.Estado.Uf.ToLower() == enderecoSca.siestado.ToLower()
                         ).FirstOrDefault();
            }

            if (cidade == null)
            {
                cidade = _cidadeRepository.FindByKey((int)CidadePadraoEnum.Goiania);
            }

            var solicitacao = new Solicitacao
            {
                Categoria = (SolicitacaoCategoriaEnum)PessoaAntiCorruption.CategoriaFromSca2Habilitacao(
                    pessoaScaDto.cdcategori
                ),
                DataRegistro = DateTime.Now,
                Cpf = user.CpfCnpj,
                Situacao = SolicitacaoSituacaoEnum.cadastro,
                Tipo = tipo,
                Titular = new Titular
                {
                    Cpf = user.CpfCnpj,
                    Nome = pessoaScaDto.nmcliente?.Trim(),
                    NomeSocial = pessoaScaDto.nmsocial?.Trim(),
                    DataNascimento = pessoaScaDto.dtnascimen,
                    Nacionalidade = pessoaScaDto.dsnacional?.Trim(),
                    Naturalidade = pessoaScaDto.dsnatural?.Trim(),
                    Email = string.IsNullOrEmpty(pessoaScaDto.email?.Trim())
                            ? user.Email
                            : pessoaScaDto.email?.Trim(),
                    NomeMae = pessoaScaDto.nmmae?.Trim(),
                    NomePai = pessoaScaDto.nmpai?.Trim(),
                    Sexo = PessoaAntiCorruption.SexoFromSca2Habilitacao(int.Parse(pessoaScaDto.cdsexo)),
                    Escolaridade = (PessoaEscolaridadeEnum)PessoaAntiCorruption
                        .EscolaridadeFromSca2Habilitacao(pessoaScaDto.cdnivel),
                    EstadoCivil = (PessoaEstadoCivilEnum)PessoaAntiCorruption
                        .EstadoCivilFromSca2Habilitacao(pessoaScaDto.cdestcivil),
                    UltimaSerie = pessoaScaDto.nuultserie?.Trim(),
                    TipoDocumento = PessoaTipoDocumentoEnum.rg,
                    Numero = pessoaScaDto.nureggeral?.Trim(),
                    DataEmissao = pessoaScaDto.dtemirg,
                    DataVencimento = pessoaScaDto.dtvencto,
                    OrgaoEmissor = pessoaScaDto.idorgemirg?.Trim(),
                    InformacaoProfissional = new InformacaoProfissional
                    {
                        CNPJ = empresaSca.nucgccei?.Trim(),
                        NomeEmpresa = empresaSca.nmfantasia?.Trim(),
                        NumeroCTPS = pessoaScaDto.nuctps?.Trim(),
                        Ocupacao = pessoaScaDto.dscargo?.Trim(),
                        DataAdmissao = pessoaScaDto.dtadmissao,
                        Renda = decimal.Parse(pessoaScaDto.vlrenda?.Replace(".", ",").Trim())
                    },
                    Contato = new Contato
                    {
                        Email = string.IsNullOrEmpty(pessoaScaDto.email?.Trim())
                            ? user.Email
                            : pessoaScaDto.email?.Trim(),
                        Cep = enderecoSca.nucep?.Trim(),
                        Bairro = enderecoSca.dsbairro?.Trim(),
                        Logradouro = enderecoSca.dslogradou?.Trim(),
                        Complemento = enderecoSca.dscomplem?.Trim(),
                        CidadeId = cidade.Id,
                        Cidade = cidade,
                        Numero = "",
                        TelefonePrincipal = "(" + pessoaScaDto.dddCelular?.Trim() + ") " + pessoaScaDto.celular?.Trim(),
                        TelefoneSecundario = pessoaScaDto.telefone?.Trim(),
                    }
                }
            };

            if (dependentesScaDto != null && dependentesScaDto.Count > 0)
            {
                solicitacao.Titular.Dependentes = new List<Dependente>();

                dependentesScaDto.ToList().ForEach(dependenteSca =>
                {
                    var dependenteDto = GetDependenteDtoFromPessoaSca(dependenteSca);
                    var dependente = _mapper.Map<Dependente>(dependenteDto);

                    dependente.Acao = (tipo == SolicitacaoTipoEnum.renovacao || tipo == SolicitacaoTipoEnum.mudancacategoria)
                        ? AcaoEnum.SemAlteracao
                        : AcaoEnum.Inclusao;
                    dependente.ContatoId = null;
                    dependente.InformacaoProfissionalId = null;
                    dependente.Id = 0;
                    dependente.Titular = solicitacao.Titular;

                    solicitacao.Titular.Dependentes.Add(
                        dependente
                    );
                });
            }

            Sanitize<Solicitacao>(solicitacao);

            return _repository.Incluir(solicitacao);
        }

        public bool HasSolicitacaoTipoEmAndamento(SolicitacaoTipoEnum tipo)
        {
            var user = _userAuthenticatedAuthService.GetUserAuthenticated();

            var solicitacao = _repository.FindBy(
                s => s.Cpf == user.CpfCnpj &&
                    s.Tipo == tipo &&
                    s.Situacao != SolicitacaoSituacaoEnum.reprovada &&
                    s.Situacao != SolicitacaoSituacaoEnum.aprovada &&
                    s.Situacao != SolicitacaoSituacaoEnum.cancelada
            ).FirstOrDefault();

            return solicitacao != null;
        }

        public SolicitacaoDto CriarNovaSolicitacaoRenovarCartao()
        {
            var user = _userAuthenticatedAuthService.GetUserAuthenticated();
            var pessoaSca = _pessoaScaService.GetPessoa(user.CpfCnpj);
            var grupoFamiliar = _pessoaScaService.GetGrupoFamiliar(user.CpfCnpj);

            if (pessoaSca == null)
            {
                throw new Exception("Antes de solicitar a renovação de cartão é necessário possuir um cartão SESC ativo.");
            }

            if ((new DateTime(pessoaSca.dtvencto.Year, pessoaSca.dtvencto.Month, 1) > DateTime.Now)
                   && grupoFamiliar != null
                   && grupoFamiliar.Select(d => new DateTime(d.dtvencto.Year, d.dtvencto.Month, 1) > DateTime.Now).All(d => d == true)
            )
            {
                throw new Exception("Este cartão SESC não está vencido para que seja realizada a renovação.");
            }

            int categoria = PessoaAntiCorruption.CategoriaFromSca2Habilitacao(pessoaSca.cdcategori);

            if (categoria != (int)SolicitacaoCategoriaEnum.Comerciario)
            {
                throw new Exception("A renovação de cartão está disponível apenas para a categoria de trabalhador do comercio.");
            }

            if (HasSolicitacaoTipoEmAndamento(SolicitacaoTipoEnum.renovacao))
            {
                throw new Exception("Você já possui uma solicitação de renovação em andamento, verifique na listagem das solicitações.");
            }

            var solicitacao = _repository.FindByInclude(
                s => s.Cpf == user.CpfCnpj &&
                    s.Tipo == SolicitacaoTipoEnum.renovacao &&
                    s.Situacao == SolicitacaoSituacaoEnum.cadastro,
                t => t.Titular
            ).FirstOrDefault();

            solicitacao = solicitacao == null
                ? MakeNewSolicitacaoFromSca(SolicitacaoTipoEnum.renovacao, pessoaSca, grupoFamiliar)
                : solicitacao;

            return _mapper.Map<SolicitacaoDto>(solicitacao);
        }

        private void Sanitize<T>(T t, bool deep = true)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                var typeName = property.PropertyType.Name;
                var propertyName = property.Name;
                var value = t.GetType().GetProperty(propertyName).GetValue(t);

                if (value == null) continue;

                if (deep == false)
                {
                    if (typeName == "String")
                    {
                        var upperValueWithoutSpecialChars = StringHelper.RemoveDiacritics(value.ToString()).ToUpper();
                        property.SetValue(t, upperValueWithoutSpecialChars);
                    }

                    continue;
                }
                else if (propertyName == "Dependentes" && ((IEnumerable<Dependente>)value).Count() > 0)
                {
                    typeName = "Dependentes";
                }

                switch (typeName)
                {
                    case "String":
                        var upperValueWithoutSpecialChars = StringHelper.RemoveDiacritics(value.ToString()).ToUpper();
                        property.SetValue(t, upperValueWithoutSpecialChars);
                        break;
                    case "Titular":
                        Sanitize<Titular>((Titular)value);
                        break;
                    case "Responsavel":
                        Sanitize<Responsavel>((Responsavel)value, false);
                        break;
                    case "Contato":
                        Sanitize<Contato>((Contato)value);
                        break;
                    case "Cidade":
                        Sanitize<Cidade>((Cidade)value);
                        break;
                    case "Estado":
                        Sanitize<Estado>((Estado)value);
                        break;
                    case "InformacaoProfissional":
                        Sanitize<InformacaoProfissional>((InformacaoProfissional)value, false);
                        break;
                    case "Dependentes":
                        ((IEnumerable<Dependente>)value).ToList().ForEach(dependente => Sanitize<Dependente>(dependente, false));
                        break;
                    default:
                        break;
                }
            }
        }

        public SolicitacaoDto CriarNovaSolicitacaoMudancaCategoria()
        {
            var user = _userAuthenticatedAuthService.GetUserAuthenticated();
            var pessoaSca = _pessoaScaService.GetPessoa(user.CpfCnpj);
            var grupoFamiliar = _pessoaScaService.GetGrupoFamiliar(user.CpfCnpj);

            if (pessoaSca == null)
            {
                throw new Exception("Antes de solicitar a mudança de categoria é necessário possuir um cartão SESC.");
            }

            int categoria = PessoaAntiCorruption.CategoriaFromSca2Habilitacao(pessoaSca.cdcategori);

            if (categoria == (int)SolicitacaoCategoriaEnum.Comerciario)
            {
                throw new Exception("Você já é Titular do Cartão Sesc, não é possível alterar a categoria.");
            }

            var today = DateTime.Now;
            var birthdate = pessoaSca.dtnascimen;
            var idade = today.Year - birthdate.Year;
            if (birthdate > today.AddYears(-idade)) idade--;

            if (idade < 16)
            {
                throw new Exception("Antes de solicitar a mudança de categoria é necessário possuir um cartão SESC.");
            }

            if (HasSolicitacaoTipoEmAndamento(SolicitacaoTipoEnum.mudancacategoria))
            {
                throw new Exception("Você já possui uma solicitação de mudança de categoria em andamento, verifique na listagem das solicitações.");
            }

            var solicitacao = _repository.FindByInclude(
                s => s.Cpf == user.CpfCnpj &&
                    s.Tipo == SolicitacaoTipoEnum.mudancacategoria &&
                    s.Situacao == SolicitacaoSituacaoEnum.cadastro,
                t => t.Titular
            ).FirstOrDefault();

            if (solicitacao == null)
            {
                solicitacao = MakeNewSolicitacaoFromSca(SolicitacaoTipoEnum.mudancacategoria, pessoaSca, grupoFamiliar);
            }

            return _mapper.Map<SolicitacaoDto>(solicitacao);
        }

        public void RemoverRenovacaoDependente(int solicitacaoId, int dependenteId)
        {
            try
            {
                var solicitacao = GetById(solicitacaoId).Result;

                if (solicitacao == null)
                {
                    throw new Exception("Nenhum dependente enviado ou solicitação ativa não encontrada.");
                }

                if (solicitacao.Situacao != SolicitacaoSituacaoEnum.cadastro &&
                    solicitacao.Situacao != SolicitacaoSituacaoEnum.aguardando &&
                    solicitacao.Situacao != SolicitacaoSituacaoEnum.aguardandoretorno
                )
                {
                    throw new Exception("Esta solicitação já está em analise e não pode mais ser alterada.");
                }

                var searchedDependente = solicitacao.Titular.Dependentes
                    .Where(d => d.Id == dependenteId)
                    .FirstOrDefault();

                if (searchedDependente == null)
                {
                    throw new Exception("Dependente não encontrado na base de dependentes ativos.");
                }

                var dependente = _dependenteRepository.FindBy(d => d.Id == searchedDependente.Id)
                    .FirstOrDefault();

                dependente.Acao = AcaoEnum.Exclusao;

                _repository.Alterar(solicitacao);
            }
            catch (Exception e)
            {
                if (e.InnerException != null) throw e.InnerException;
                throw e;
            }
        }
    }
}
