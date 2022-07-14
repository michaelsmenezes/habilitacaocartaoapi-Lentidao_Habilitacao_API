using System;
using System.Collections.Generic;
using System.Text;

namespace Sesc.CrossCutting.ServiceAgents.AuthServer.ViewModel
{
    public class ConfirmChangeEmailViewModel
    {
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Code { get; set; }
    }
}
