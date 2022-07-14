using System;
using System.Runtime.Serialization;

namespace Sesc.MeuSesc.SharedKernel.Authentication.Models
{
    [DataContract]
    public class ChangeCode
    {
        [DataMember(Name ="code")]
        public string Code { get; set; }
    }
}
