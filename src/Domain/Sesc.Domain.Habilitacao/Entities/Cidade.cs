using Sesc.MeuSesc.SharedKernel.Infrastructure.Entities;

namespace Sesc.Domain.Habilitacao.Entities
{
    public class Cidade : EntityBase
    {
        public string CodigoIBGE { get; set; }
        public string Descricao { get; set; }
        public int EstadoId { get; set; }
        public Estado Estado { get; set; }
        public int? CidadeResponsavelId { get; set; }
        public virtual Cidade CidadeResponsavel { get; set; }

    }
}
