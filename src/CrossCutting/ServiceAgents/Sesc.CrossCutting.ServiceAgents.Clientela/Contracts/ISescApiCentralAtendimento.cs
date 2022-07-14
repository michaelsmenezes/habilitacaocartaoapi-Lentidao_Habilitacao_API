using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Sesc.CrossCutting.ServiceAgents.Clientela.Contracts
{
    public interface ISescApiCentralAtendimento
    {
        string GetUrlBase();
        HttpClient GetHttpClient();
    }
}
