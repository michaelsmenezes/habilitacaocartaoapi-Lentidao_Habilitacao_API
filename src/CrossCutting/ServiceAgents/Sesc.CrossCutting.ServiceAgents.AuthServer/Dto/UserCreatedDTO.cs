using Sesc.MeuSesc.SharedKernel.Authentication.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sesc.CrossCutting.ServiceAgents.AuthServer.Dto
{
    public class UserCreatedDTO
    {
        public UserAuth UserAuth { get; set; }

        public string Code { get; set; }
    }
}
