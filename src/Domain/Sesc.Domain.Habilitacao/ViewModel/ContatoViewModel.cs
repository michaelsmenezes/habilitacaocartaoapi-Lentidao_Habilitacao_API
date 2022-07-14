using Sesc.MeuSesc.SharedKernel.Infrastructure.ViewModel;

namespace Sesc.Domain.Habilitacao.ViewModel
{
    public class ContatoViewModel : ViewModelBase
    {
        public int? Id { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Numero { get; set; }
        public string Cep { get; set; }
        public string Bairro { get; set; }
        public int? CidadeId { get; set; }
        public CidadeViewModel Cidade { get; set; }
        public string TelefonePrincipal { get; set; }
        public string TelefoneSecundario { get; set; }
        public string Email { get; set; }
    }
}
