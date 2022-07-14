using Sesc.MeuSesc.SharedKernel.Infrastructure.ViewModel;

namespace Sesc.Domain.Habilitacao.ViewModel
{
    public class EnderecoClienteViewModel : ViewModelBase
    {
        public string Municipio { get; set; }

        public string Bairro { get; set; }

        public string Cep { get; set; }

        public int CodMunicipio { get; set; }

        public string Complemento { get; set; }

        public string SiglaEstado { get; set; }

        public string Logradouro { get; set; }
    }
}
