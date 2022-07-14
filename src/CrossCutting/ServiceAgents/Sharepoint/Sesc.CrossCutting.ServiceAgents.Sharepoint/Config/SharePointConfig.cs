using System;
using System.Collections.Generic;
using System.Text;

namespace Sesc.CrossCutting.ServiceAgents.Sharepoint.Config
{
    public class SharePointConfig
    {
        public string BaseUrl { get; set; }
        public string BaseFolder { get; set; }
        public Auth AuthConfig { get; set; }

        public class Auth
        {
            public string ClientId { get; set; }
            public string BaseUrl { get; set; }
            public string ClientSecret { get; set; }
            public string Resource { get; set; }
            public string TenantId { get; set; }
            public string Domain { get; set; }
        }
    }
}
