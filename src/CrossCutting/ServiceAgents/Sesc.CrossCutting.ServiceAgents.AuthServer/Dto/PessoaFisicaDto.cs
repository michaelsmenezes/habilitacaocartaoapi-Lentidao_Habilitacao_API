using System;
using System.Collections.Generic;
using System.Text;

namespace Sesc.CrossCutting.ServiceAgents.AuthServer.Dto
{
    public class PessoaFisicaDto : PessoaDto
    {
        public string Matricula { get; set; }
        public string RG { get; set; }
        public string Emissor { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Quadra { get; set; }
        public string Lote { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public Nullable<decimal> EstadoPaisId { get; set; }
        public string Cep { get; set; }
        public System.DateTime DataNascimento { get; set; }
        public string Nacionalidade { get; set; }
        public string Naturalidade { get; set; }
        public string Sexo { get; set; }
        public Nullable<decimal> EstadoCivil { get; set; }

    }
}
