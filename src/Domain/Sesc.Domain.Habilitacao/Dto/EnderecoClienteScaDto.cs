using Sesc.MeuSesc.SharedKernel.Infrastructure.Dto;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sesc.Domain.Habilitacao.Dto
{
    public class EnderecoClienteScaDto : DtoBase
    {
        [Column("dsmunicip")]
        public string Municipio { get; set; }

        [Column("dsbairro")]
        public string Bairro { get; set; }

        [Column("nucep")]
        public string Cep { get; set; }

        [Column("cdmunicip")]
        public string CodMunicipio { get; set; }

        [Column("dscomplem")]
        public string Complemento { get; set; }

        [Column("siestado")]
        public string SiglaEstado { get; set; }

        [Column("dslogradou")]
        public string Logradouro { get; set; }
    }
}
