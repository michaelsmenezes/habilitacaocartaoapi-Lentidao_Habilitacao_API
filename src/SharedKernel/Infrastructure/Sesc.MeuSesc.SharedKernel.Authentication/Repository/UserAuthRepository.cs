using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Sesc.MeuSesc.SharedKernel.Authentication.Repository.Contracts;
using Sesc.MeuSesc.SharedKernel.Authentication.Config;
using Sesc.MeuSesc.SharedKernel.Authentication.Models;
using Microsoft.Extensions.Options;

namespace Sesc.MeuSesc.SharedKernel.Authentication.Repository
{
    public class UserAuthRepository : IUserAuthRepository
    {
        protected readonly DataContractJsonSerializerSettings _serializerSettings;
        protected readonly DataContractJsonSerializerSettings _deserializerSettings;
        protected readonly HttpClient _apiClient;
        protected readonly AuthClient _authClientOptions;
        protected readonly IAuthServerAuthenticationRepository _authServerAuthenticationRepository;

        public UserAuthRepository(IOptions<AuthServer> options, IOptions<AuthClient> authClientOptions, IAuthServerAuthenticationRepository authServerAuthenticationRepository)
        {
            _authClientOptions = authClientOptions.Value;

            _apiClient = new HttpClient();
            _apiClient.BaseAddress = new Uri(options.Value.Authority);
            _apiClient.DefaultRequestHeaders.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _serializerSettings = new DataContractJsonSerializerSettings();
            _deserializerSettings = new DataContractJsonSerializerSettings();

            _authServerAuthenticationRepository = authServerAuthenticationRepository;

            var tokenAuth = _authServerAuthenticationRepository.GetToken();
            _apiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenAuth.TokenType, tokenAuth.AccessToken);
        }

        public async Task<UserAuth> UpdateUser(string email, UserAuth user)
        {
            var serializer = new DataContractJsonSerializer(typeof(UserAuth), _serializerSettings);

            var ms = new MemoryStream();
            serializer.WriteObject(ms, user);
            ms.Position = 0;

            var requestContent = Encoding.UTF8.GetString(ms.ToArray());

            var content = new StringContent(requestContent, Encoding.UTF8, "application/json");

            var result = await _apiClient.PutAsync("api/User/email/" + email, content);

            if (result.IsSuccessStatusCode)
            {
                var serializerResult = new DataContractJsonSerializer(typeof(UserAuth), _deserializerSettings);
                var userUpdated = serializerResult.ReadObject(result.Content.ReadAsStreamAsync().Result) as UserAuth;

                userUpdated.StatusCode = (int)result.StatusCode;
                userUpdated.IsSuccessStatusCode = result.IsSuccessStatusCode;

                return userUpdated;
            }

            else
            {
                return new UserAuth
                {
                    StatusCode = (int)result.StatusCode,
                    IsSuccessStatusCode = result.IsSuccessStatusCode
                };
            }
        }

        public async Task<UserAuth> FindByEmail(string email)
        {
            var serializer = new DataContractJsonSerializer(typeof(UserAuth), _deserializerSettings);

            var response = await _apiClient.GetAsync("api/User/email/" + email);

            if (response.IsSuccessStatusCode)
            {
                var userAuth = serializer.ReadObject(await response.Content.ReadAsStreamAsync()) as UserAuth;
                userAuth.StatusCode = (int)response.StatusCode;
                userAuth.IsSuccessStatusCode = response.IsSuccessStatusCode;

                return userAuth;
            }
            else
            {
                return new UserAuth { IsSuccessStatusCode = response.IsSuccessStatusCode, StatusCode = (int)response.StatusCode };
            }
        }

        public async Task<UserAuth> FindByCpf(string cpf)
        {
            var serializer = new DataContractJsonSerializer(typeof(UserAuth), _deserializerSettings);

            var response = await _apiClient.GetAsync("api/User/cpf/" + cpf);

            if (response.IsSuccessStatusCode)
            {
                var userAuth = serializer.ReadObject(await response.Content.ReadAsStreamAsync()) as UserAuth;
                userAuth.StatusCode = (int)response.StatusCode;
                userAuth.IsSuccessStatusCode = response.IsSuccessStatusCode;

                return userAuth;
            }
            else
            {
                return new UserAuth { IsSuccessStatusCode = response.IsSuccessStatusCode, StatusCode = (int)response.StatusCode };
            }
        }

        public async Task<UserCreated> CreateUser(UserAuth user)
        {
            var serializer = new DataContractJsonSerializer(typeof(UserAuth), _serializerSettings);

            var ms = new MemoryStream();
            serializer.WriteObject(ms, user);
            ms.Position = 0;

            var requestContent = Encoding.UTF8.GetString(ms.ToArray());

            var content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            var result = await _apiClient.PostAsync("api/User", content);

            if (result.IsSuccessStatusCode)
            {
                var serializerResult = new DataContractJsonSerializer(typeof(UserCreated), _deserializerSettings);
                var userCreated = serializerResult.ReadObject(result.Content.ReadAsStreamAsync().Result) as UserCreated;

                userCreated.UserAuth.StatusCode = (int)result.StatusCode;
                userCreated.UserAuth.IsSuccessStatusCode = result.IsSuccessStatusCode;

                return userCreated;
            }
            else
            {
                return new UserCreated
                {
                    UserAuth = new UserAuth
                    {
                        StatusCode = (int)result.StatusCode,
                        IsSuccessStatusCode = result.IsSuccessStatusCode
                    },
                    Code = ""
                };
            }
        }

        public async Task<ResponseData> ChangePassword(ChangePassword change)
        {
            var serializer = new DataContractJsonSerializer(typeof(ChangePassword), _serializerSettings);

            var ms = new MemoryStream();
            serializer.WriteObject(ms, change);
            ms.Position = 0;

            var requestContent = Encoding.UTF8.GetString(ms.ToArray());

            var content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            var result = await _apiClient.PostAsync("api/User/changePassword", content);

            if (result.StatusCode < System.Net.HttpStatusCode.InternalServerError)
            {

                var serializerResult = new DataContractJsonSerializer(typeof(ResponseData), _deserializerSettings);
                var reponseData = serializerResult.ReadObject(result.Content.ReadAsStreamAsync().Result) as ResponseData;

                reponseData.RequestSuccess = result.IsSuccessStatusCode;

                return reponseData;
            }
            else
            {
                Response(new ResponseData
                {
                    RequestSuccess = false
                });
                return null;
            }

        }

        public async Task<ChangeCode> ChangeEmail(ChangeEmail changeEmail)
        {
            var serializer = new DataContractJsonSerializer(typeof(ChangeEmail), _serializerSettings);

            var ms = new MemoryStream();
            serializer.WriteObject(ms, changeEmail);
            ms.Position = 0;

            var requestContent = Encoding.UTF8.GetString(ms.ToArray());

            var content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            var result = await _apiClient.PostAsync("api/User/changeEmail", content);

            if (result.StatusCode < System.Net.HttpStatusCode.InternalServerError)
            {

                var serializerResult = new DataContractJsonSerializer(typeof(ChangeCode), _deserializerSettings);
                var changeCode = serializerResult.ReadObject(result.Content.ReadAsStreamAsync().Result) as ChangeCode;

                if (changeCode.Code == null)
                {
                    Response(new ResponseData
                    {
                        RequestSuccess = false
                    });
                }

                return changeCode;
            }
            else
            {
                Response(new ResponseData
                {
                    RequestSuccess = false
                });
                return null;
            }

        }

        public async Task<ResponseData> ConfirmChangeEmail(ChangeEmail changeEmail)
        {
            var serializer = new DataContractJsonSerializer(typeof(ChangeEmail), _serializerSettings);

            var ms = new MemoryStream();
            serializer.WriteObject(ms, changeEmail);
            ms.Position = 0;

            var requestContent = Encoding.UTF8.GetString(ms.ToArray());

            var content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            var result = await _apiClient.PostAsync("api/User/confirmChangeEmail", content);

            if (result.StatusCode < System.Net.HttpStatusCode.InternalServerError)
            {

                var serializerResult = new DataContractJsonSerializer(typeof(ResponseData), _deserializerSettings);
                var reponseData = serializerResult.ReadObject(result.Content.ReadAsStreamAsync().Result) as ResponseData;

                reponseData.RequestSuccess = result.IsSuccessStatusCode;

                return reponseData;
            }
            else
            {
                Response(new ResponseData
                {
                    RequestSuccess = false
                });
                return null;
            }

        }

        public async Task<ChangeCode> RequestResetPassword(UserAuth user)
        {
            var serializer = new DataContractJsonSerializer(typeof(UserAuth), _serializerSettings);

            var ms = new MemoryStream();
            serializer.WriteObject(ms, user);
            ms.Position = 0;

            var requestContent = Encoding.UTF8.GetString(ms.ToArray());

            var content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            var result = await _apiClient.PostAsync("api/User/requestResetPassword", content);

            if (result.StatusCode < System.Net.HttpStatusCode.InternalServerError)
            {

                var serializerResult = new DataContractJsonSerializer(typeof(ChangeCode), _deserializerSettings);
                var changeCode = serializerResult.ReadObject(result.Content.ReadAsStreamAsync().Result) as ChangeCode;

                if (changeCode.Code == null)
                {
                    Response(new ResponseData
                    {
                        RequestSuccess = false
                    });
                }

                return changeCode;
            }
            else
            {
                Response(new ResponseData
                {
                    RequestSuccess = false
                });
                return null;
            }
        }

        public async Task<ResponseData> ResetPassword(UserResetPassword user)
        {
            var serializer = new DataContractJsonSerializer(typeof(UserResetPassword), _serializerSettings);

            var ms = new MemoryStream();
            serializer.WriteObject(ms, user);
            ms.Position = 0;

            var requestContent = Encoding.UTF8.GetString(ms.ToArray());

            var content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            var result = await _apiClient.PostAsync("api/User/resetPassword", content);

            if (result.StatusCode < System.Net.HttpStatusCode.InternalServerError)
            {

                var serializerResult = new DataContractJsonSerializer(typeof(ResponseData), _deserializerSettings);
                var reponseData = serializerResult.ReadObject(result.Content.ReadAsStreamAsync().Result) as ResponseData;

                reponseData.RequestSuccess = result.IsSuccessStatusCode;

                return reponseData;
            }
            else
            {
                Response(new ResponseData
                {
                    RequestSuccess = false
                });
                return null;
            }
        }

        private void Response(ResponseData response)
        {

            if (response != null && !response.RequestSuccess)
            {
                if (response.Erros == null)
                {
                    //ContentSingleton.AddMessage("Ops, ocorreu um erro ao executar a operação. Por favor tente novamente mais tarde.");
                    //ContentSingleton.Dispatch();
                }

                //response.Erros.ToList().ForEach(e =>
                //{
                //    ContentSingleton.AddMessage(e);
                //    ContentSingleton.Dispatch();
                //});
            }
        }
        public async Task<bool> RequestResetByEmail(string email)
        {
            var result = await _apiClient.GetAsync("api/User/RequestResetByEmail/" + email);

            if (result.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
