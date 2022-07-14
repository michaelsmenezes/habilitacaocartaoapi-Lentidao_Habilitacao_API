using System.Runtime.Serialization;

namespace Sesc.MeuSesc.SharedKernel.Authentication.Models
{
    [DataContract]
    public class UserCreated
    {
        [DataMember(Name = "user")]
        public UserAuth UserAuth { get; set; }

        [DataMember(Name = "code")]
        public string Code { get; set; }
    }
}
