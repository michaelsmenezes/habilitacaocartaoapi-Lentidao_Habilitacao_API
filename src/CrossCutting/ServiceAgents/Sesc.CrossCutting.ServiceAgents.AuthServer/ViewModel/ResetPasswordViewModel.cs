using System;
using System.Collections.Generic;
using System.Text;

namespace Sesc.CrossCutting.ServiceAgents.AuthServer.ViewModel
{
    public class ResetPasswordViewModel
    {
        public string Cpf { get; set; }
        public string Code { get; set; }
        public string novaSenha { get; set; }
        public string confirmNovaSenha { get; set; }
    }
}
