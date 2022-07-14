using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Sesc.MeuSesc.SharedKernel.Authentication.Models
{
    [DataContract]
    public class ChangePassword
    {
        [DataMember(Name = "Email")]
        public string Email { get; set; }
        [DataMember(Name = "Cpf")]
        public string Cpf { get; set; }
        [DataMember(Name = "Password")]
        public string Password { get; set; }
        [DataMember(Name = "ConfirmPassword")]
        public string ConfirmPassword { get; set; }
        [DataMember(Name = "OldPassword")]
        public string OldPassword { get; set; }
    }
}
