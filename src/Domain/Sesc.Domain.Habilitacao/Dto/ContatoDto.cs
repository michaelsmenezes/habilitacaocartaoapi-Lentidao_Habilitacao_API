using Sesc.MeuSesc.SharedKernel.Infrastructure.Dto;

namespace Sesc.Domain.Habilitacao.Dto
{
    public class ContatoDto : DtoBase
    {
        public int Id { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Numero { get; set; }
        public string Cep { get; set; }
        public string Bairro { get; set; }
        public int CidadeId { get; set; }
        public CidadeDto Cidade { get; set; }
        public string TelefonePrincipal { get; set; }
        public string TelefoneSecundario { get; set; }
        public string Email { get; set; }
    }
}
