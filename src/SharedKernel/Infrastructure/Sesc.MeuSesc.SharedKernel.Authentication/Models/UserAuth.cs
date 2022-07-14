using Sesc.MeuSesc.SharedKernel.Authentication.Models.Bases;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Sesc.MeuSesc.SharedKernel.Authentication.Models
{
    [DataContract]
    public class UserAuth : ResponseUserAuth
    {
        public UserAuth()
        {
            this.Claims = new List<ClaimAuth>();
            this.Roles = new List<RoleAuth>();
        }

        [DataMember(Name = "userName")]
        public string UserName { get; set; }

        [DataMember(Name = "password")]
        public string Password { get; set; }

        [DataMember(Name = "confirmPassword")]
        public string ConfirmPassword { get; set; }

        [DataMember(Name = "roles")]
        public List<RoleAuth> Roles { get; set; }

        [DataMember(Name = "claims")]
        public List<ClaimAuth> Claims { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "cpf")]
        public string CpfCnpj { get; set; }

        [DataMember(Name = "matricula")]
        public string Matricula { get; set; }

        [DataMember(Name = "emailConfirmed")]
        public bool EmailConfirmed { get; set; }
    }
}
