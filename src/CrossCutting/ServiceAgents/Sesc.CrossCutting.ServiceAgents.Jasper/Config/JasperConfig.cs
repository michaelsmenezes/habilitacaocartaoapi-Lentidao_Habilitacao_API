using System;
using System.Collections.Generic;
using System.Text;

namespace Sesc.CrossCutting.ServiceAgents.Jasper.Config
{
    public class JasperConfig
    {
        public string ApiUrl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserLocale { get; set; }
        public string UserTimezone { get; set; }
    }
}
