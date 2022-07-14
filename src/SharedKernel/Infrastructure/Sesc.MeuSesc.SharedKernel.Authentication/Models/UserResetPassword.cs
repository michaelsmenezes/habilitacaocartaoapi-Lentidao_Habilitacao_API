using System.Runtime.Serialization;

namespace Sesc.MeuSesc.SharedKernel.Authentication.Models
{
    [DataContract]
    public class UserResetPassword
    {
        [DataMember(Name = "user")]
        public UserAuth User { get; set; }
        [DataMember(Name = "code")]
        public string Code { get; set; }
    }
}
