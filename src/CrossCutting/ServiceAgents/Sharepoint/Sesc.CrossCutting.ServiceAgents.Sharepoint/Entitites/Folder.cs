using Sesc.CrossCutting.ServiceAgents.Sharepoint.Entitites.Base;
using System.Runtime.Serialization;

namespace Sesc.CrossCutting.ServiceAgents.Sharepoint.Entitites
{
    [DataContract]
    public class Folder : ResponseBase
    {
        [DataMember(Name ="Name")]
        public string Nome { get; set; }

        [DataMember(Name = "ServerRelativeUrl")]
        public string ServerRelativeUrl { get; set; }

        [DataMember(Name = "UniqueId")]
        public string UniqueId { get; set; }

        [DataMember(Name = "Exists")]
        public bool Exists { get; set; }
    }
}
