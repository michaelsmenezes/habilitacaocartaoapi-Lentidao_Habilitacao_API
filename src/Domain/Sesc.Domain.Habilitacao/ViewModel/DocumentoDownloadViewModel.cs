using System.IO;

namespace Sesc.Domain.Habilitacao.ViewModel
{
    public class DocumentoDownloadViewModel
    {
        public byte[] ArrayBytes { get; set; }
        public string NomeArquivo { get; set; }
        public string MimeType { get; set; }
    }
}
