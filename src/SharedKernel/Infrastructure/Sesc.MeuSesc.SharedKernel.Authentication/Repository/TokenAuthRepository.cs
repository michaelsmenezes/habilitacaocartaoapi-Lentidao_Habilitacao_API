using Microsoft.Extensions.Options;
using Sesc.SharedKernel.Authentication.ViewModel;
using Sesc.MeuSesc.SharedKernel.Authentication.Config;
using Sesc.MeuSesc.SharedKernel.Authentication.Models;
using Sesc.MeuSesc.SharedKernel.Authentication.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Sesc.MeuSesc.SharedKernel.Authentication.Repository
{
    public class TokenAuthRepository : ITokenAuthRepository
    {
        private readonly DataContractJsonSerializerSettings _serializerSettings;
        private readonly DataContractJsonSerializerSettings _deserializerSettings;
        private readonly HttpClient _httpClient;
        private readonly AuthClient _authClientOptions;
        private readonly AuthServer _authServeroptions;

        public TokenAuthRepository(IOptions<AuthServer> options, IOptions<AuthClient> authClientOptions)
        {
            _authClientOptions = authClientOptions.Value;
            _authServeroptions = options.Value;

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(options.Value.Authority);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _serializerSettings = new DataContractJsonSerializerSettings();

            _deserializerSettings = new DataContractJsonSerializerSettings();

        }

        public TokenAuthViewModel GetToken(UserAuth user)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "connect/token");

            var requestContent = string.Format(
                "grant_type={0}&username={1}&password={2}&client_id={3}&client_secret={4}&scope={5}",
                Uri.EscapeDataString("password"),
                Uri.EscapeDataString(user.UserName),
                Uri.EscapeDataString(user.Password),
                Uri.EscapeDataString(_authClientOptions.ClientId),
                Uri.EscapeDataString(_authClientOptions.ClientSecrect),
                Uri.EscapeDataString(_authClientOptions.Scope)
            );

            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/x-www-form-urlencoded");

            var token = this.PostAsync(request).Result;

            return token == null ? null : new TokenAuthViewModel
            {
                AccessToken = token.AccessToken,
                ExpiresIn = token.ExpiresIn,
                RefreshToken = token.RefreshToken,
                TokenType = token.TokenType
            };
        }


        public TokenAuthViewModel GetRefreshedToken(string refreshToken)
        {
            if (refreshToken == null) return null;

            var request = new HttpRequestMessage(HttpMethod.Post, "connect/token");

            var requestContent = string.Format(
                "grant_type={0}&client_id={1}&client_secret={2}&scope={3}&refresh_token={4}",
                Uri.EscapeDataString("refresh_token"),
                Uri.EscapeDataString(_authClientOptions.ClientId),
                Uri.EscapeDataString(_authClientOptions.ClientSecrect),
                Uri.EscapeDataString(_authClientOptions.Scope),
                Uri.EscapeDataString(refreshToken)
            );

            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/x-www-form-urlencoded");

            var token = this.PostAsync(request).Result;

            return token == null ? null : new TokenAuthViewModel
            {
                AccessToken = token.AccessToken,
                ExpiresIn = token.ExpiresIn,
                RefreshToken = token.RefreshToken,
                TokenType = token.TokenType
            };
        }


        public TokenAuthViewModel GetTokenByAuthorizationCode(AuthorizationCodeAuth code)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "connect/token");

            var requestContent = string.Format(
                "client_id={0}&client_secret={1}&scope={2}&grant_type={3}&response_type={4}&code={5}&redirect_uri={6}",
                Uri.EscapeDataString(_authClientOptions.ClientId),
                Uri.EscapeDataString(_authClientOptions.ClientSecrect),
                Uri.EscapeDataString(_authClientOptions.Scope),
                Uri.EscapeDataString("authorization_code"),
                Uri.EscapeDataString("code"),
                Uri.EscapeDataString(code.Code),
                Uri.EscapeDataString(code.Callback)
            );

            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/x-www-form-urlencoded");

            var token = this.PostAsync(request).Result;

            if (token == null)
            {
                return null;
            }

            return new TokenAuthViewModel
            {
                AccessToken = token.AccessToken,
                ExpiresIn = token.ExpiresIn,
                RefreshToken = token.RefreshToken,
                TokenType = token.TokenType
            };
        }

        public TokenAuth GetTokenByClientCredentials()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "connect/token");

            var requestContent = string.Format(
                "client_id={0}&client_secret={1}&scope={2}&grant_type={3}",
                Uri.EscapeDataString(_authClientOptions.ClientId),
                Uri.EscapeDataString(_authClientOptions.ClientSecrect),
                Uri.EscapeDataString(string.Join(" ", _authServeroptions.AllowedScopes)),
                Uri.EscapeDataString("client_credentials")
            );

            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/x-www-form-urlencoded");

            return this.PostAsync(request).Result;
        }

        public async Task<IntrospectResult> Introspect(string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "connect/introspect");

            var requestContent = string.Format(
                "token={0}",
                Uri.EscapeDataString(token)
            );

            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/x-www-form-urlencoded");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes(_authServeroptions.ApiName + ":" + _authServeroptions.ApiSecret)));
            var response = await _httpClient.SendAsync(request);

            var i = new IntrospectResult();

            if (response.IsSuccessStatusCode)
            {
                var serializerResult = new DataContractJsonSerializer(typeof(IntrospectResult), _deserializerSettings);
                i = serializerResult.ReadObject(response.Content.ReadAsStreamAsync().Result) as IntrospectResult;
            }

            return i;
        }

        private async Task<TokenAuth> PostAsync(HttpRequestMessage request)
        {
            TokenAuth token = null;

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var serializerResult = new DataContractJsonSerializer(typeof(TokenAuth), _deserializerSettings);
                token = serializerResult.ReadObject(response.Content.ReadAsStreamAsync().Result) as TokenAuth;
            }

            return token;
        }

    }
}
