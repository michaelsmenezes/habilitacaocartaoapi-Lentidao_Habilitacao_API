using System;
using System.Collections.Generic;
using System.Text;

namespace Sesc.CrossCutting.ServiceAgents.Jasper.Model
{
    public class GenerateReportCommand
    {
        public string Path { get; set; }

        public Dictionary<string, string> Parameters { get; set; }
    }
}
