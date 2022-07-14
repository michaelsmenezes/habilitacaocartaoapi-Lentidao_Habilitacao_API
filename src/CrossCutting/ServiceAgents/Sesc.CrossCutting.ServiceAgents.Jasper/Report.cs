using System;
using System.IO;

namespace Sesc.CrossCutting.ServiceAgents.Jasper
{
    public class Report
    {
        public Report(MemoryStream arquivo)
        {
            this.Arquivo = Convert.ToBase64String(arquivo.ToArray());
            this.ArquivoMemoryStream = arquivo;
        }

        public Report()
        {
        }

        public string Arquivo { get; set; }
        public MemoryStream ArquivoMemoryStream { get; set; }
    }
}
