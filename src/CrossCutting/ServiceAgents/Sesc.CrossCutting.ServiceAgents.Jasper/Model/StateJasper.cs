using Sesc.CrossCutting.ServiceAgents.Jasper.Model.Base;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Sesc.CrossCutting.ServiceAgents.Jasper.Model
{
    [DataContract]
    public class StateJasper : JasperModel
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "uri")]
        public string Uri { get; set; }

        [DataMember(Name = "value")]
        public string Value { get; set; }

        [DataMember(Name = "options")]
        public List<OptionJasper> Options { get; set; }
    }
}
