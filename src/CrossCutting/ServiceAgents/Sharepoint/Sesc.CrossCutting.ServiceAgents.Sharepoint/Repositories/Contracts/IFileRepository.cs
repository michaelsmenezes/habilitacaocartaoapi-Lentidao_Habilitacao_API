
using Sesc.CrossCutting.ServiceAgents.Sharepoint.Entitites;
using System.IO;
using System.Threading.Tasks;

namespace Sesc.CrossCutting.ServiceAgents.Sharepoint.Repositories.Contracts
{
    public interface IFileRepository
    {
        Task<FileSharepoint> Upload(MemoryStream stream, string dir, string fileName);
        Task<MemoryStream> Download(string serverRelativeUrl);
        Task<bool> CheckIn(string serverRelativeUrl);
        Task<bool> Delete(string serverRelativeUrl);
    }
}
