using System;
using System.Collections.Generic;
using System.Text;

namespace Sesc.CrossCutting.ServiceAgents.AuthServer.Dto
{
    public abstract class PessoaDto
    {
        public decimal PessoaId { get; set; }
        public string Nome { get; set; }
        public string CPF_CGC { get; set; }
        public string Email { get; set; }
    }
}
