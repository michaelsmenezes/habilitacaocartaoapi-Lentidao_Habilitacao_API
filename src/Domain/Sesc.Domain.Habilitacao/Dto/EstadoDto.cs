using Sesc.MeuSesc.SharedKernel.Infrastructure.Dto;

namespace Sesc.Domain.Habilitacao.Dto
{
    public class EstadoDto : DtoBase
    {
        public int Id { get; set; }
        public string Uf { get; set; }
        public string Descricao { get; set; }
    }
}
