using Microsoft.AspNetCore.Http;
using Sesc.Domain.Habilitacao.Enum;

namespace Sesc.Domain.Habilitacao.ViewModel
{
    public class UploadDocumentoViewModel
    {
        public int Id { get; set; }
        public int PessoaId { get; set; }
        public DocumentoTipoEnum TipoDocumento { get; set; }
        public IFormFile Arquivo { get; set; }
    }
}
