using Sesc.CrossCutting.ServiceAgents.Jasper.Model.Base;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Sesc.CrossCutting.ServiceAgents.Jasper.Model
{
    [DataContract]
    public class InputControlResourceJasper : JasperModel
    {
        [DataMember(Name = "inputControl")]
        public IList<InputControlJasper> InputControl { get; set; }

        public InputControlResourceJasper()
        {
            InputControl = new List<InputControlJasper>();
        }
    }
}
