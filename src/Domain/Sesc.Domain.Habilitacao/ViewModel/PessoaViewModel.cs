using Sesc.Domain.Habilitacao.Enum;
using Sesc.MeuSesc.SharedKernel.Infrastructure.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Sesc.Domain.Habilitacao.ViewModel
{
    abstract public class PessoaViewModel : ViewModelBase
    {
        public PessoaViewModel()
        {
            Documentos = new Collection<DocumentoViewModel>();
        }

        public int? Id { get; set; }
        public string Nome { get; set; }
        public string Sexo { get; set; }
        public string NomeSocial { get; set; }
        public DateTime DataNascimento { get; set; }
        public string  Cpf { get; set; }
        public string Email { get; set; }
        public string NomeMae { get; set; }
        public string NomePai { get; set; }
        public int? ContatoId { get; set; }
        public virtual ContatoViewModel Contato { get; set; }
        public int? InformacaoProfissionalId { get; set; }
        public InformacaoProfissionalViewModel InformacaoProfissional { get; set; }
        public PessoaEstadoCivilEnum EstadoCivil { get; set; }
        public PessoaEscolaridadeEnum Escolaridade { get; set; }
        public string UltimaSerie { get; set; }
        public string Nacionalidade { get; set; }
        public string Naturalidade { get; set; }
        public PessoaTipoDocumentoEnum TipoDocumento { get; set; }
        public string Numero { get; set; }
        public string OrgaoEmissor { get; set; }
        public DateTime? DataEmissao { get; set; }
        public DateTime? DataVencimento { get; set; }
        public ICollection<DocumentoViewModel> Documentos { get; set; }
    }
}
