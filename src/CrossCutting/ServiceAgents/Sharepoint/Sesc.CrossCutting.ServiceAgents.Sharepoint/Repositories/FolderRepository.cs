using Microsoft.Extensions.Options;
using Sesc.CrossCutting.ServiceAgents.Sharepoint.Config;
using Sesc.CrossCutting.ServiceAgents.Sharepoint.Entitites;
using Sesc.CrossCutting.ServiceAgents.Sharepoint.Repositories.Contracts;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Sesc.CrossCutting.ServiceAgents.Sharepoint.Repositories
{
    public class FolderRepository : IFolderRepository
    {
        private readonly DataContractJsonSerializerSettings _serializerSettings;
        private readonly DataContractJsonSerializerSettings _deserializerSettings;
        private readonly HttpClient _apiClient;
        private readonly SharePointConfig _sharePointConfig;

        public FolderRepository(IOptions<SharePointConfig> options, IAuthTokenRepostiory tokenAtuthService)
        {
            _apiClient = new HttpClient();
            _apiClient.BaseAddress = new Uri(options.Value.BaseUrl);
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _sharePointConfig = options.Value;

            var tokenAuthServer = tokenAtuthService.GetTokenClientCredentials().Result;
            _apiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenAuthServer.Type, tokenAuthServer.AccessToken);

            _serializerSettings = new DataContractJsonSerializerSettings
            {
                DateTimeFormat = new DateTimeFormat("yyyy-MM-dd'T'HH:mm:ss") { DateTimeStyles = System.Globalization.DateTimeStyles.AdjustToUniversal }
            };

            _deserializerSettings = new DataContractJsonSerializerSettings
            {
                DateTimeFormat = new DateTimeFormat("yyyy-MM-dd'T'HH:mm:ss") { DateTimeStyles = System.Globalization.DateTimeStyles.AdjustToUniversal }
            };
        }
        public async Task<Folder> CreateFolder(string name)
        {
            var serializer = new DataContractJsonSerializer(typeof(Folder), _deserializerSettings);

            var response = await _apiClient.PostAsync("folders/add('" + _sharePointConfig.BaseFolder + name + "')", null);

            if (response.IsSuccessStatusCode)
            {
                var folder = serializer.ReadObject(await response.Content.ReadAsStreamAsync()) as Folder;
                folder.StatusCode = (int)response.StatusCode;
                folder.IsSuccessStatusCode = response.IsSuccessStatusCode;

                return folder;
            }
            else
            {
                return new Folder { IsSuccessStatusCode = response.IsSuccessStatusCode, StatusCode = (int)response.StatusCode };
            }
        }

        public async Task<Folder> GetFolder(string ralativeDir)
        {
            var serializer = new DataContractJsonSerializer(typeof(Folder), _deserializerSettings);

            var response = await _apiClient.GetAsync("GetFolderByServerRelativeUrl('" + _sharePointConfig.BaseFolder + ralativeDir + "')");

            if (response.IsSuccessStatusCode)
            {
                var folder = serializer.ReadObject(await response.Content.ReadAsStreamAsync()) as Folder;
                folder.StatusCode = (int)response.StatusCode;
                folder.IsSuccessStatusCode = response.IsSuccessStatusCode;

                return folder;
            }
            else
            {
                return new Folder { IsSuccessStatusCode = response.IsSuccessStatusCode, StatusCode = (int)response.StatusCode };
            }
        }
    }
}
