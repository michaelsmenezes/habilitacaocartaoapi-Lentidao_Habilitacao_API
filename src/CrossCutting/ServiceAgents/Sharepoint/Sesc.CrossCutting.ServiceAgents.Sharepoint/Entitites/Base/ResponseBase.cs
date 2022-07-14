using System.Runtime.Serialization;

namespace Sesc.CrossCutting.ServiceAgents.Sharepoint.Entitites.Base
{
    [DataContract]
    public class ResponseBase
    {
        public int StatusCode { get; set; }
        public bool IsSuccessStatusCode { get; set; }
    }
}
