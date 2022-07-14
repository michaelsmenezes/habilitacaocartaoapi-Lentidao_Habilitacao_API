
using Sesc.CrossCutting.ServiceAgents.Sharepoint.Entitites;
using System.IO;
using System.Threading.Tasks;

namespace Sesc.CrossCutting.ServiceAgents.Sharepoint.Services.Contracts
{
    public interface IFileService
    {
        Task<FileSharepoint> Upload(MemoryStream stream, string fileName);
        Task<MemoryStream> Download(string serverRelativeUrl);
        Task<bool> CheckIn(string serverRelativeUrl);
        Task<bool> Delete(string serverRelativeUrl);
    }
}
