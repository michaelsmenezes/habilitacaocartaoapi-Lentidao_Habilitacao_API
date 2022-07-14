using Sesc.MeuSesc.SharedKernel.Infrastructure.Entities;

namespace Sesc.Domain.Habilitacao.Entities
{
    public class Contato : EntityBase
    {
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Numero { get; set; }
        public string Cep { get; set; }
        public string Bairro { get; set; }
        public int? CidadeId { get; set; }
        public virtual Cidade Cidade { get; set; }
        public string TelefonePrincipal { get; set; }
        public string TelefoneSecundario { get; set; }
        public string Email { get; set; }

        public void ChangeContato(Contato contatoNew)
        {
            if (contatoNew == null) return ;

            this.Logradouro = contatoNew?.Logradouro;
            this.Complemento = contatoNew?.Complemento;
            this.Numero = contatoNew?.Numero;
            this.Cep = contatoNew?.Cep;
            this.Bairro = contatoNew?.Bairro;
            this.CidadeId = contatoNew?.CidadeId;
            this.TelefonePrincipal = contatoNew?.TelefonePrincipal;
            this.TelefoneSecundario = contatoNew?.TelefoneSecundario;
            this.Email = contatoNew?.Email;
        }
    }
}
