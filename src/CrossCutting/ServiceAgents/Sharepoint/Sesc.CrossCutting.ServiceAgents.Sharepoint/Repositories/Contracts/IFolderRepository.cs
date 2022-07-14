
using Sesc.CrossCutting.ServiceAgents.Sharepoint.Entitites;
using System.Threading.Tasks;

namespace Sesc.CrossCutting.ServiceAgents.Sharepoint.Repositories.Contracts
{
    public interface IFolderRepository
    {
        Task<Folder> CreateFolder(string name);
        Task<Folder> GetFolder(string ralativeDir);
    }
}
