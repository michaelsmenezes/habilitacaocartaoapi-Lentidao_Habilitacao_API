using Sesc.CrossCutting.ServiceAgents.Jasper.Model.Base;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Sesc.CrossCutting.ServiceAgents.Jasper.Model
{
    [DataContract]
    public class ResourceLookupJasper : JasperModel
    {
        [DataMember(Name = "resourceLookup")]
        public IList<ResourceJasper> ResourceLookup { get; set; }

        public ResourceLookupJasper()
        {
            ResourceLookup = new List<ResourceJasper>();
        }
    }
}
