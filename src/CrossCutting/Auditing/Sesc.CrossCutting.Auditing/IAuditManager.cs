using System;
using Auditing.EventArgs;
using System.Threading.Tasks;

namespace Auditing
{
    public interface IAuditManager
    {
        void ConfigureAuditEnvironment(Action<AuditEnvironment> environment);

        void OnAuditEntity(AuditEventArgs eventArgs);

        Task<AuditQueryResponse> ViewRecordsAsync(string EntidadeNome, string EntidadeNamespace, string EntidadeId);
    }

}
