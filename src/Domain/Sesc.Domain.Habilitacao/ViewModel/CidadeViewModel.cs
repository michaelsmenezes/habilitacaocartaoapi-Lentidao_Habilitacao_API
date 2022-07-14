using Sesc.MeuSesc.SharedKernel.Infrastructure.ViewModel;

namespace Sesc.Domain.Habilitacao.ViewModel
{
    public class CidadeViewModel
    {
        public int? Id { get; set; }
        public string CodigoIBGE { get; set; }
        public string Descricao { get; set; }
        public int? EstadoId { get; set; }
        public EstadoViewModel Estado { get; set; }
        public int? CidadeResponsavelId { get; set; }
        public virtual CidadeViewModel CidadeResponsavel { get; set; }
        //public string CidadeResponsavelDescricao { get; set; }
    }
}
