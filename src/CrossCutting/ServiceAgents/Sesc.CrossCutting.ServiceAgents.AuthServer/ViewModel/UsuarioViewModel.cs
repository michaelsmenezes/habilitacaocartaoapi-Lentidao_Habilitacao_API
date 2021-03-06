using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sesc.CrossCutting.ServiceAgents.AuthServer.ViewModel
{
    public class UsuarioViewModel
    {
        public string Nome { get; set; }
        public string DataNascimento { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string ConfirmarSenha { get; set; }
    }
}
