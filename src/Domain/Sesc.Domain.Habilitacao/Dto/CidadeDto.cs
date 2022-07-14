using Sesc.MeuSesc.SharedKernel.Infrastructure.Dto;

namespace Sesc.Domain.Habilitacao.Dto
{
    public class CidadeDto : DtoBase
    {
        public int Id { get; set; }
        public string CodigoIBGE { get; set; }
        public string Descricao { get; set; }
        public int EstadoId { get; set; }
        public EstadoDto Estado { get; set; }
        public int? CidadeResponsavelId { get; set; }
        public virtual CidadeDto CidadeResponsavel { get; set; }
    }
}
