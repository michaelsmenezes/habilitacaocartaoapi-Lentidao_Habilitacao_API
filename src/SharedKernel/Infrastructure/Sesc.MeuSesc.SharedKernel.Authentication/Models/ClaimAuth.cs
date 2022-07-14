using Sesc.MeuSesc.SharedKernel.Authentication.Models.Bases;
using System.Runtime.Serialization;

namespace Sesc.MeuSesc.SharedKernel.Authentication.Models
{
    [DataContract]
    public class ClaimAuth : ModelAuthBase
    {
        [DataMember(Name = "claimType")]
        public string ClaimType { get; set; }

        [DataMember(Name = "claimValue")]
        public string ClaimValue { get; set; }
    }
}
