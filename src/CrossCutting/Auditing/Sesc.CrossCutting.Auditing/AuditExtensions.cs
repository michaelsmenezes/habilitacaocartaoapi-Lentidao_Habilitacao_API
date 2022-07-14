
using Microsoft.AspNetCore.Builder;

namespace Auditing
{
    public static class AuditExtensions
    {
        public static IApplicationBuilder UseAuditManager(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuditMiddleware>();
        }

    }
}
