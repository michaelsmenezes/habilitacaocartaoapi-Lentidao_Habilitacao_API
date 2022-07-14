using Sesc.CrossCutting.ServiceAgents.Sharepoint.Entitites;
using System.Threading.Tasks;

namespace Sesc.CrossCutting.ServiceAgents.Sharepoint.Services.Contracts
{
    public interface IFolderService
    {
        Task<Folder> Create(string name);
        Task<Folder> Get(string ralativeDir);
        Task<Folder> AutomaticGenerateFolder();

    }
}
