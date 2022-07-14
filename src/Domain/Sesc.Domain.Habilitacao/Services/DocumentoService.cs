using AutoMapper;
using Microsoft.AspNetCore.Http;
using Sesc.CrossCutting.ServiceAgents.Sharepoint.Services.Contracts;
using Sesc.CrossCutting.Validation.BaseException;
using Sesc.Domain.Habilitacao.Dto;
using Sesc.Domain.Habilitacao.Entities;
using Sesc.Domain.Habilitacao.Helpers;
using Sesc.Domain.Habilitacao.Repositories.Contracts;
using Sesc.Domain.Habilitacao.Services.Contracts;
using Sesc.Domain.Habilitacao.ViewModel;
using Sesc.MeuSesc.SharedKernel.Application.Services;
using Sesc.MeuSesc.SharedKernel.Infrastructure.UnitOfWork;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Sesc.Domain.Habilitacao.Services
{
    public class DocumentoService : Service<Documento, IDocumentoRepository>, IDocumentoService
    {
        private readonly IFileService _fileService;

        public DocumentoService(
            IUnitOfWork unitOfWork,
            IDocumentoRepository repository,
            IFileService fileService,
            IMapper mapper) : base(unitOfWork, repository, mapper)
        {
            _fileService = fileService;
        }

        public async Task<Documento> GetById(int id)
        {
            return _repository.GetById(id);
        }

        public async Task<DocumentoDto> Alterar(Documento Documento)
        {
            _repository.Alterar(Documento);

            return _mapper.Map<DocumentoDto>(Documento);
        }

        public async Task Incluir(Pessoa pessoa, UploadDocumentoViewModel documentoViewModel)
        {
            try
            {
                if (pessoa == null || documentoViewModel == null)
                    throw new Exception("Não foi possível salvar o arquivo");

                Documento documento = new Documento
                {
                    PessoaId = pessoa.Id,
                    Pessoa = pessoa,
                    Tipo = documentoViewModel.TipoDocumento,
                    Extensao = Path.GetExtension(documentoViewModel.Arquivo.FileName).Replace(".", ""),
                    Url = "",
                    MimeType = documentoViewModel.Arquivo.ContentType,
                    Nome = documentoViewModel.Arquivo.FileName,
                    DataRegistro = DateTime.Now,
                    Situacao = Enum.DocumentoSituacaoEnum.aguardando
                };

                switch (documentoViewModel.TipoDocumento)
                {
                    case Enum.DocumentoTipoEnum.foto:
                        if (!string.Equals(documentoViewModel.Arquivo.ContentType, "image/jpg", StringComparison.OrdinalIgnoreCase) &&
                            !string.Equals(documentoViewModel.Arquivo.ContentType, "image/jpeg", StringComparison.OrdinalIgnoreCase) &&
                            !string.Equals(documentoViewModel.Arquivo.ContentType, "image/png", StringComparison.OrdinalIgnoreCase)
                        )
                        {
                            throw new Exception("O arquivo enviado deve ser PNG ou JPEG.");
                        }
                        break;

                    default:
                        if (!string.Equals(documentoViewModel.Arquivo.ContentType, "image/jpg", StringComparison.OrdinalIgnoreCase) &&
                            !string.Equals(documentoViewModel.Arquivo.ContentType, "image/jpeg", StringComparison.OrdinalIgnoreCase) &&
                            !string.Equals(documentoViewModel.Arquivo.ContentType, "image/png", StringComparison.OrdinalIgnoreCase) &&
                            !string.Equals(documentoViewModel.Arquivo.ContentType, "application/pdf", StringComparison.OrdinalIgnoreCase)
                        )
                        {
                            throw new Exception("O arquivo enviado deve ser PNG, JPEG ou PDF.");
                        }
                        break;
                }


                MemoryStream arquivoUpload;
                if (documento.Extensao != "pdf") // Já passou nos testes acima e ou é imagem ou é pdf
                {
                    // Redimensiona e remove qualidade da imagem
                    arquivoUpload = ImageHelper.Resize(documentoViewModel.Arquivo, 1024);
                    //seta a nova extensão da imagem, sempre será jpg
                    documento.Extensao = "jpg";
                }
                else
                {
                    if (documentoViewModel.Arquivo.Length > (5 * 1024 * 1024))
                    {
                        throw new Exception("O arquivo enviado ultrapassa o limite de 5mb");
                    }

                    // Converte arquivo para memorystream
                    var memoryStream = new MemoryStream();
                    await documentoViewModel.Arquivo.OpenReadStream().CopyToAsync(memoryStream);
                    arquivoUpload = memoryStream;
                }

                var guid = Guid.NewGuid().ToString();
                var fileUpload = _fileService.Upload(arquivoUpload,
                    guid + '.' + documento.Extensao
                ).Result;

                documento.Url = fileUpload.ServerRelativeUrl;

                _repository.Incluir(documento);
            }
            catch (Exception e)
            {
                if (e.InnerException is ApiException) throw e.InnerException;

                ContentSingleton.AddMessage(e.Message);
                ContentSingleton.Dispatch();
            }
        }

        public async Task Deletar(Documento documento)
        {
            if (documento == null) return;

            _repository.Deletar(documento);

            if (string.IsNullOrEmpty(documento.Url)) return;

            _fileService.Delete(documento.Url);
        }
    }
}
