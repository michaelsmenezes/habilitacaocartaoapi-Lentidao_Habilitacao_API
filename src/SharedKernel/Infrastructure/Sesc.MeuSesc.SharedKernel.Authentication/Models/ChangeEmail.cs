using System.Runtime.Serialization;

namespace Sesc.MeuSesc.SharedKernel.Authentication.Models
{
    [DataContract]
    public class ChangeEmail
    {
        [DataMember(Name = "User")]
        public UserAuth UserAuth { get; set; }

        [DataMember(Name = "NewEmail")]
        public string NewEmail { get; set; }

        [DataMember(Name = "Code")]
        public string Code { get; set; }

    }
}
