using Microsoft.Extensions.Options;
using Sesc.CrossCutting.Config;
using Sesc.CrossCutting.ServiceAgents.Clientela.Contracts;
using Sesc.MeuSesc.SharedKernel.Authentication.Models;
using Sesc.MeuSesc.SharedKernel.Authentication.Repository.Contracts;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Sesc.CrossCutting.ServiceAgents.Clientela
{
    public class SescApiCentralAtendimento : ISescApiCentralAtendimento
    {

        public readonly HttpClient _Client;
        private readonly string _UrlBase;
        public SescApiCentralAtendimento(
            IAuthServerAuthenticationRepository authServerAuthenticationRepository, 
            IOptions<ApiConfig> apiConfig
        ) {
            _UrlBase = apiConfig.Value.CentralAtendimento;

            _Client = new HttpClient();
            _Client.DefaultRequestHeaders.Accept.Clear();
            _Client.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            TokenAuth tokenAuth = authServerAuthenticationRepository.GetToken();
            _Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenAuth.TokenType, tokenAuth.AccessToken);

        }

        public string GetUrlBase()
        {
            return _UrlBase;
        }

        public HttpClient GetHttpClient()
        {
            return _Client;
        }
    }
}
