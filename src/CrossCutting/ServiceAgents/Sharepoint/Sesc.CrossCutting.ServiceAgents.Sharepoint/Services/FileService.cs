using Sesc.CrossCutting.ServiceAgents.Sharepoint.Entitites;
using Sesc.CrossCutting.ServiceAgents.Sharepoint.Repositories.Contracts;
using Sesc.CrossCutting.ServiceAgents.Sharepoint.Services.Contracts;
using System.IO;
using System.Threading.Tasks;

namespace Sesc.CrossCutting.ServiceAgents.Sharepoint.Services
{
    public class FileService : IFileService
    {
        private readonly IFileRepository _repository;
        private readonly IFolderService _folderService;

        public FileService(IFileRepository repository, IFolderService folderService)
        {
            _repository = repository;
            _folderService = folderService;
        }

        public async Task<bool> CheckIn(string serverRelativeUrl)
        {
            return await _repository.CheckIn(serverRelativeUrl);
        }

        public async Task<bool> Delete(string serverRelativeUrl)
        {
            return await _repository.Delete(serverRelativeUrl);
        }

        public async Task<MemoryStream> Download(string serverRelativeUrl)
        {
            return await _repository.Download(serverRelativeUrl);
        }

        public async Task<FileSharepoint> Upload(MemoryStream stream, string fileName)
        {
            var folder = await _folderService.AutomaticGenerateFolder();

            var file = await _repository.Upload(stream, folder.ServerRelativeUrl, fileName);

            await CheckIn(file.ServerRelativeUrl);

            return file;
        }
    }
}
