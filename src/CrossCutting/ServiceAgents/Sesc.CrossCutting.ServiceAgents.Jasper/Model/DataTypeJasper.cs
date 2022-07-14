using Sesc.CrossCutting.ServiceAgents.Jasper.Model.Base;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Sesc.CrossCutting.ServiceAgents.Jasper.Model
{
    [DataContract]
    public class DataTypeJasper : JasperModel
    {
        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "strictMax")]
        public string StrictMax { get; set; }

        [DataMember(Name = "strictMin")]
        public string StrictMin { get; set; }
    }
}
