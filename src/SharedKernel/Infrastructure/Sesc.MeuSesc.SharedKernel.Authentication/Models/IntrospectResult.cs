using System.Runtime.Serialization;

namespace Sesc.MeuSesc.SharedKernel.Authentication.Models
{
    [DataContract]
    public class IntrospectResult
    {
        [DataMember(Name = "active")]
        public bool Active { get; set; }
    }
}
