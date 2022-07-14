using AutoMapper;
using Sesc.Application.ApplicationServices.Queries.Contracts;
using Sesc.CrossCutting.ServiceAgents.Sharepoint.Services.Contracts;
using Sesc.Domain.Habilitacao.Services.Contracts;
using Sesc.Domain.Habilitacao.ViewModel;
using Sesc.MeuSesc.Infrastructure.EntityFramework.Extensions;
using System;
using System.Threading.Tasks;

namespace Sesc.Application.ApplicationServices.Queries
{
    public class DocumentoQueryServiceApplication : IDocumentoQueryServiceApplication
    {
        protected readonly IDocumentoService _documentoService;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public DocumentoQueryServiceApplication(
            IDocumentoService documentoService,
            IMapper mapper,
            IFileService fileService
        )
        {
            _documentoService = documentoService;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<DocumentoDownloadViewModel> Download(int Id)
        {
            try
            {
                var documento = _mapper.Map<DocumentoViewModel>(await _documentoService.GetById(Id));
                var memoryStream = await _fileService.Download(documento.Url);

                var DocumentoDownload = new DocumentoDownloadViewModel
                {
                    NomeArquivo = documento.Tipo.GetDisplayName() + " - " + Guid.NewGuid(),
                    MimeType = documento.MimeType,
                    ArrayBytes = memoryStream.ToArray()
                };

                return DocumentoDownload;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

    }
}
