
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
    public class AuthTokenRepostiory : IAuthTokenRepostiory
    {
        private readonly DataContractJsonSerializerSettings _serializerSettings;
        private readonly DataContractJsonSerializerSettings _deserializerSettings;
        private readonly HttpClient _apiClient;
        private readonly SharePointConfig _sharePointConfig;

        public AuthTokenRepostiory(IOptions<SharePointConfig> options)
        {
            _apiClient = new HttpClient();
            _apiClient.BaseAddress = new Uri(options.Value.AuthConfig.BaseUrl);
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

            _sharePointConfig = options.Value;


            _serializerSettings = new DataContractJsonSerializerSettings
            {
                DateTimeFormat = new DateTimeFormat("yyyy-MM-dd'T'HH:mm:ss") { DateTimeStyles = System.Globalization.DateTimeStyles.AdjustToUniversal }
            };

            _deserializerSettings = new DataContractJsonSerializerSettings
            {
                DateTimeFormat = new DateTimeFormat("yyyy-MM-dd'T'HH:mm:ss") { DateTimeStyles = System.Globalization.DateTimeStyles.AdjustToUniversal }
            };
        }

        public async Task<Token> GetTokenClientCredentials()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, _sharePointConfig.AuthConfig.TenantId + "/tokens/OAuth/2");

            var requestContent = string.Format(
                "client_id={0}&client_secret={1}&grant_type={2}&resource={3}",
                Uri.EscapeDataString(_sharePointConfig.AuthConfig.ClientId + "@" + _sharePointConfig.AuthConfig.TenantId),
                Uri.EscapeDataString(_sharePointConfig.AuthConfig.ClientSecret),
                Uri.EscapeDataString("client_credentials"),
                Uri.EscapeDataString(_sharePointConfig.AuthConfig.Resource + "/" + _sharePointConfig.AuthConfig.Domain+ "@" +_sharePointConfig.AuthConfig.TenantId)
            );

            Token token = null;

            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/x-www-form-urlencoded");

            var response = await _apiClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var serializerResult = new DataContractJsonSerializer(typeof(Token), _deserializerSettings);
                token = serializerResult.ReadObject(response.Content.ReadAsStreamAsync().Result) as Token;
            }

            return token;
        }

    }
}
