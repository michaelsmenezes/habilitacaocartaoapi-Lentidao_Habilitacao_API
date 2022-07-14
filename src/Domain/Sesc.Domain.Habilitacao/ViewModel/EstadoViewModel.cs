using Sesc.MeuSesc.SharedKernel.Infrastructure.ViewModel;

namespace Sesc.Domain.Habilitacao.ViewModel
{
    public class EstadoViewModel : ViewModelBase
    {
        public int? Id { get; set; }
        public string Uf { get; set; }
        public string Descricao { get; set; }
    }
}
