using Sesc.CrossCutting.ServiceAgents.Sharepoint.Entitites.Base;
using System.Runtime.Serialization;

namespace Sesc.CrossCutting.ServiceAgents.Sharepoint.Entitites
{
    [DataContract]
    public class Token : ResponseBase
    {
        [DataMember(Name = "token_type")]
        public string Type { get; set; }

        [DataMember(Name = "expires_in")]
        public string ExpiresIn { get; set; }

        [DataMember(Name = "not_before")]
        public string Before { get; set; }

        [DataMember(Name = "expires_on")]
        public string ExpiresOn { get; set; }

        [DataMember(Name = "resource")]
        public string Resource { get; set; }

        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }
    }
}
