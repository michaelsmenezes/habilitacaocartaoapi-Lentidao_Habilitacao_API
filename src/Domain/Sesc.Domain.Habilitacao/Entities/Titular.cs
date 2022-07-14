using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sesc.Domain.Habilitacao.Entities
{
    public class Titular : Pessoa
    {
        public int? ResponsavelId { get; set; }
        public Responsavel Responsavel{ get; set; }

        public ICollection<Dependente> Dependentes { get; set; }

        public void ChangeTitular(Titular newTitular)
        {
            if (newTitular == null) return ;

            this.Cpf = newTitular.Cpf;
            this.DataEmissao = newTitular.DataEmissao;
            this.DataNascimento = newTitular.DataNascimento;
            this.Escolaridade = newTitular.Escolaridade;
            this.EstadoCivil = newTitular.EstadoCivil;
            this.Nacionalidade = newTitular.Nacionalidade;
            this.Naturalidade = newTitular.Naturalidade;
            this.Nome = newTitular.Nome;
            this.NomePai = newTitular.NomePai;
            this.NomeSocial = newTitular.NomeSocial;
            this.Numero = newTitular.Numero;
            this.OrgaoEmissor = newTitular.OrgaoEmissor;
            this.Sexo = newTitular.Sexo;
            this.TipoDocumento = newTitular.TipoDocumento;
            this.UltimaSerie = newTitular.UltimaSerie;
            this.DataVencimento = newTitular.DataVencimento;
        }
    }
}
