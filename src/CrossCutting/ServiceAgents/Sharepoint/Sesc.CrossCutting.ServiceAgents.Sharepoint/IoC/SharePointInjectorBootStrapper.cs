
using Microsoft.Extensions.DependencyInjection;
using Sesc.CrossCutting.ServiceAgents.Sharepoint.Repositories;
using Sesc.CrossCutting.ServiceAgents.Sharepoint.Repositories.Contracts;
using Sesc.CrossCutting.ServiceAgents.Sharepoint.Services;
using Sesc.CrossCutting.ServiceAgents.Sharepoint.Services.Contracts;

namespace Sesc.CrossCutting.ServiceAgents.Sharepoint.IoC
{
    public class SharePointInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IAuthTokenRepostiory, AuthTokenRepostiory>();
            services.AddTransient<IFolderRepository, FolderRepository>();
            services.AddTransient<IFileRepository, FileRepository>();

            services.AddTransient<IFolderService, FolderService>();
            services.AddTransient<IFileService, FileService>();
        }
    }
}
