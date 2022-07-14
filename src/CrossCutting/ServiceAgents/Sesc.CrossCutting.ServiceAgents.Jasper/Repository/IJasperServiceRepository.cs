using Sesc.CrossCutting.ServiceAgents.Jasper.Model;
using System.Threading.Tasks;

namespace Sesc.CrossCutting.ServiceAgents.Jasper.Repository
{
    public interface IJasperServiceRepository
    {
        Task<ResourceLookupJasper> GetAllReports(string reportFolder);
        Task<InputControlResourceJasper> GetInputControlByReportUri(string reportUri);
        Report GetReport(GenerateReportCommand command);
    }
}
