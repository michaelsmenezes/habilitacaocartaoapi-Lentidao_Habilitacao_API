using Sesc.MeuSesc.SharedKernel.Infrastructure.Dto;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sesc.Domain.Habilitacao.Dto
{
    public class ClienteScaDto : DtoBase
    {
        public decimal Id { get; set; }
        [Column("nmcliente")]
        public string Nome { get; set; }

        [Column("cdsexo")]
        public string CodSexo { get; set; }

        [Column("dtnascimen")]
        public string DataNascimento { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("telefone")]
        public string Telefone { get; set; }

        [Column("celular")]
        public string Celular { get; set; }

        [Column("nucpf")]
        public string Cpf { get; set; }

        [NotMapped]
        public EnderecoClienteScaDto Endereco { get; set; }
    }
}
