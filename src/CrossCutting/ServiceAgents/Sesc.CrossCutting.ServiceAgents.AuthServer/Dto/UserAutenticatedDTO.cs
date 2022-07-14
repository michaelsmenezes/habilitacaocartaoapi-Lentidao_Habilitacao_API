using System;
using System.Collections.Generic;
using System.Text;

namespace Sesc.CrossCutting.ServiceAgents.AuthServer.Dto
{
    public class UserAutenticatedDTO
    {
        public string Email { get; set; }

        //public string Matricula { get; set; }

        public string Cpf { get; set; }
    }
}
