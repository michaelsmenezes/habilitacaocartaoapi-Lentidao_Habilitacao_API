using Sesc.MeuSesc.SharedKernel.Authentication.Models.Bases;
using System.Runtime.Serialization;


namespace Sesc.MeuSesc.SharedKernel.Authentication.Models
{
    [DataContract]
    public class RoleAuth : ModelAuthBase
    {
        [DataMember(Name = "roleId")]
        public string RoleId { get; set; }
    }
}
