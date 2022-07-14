using Sesc.Domain.Habilitacao.Enum;
using Sesc.MeuSesc.SharedKernel.Infrastructure.Entities;
using System;

namespace Sesc.Domain.Habilitacao.Entities
{
    public class Documento : EntityBase
    {
        public int PessoaId { get; set; }
        public Pessoa Pessoa { get; set; }
        public DocumentoTipoEnum Tipo { get; set; } 
        public string Extensao { get; set; }
        public string Url { get; set; }
        public string MimeType { get; set; }
        public string Nome { get; set; }
        public DateTime DataRegistro { get; set; }
        public DocumentoSituacaoEnum Situacao { get; set; }

        public void ChangeDocumento(Documento newDocumento)
        {
            if (newDocumento == null) return;

            this.Situacao = newDocumento.Situacao;
        }
    }
}
