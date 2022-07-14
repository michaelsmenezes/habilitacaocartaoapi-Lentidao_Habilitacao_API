using Sesc.MeuSesc.SharedKernel.Infrastructure.Dto;

namespace Sesc.Domain.Habilitacao.Dto
{
    public class ArquivoDto : DtoBase
    {
        public string Nome { get; set; }
        public string Documento { get; set; }
        public string ContentType { get; set; }
    }
}
