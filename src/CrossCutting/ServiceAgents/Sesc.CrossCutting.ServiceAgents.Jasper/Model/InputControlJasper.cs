using Sesc.CrossCutting.ServiceAgents.Jasper.Model.Base;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Sesc.CrossCutting.ServiceAgents.Jasper.Model
{
    [DataContract]
    public class InputControlJasper : JasperModel
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "label")]
        public string Label { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "uri")]
        public string Uri { get; set; }

        [DataMember(Name = "mandatory")]
        public bool Mandatory { get; set; }

        [DataMember(Name = "state")]
        public StateJasper State { get; set; }

        [DataMember(Name = "dataType")]
        public DataTypeJasper DataType { get; set; }

        [DataMember(Name = "visible")]
        public bool Visible { get; set; }
    }
}
