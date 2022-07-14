using System;
using System.Collections.Generic;
using System.Text;

namespace Sesc.CrossCutting.ServiceAgents.AuthServer.Dto
{
    public class ForgotPasswordDTO
    {
        public bool Success { get; set; }
        public string Email { get; set; }
    }
}
