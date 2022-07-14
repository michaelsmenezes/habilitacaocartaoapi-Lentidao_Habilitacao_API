using Sesc.Domain.Habilitacao.Enum;
using System;

namespace Sesc.Domain.Habilitacao.ViewModel
{
    public class DocumentoViewModel
    {
        public int Id { get; set; }
        //public PessoaViewModel Pessoa { get; set; }
        public DocumentoTipoEnum Tipo { get; set; }
        public string Extensao { get; set; }
        public string Url { get; set; }
        public string MimeType { get; set; }
        public string Nome { get; set; }
        public DateTime DataRegistro { get; set; }
        public DocumentoSituacaoEnum? Situacao { get; set; }
    }
}
