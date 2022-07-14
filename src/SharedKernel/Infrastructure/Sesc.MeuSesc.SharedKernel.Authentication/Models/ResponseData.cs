using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Sesc.MeuSesc.SharedKernel.Authentication.Models
{
    [DataContract]
    public class ResponseData
    {
        [DataMember(Name = "Error")]
        public IList<string> Erros { get; set; }

        public bool RequestSuccess { get; set; }
    }
}
