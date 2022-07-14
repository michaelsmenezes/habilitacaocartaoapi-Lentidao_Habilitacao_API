using System;
using System.Collections.Generic;
using System.Text;

namespace Sesc.MeuSesc.SharedKernel.Authentication.Config
{

    public class AuthClient
    {
        public string ClientId { get; set; }
        public string ClientSecrect { get; set; }
        public string Scope { get; set; }
    }
}
