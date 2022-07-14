using System;
using System.Collections.Generic;
using System.Text;

namespace Sesc.CrossCutting.ServiceAgents.AuthServer.Dto
{
    public class UsuarioCadastradoDTO
    {
        public UserCreatedDTO UserCreated { get; set; }

        public bool Success { get; set; }
    }
}
