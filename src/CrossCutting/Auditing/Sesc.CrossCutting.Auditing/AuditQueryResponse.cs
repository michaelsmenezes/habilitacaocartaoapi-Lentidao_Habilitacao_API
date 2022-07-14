using Auditing.Trail;
using System.Collections.Generic;

namespace Auditing
{
    public class AuditQueryResponse
    {
        public string ResultStatus { get; set; }
        public string ResultMessage { get; set; }
        public IEnumerable<EntityAuditTrail> ResultItems { get; set; }
    }
}
