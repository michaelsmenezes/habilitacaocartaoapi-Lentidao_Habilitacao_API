using Microsoft.Extensions.Options;
using Sesc.CrossCutting.ServiceAgents.Sharepoint.Config;
using Sesc.CrossCutting.ServiceAgents.Sharepoint.Entitites;
using Sesc.CrossCutting.ServiceAgents.Sharepoint.Repositories.Contracts;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace Sesc.CrossCutting.ServiceAgents.Sharepoint.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly DataContractJsonSerializerSettings _serializerSettings;
        private readonly DataContractJsonSerializerSettings _deserializerSettings;
        private readonly HttpClient _apiClient;
        private readonly SharePointConfig _sharePointConfig;

        public FileRepository(IOptions<SharePointConfig> options, IAuthTokenRepostiory tokenAtuthService)
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

        public async Task<MemoryStream> Download(string serverRelativeUrl)
        {
            HttpResponseMessage response = await _apiClient.GetAsync("GetFileByServerRelativeUrl('" + serverRelativeUrl + "')/$value", HttpCompletionOption.ResponseHeadersRead);

            var memoryStream = new MemoryStream(await response.Content.ReadAsByteArrayAsync());

            return memoryStream;
        }

        public async Task<FileSharepoint> Upload(MemoryStream stream, string dir, string fileName)
        {
            //var memoryStream = new MemoryStream();
            //await stream.CopyToAsync(memoryStream);

            var content = new ByteArrayContent(stream.ToArray());

            var response = await _apiClient.PostAsync("GetFolderByServerRelativeUrl('" + dir + "/')/Files/add(url='" + fileName + "',overwrite=true)", content);

            var a = await response.Content.ReadAsStringAsync();

            FileSharepoint file = null;

            if (response.IsSuccessStatusCode)
            {
                var serializerResult = new DataContractJsonSerializer(typeof(FileSharepoint), _deserializerSettings);
                file = serializerResult.ReadObject(response.Content.ReadAsStreamAsync().Result) as FileSharepoint;
            }

            return file;
        }

        public async Task<bool> CheckIn(string serverRelativeUrl)
        {

            var response = await _apiClient.PostAsync("GetFileByServerRelativeUrl('" + serverRelativeUrl + "')/CheckIn(comment='Auto comment',checkintype=0)", null);

            var a = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> Delete(string serverRelativeUrl)
        {

            HttpResponseMessage response = await _apiClient.DeleteAsync("GetFileByServerRelativeUrl('" + serverRelativeUrl + "')");

            var a = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
    }
}
