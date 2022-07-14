using Microsoft.Extensions.Options;
using Sesc.CrossCutting.ServiceAgents.Sharepoint.Config;
using Sesc.CrossCutting.ServiceAgents.Sharepoint.Entitites;
using Sesc.CrossCutting.ServiceAgents.Sharepoint.Repositories.Contracts;
using Sesc.CrossCutting.ServiceAgents.Sharepoint.Services.Contracts;
using System;
using System.Threading.Tasks;

namespace Sesc.CrossCutting.ServiceAgents.Sharepoint.Services
{
    public class FolderService : IFolderService
    {
        private readonly IFolderRepository _folderRepository;
        private readonly SharePointConfig _sharePointConfig;

        public FolderService(IFolderRepository folderRepository, IOptions<SharePointConfig> options)
        {
            _folderRepository = folderRepository;
            _sharePointConfig = options.Value;
        }

        public async Task<Folder> Create(string name)
        {
            return await _folderRepository.CreateFolder(name);
        }

        public async Task<Folder> Get(string ralativeDir)
        {
            return await _folderRepository.GetFolder(ralativeDir);
        }

        public async Task<Folder> AutomaticGenerateFolder()
        {
            return await CreateFolderMonth();
        }

        private async Task<Folder> CreateFolderMonth()
        {
            var FolderYear = await CreateFolderYear();

            var month = DateTime.Now.Month;
            var dir = FolderYear.Nome + "/" + month;

            var currentFolder = await Get(dir);

            if (currentFolder == null || !(currentFolder.Exists))
            {
                currentFolder = await _folderRepository.CreateFolder(dir);
            }

            return currentFolder;

        }

        private async Task<Folder> CreateFolderYear()
        {
            var ano = DateTime.Now.Year;

            var currentFolder = await Get(ano.ToString());

            if(currentFolder == null || !(currentFolder.Exists))
            {
                currentFolder = await Create(ano.ToString());
            }

            return currentFolder;
        }
    }
}
