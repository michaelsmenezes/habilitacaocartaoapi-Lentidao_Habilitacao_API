using Sesc.MeuSesc.SharedKernel.Infrastructure.ViewModel;
using System;

namespace Sesc.Domain.Habilitacao.ViewModel
{
    public class PessoaLogadaViewModel : ViewModelBase
    {
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string DataNascimento { get; set; }
    }
}
