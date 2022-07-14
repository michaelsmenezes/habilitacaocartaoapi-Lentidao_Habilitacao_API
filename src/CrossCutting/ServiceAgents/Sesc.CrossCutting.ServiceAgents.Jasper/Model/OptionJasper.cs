using Sesc.CrossCutting.ServiceAgents.Jasper.Model.Base;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Sesc.CrossCutting.ServiceAgents.Jasper.Model
{
    [DataContract]
    public class OptionJasper : JasperModel
    {
        [DataMember(Name = "label")]
        public string Label { get; set; }

        [DataMember(Name = "selected")]
        public bool Selected { get; set; }

        [DataMember(Name = "value")]
        public string Value { get; set; }
    }
}
