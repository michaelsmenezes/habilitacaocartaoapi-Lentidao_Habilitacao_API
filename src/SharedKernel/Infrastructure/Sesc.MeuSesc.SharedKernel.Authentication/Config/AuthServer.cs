using System;
using System.Collections.Generic;
using System.Text;

namespace Sesc.MeuSesc.SharedKernel.Authentication.Config
{
    public class AuthServer
    {
        public string Authority { get; set; }
        public string ApiName { get; set; }
        public string ApiSecret { get; set; }
        public List<string> AllowedScopes { get; set; }
        public bool RequireHttpsMetadata { get; set; }
        public bool AutomaticAuthenticate { get; set; }
        public bool AutomaticChallenge { get; set; }
    }
}
