using Sesc.CrossCutting.ServiceAgents.Jasper.Model.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Sesc.CrossCutting.ServiceAgents.Jasper.Api
{
    public class JasperserverRestClient
    {
        private string baseurl;
        private AuthenticationHeaderValue authentication;

        /// <summary>
        /// Util to retrieve a report from JasperServer.
        /// https://github.com/netinhoteixeira/jrs-rest-csharp-client
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password for Username</param>
        /// <param name="baseurl">JasperReports</param>
        /// <example>
        /// JasperserverRestClient jasperserverRestClient = new JasperserverRestClient("username", "password", "http://localhost:8080/jasperserver/rest_v2/reports");
        /// Stream stream = jasperserverRestClient.Get("/reports/SGV/Total.pdf");
        /// 
        /// // or
        /// 
        /// JasperserverRestClient jasperserverRestClient = new JasperserverRestClient("username", "password", "http://localhost:8080/jasperserver/rest_v2/reports");
        /// 
        /// Dictionary<string, string> parameters = new Dictionary<string, string>();
        /// parameters.Add("PARAM1", "VALUE1");
        /// parameters.Add("PARAM2", "VALUE2");
        /// 
        /// Stream stream = jasperserverRestClient.Get("/reports/SGV/Total.pdf", parameters);
        /// </example>
        public JasperserverRestClient(string username, string password, string baseurl)
        {
            if (String.IsNullOrEmpty(username))
            {
                throw new NullReferenceException("Username could not be empty.");
            }

            if (String.IsNullOrEmpty(password))
            {
                throw new NullReferenceException("Password could not be empty.");
            }

            if (String.IsNullOrEmpty(password))
            {
                throw new NullReferenceException("Base URL could not be empty.");
            }

            var byteArray = Encoding.ASCII.GetBytes(username + ":" + password);
            var encoded = Convert.ToBase64String(byteArray);
            this.authentication = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", encoded);

            this.baseurl = baseurl;
        }

        /// <summary>
        /// Retrieve the stream of report asked.
        /// </summary>
        /// <param name="report">Report path at server</param>
        /// <returns>Stream of report asked</returns>
        public Stream Get(string report)
        {
            return this.Get(report, null);
        }

        /// <summary>
        /// Retrieve the stream of report asked using parameters.
        /// </summary>
        /// <param name="report">Report path at server</param>
        /// <param name="parameters">Parameters of report</param>
        /// <returns>Stream of report asked using parameters</returns>
        public MemoryStream Get(string report, Dictionary<string, string> parameters)
        {
            if (String.IsNullOrEmpty(report))
            {
                throw new NullReferenceException("Report could not be empty.");
            }

            string url = this.baseurl + report;

            if ((parameters != null) && (parameters.Count > 0))
            {
                List<String> items = new List<String>();

                foreach (var pair in parameters)
                {
                    items.Add(String.Concat(pair.Key, "=", WebUtility.UrlEncode(pair.Value)));
                }

                url += "?" + String.Join("&", items.ToArray()); ;
            }

            MemoryStream ms = new MemoryStream();
            this.GetTask(url, ms);

            return ms;
        }

        /// <summary>
        /// Retrieve a task of url.
        /// </summary>
        /// <param name="url">Url to retrieve content</param>
        /// <param name="stream">Stream to save the url content</param>
        private /*async*/ void GetTask(string url, MemoryStream stream)
        {
            try
            {
                var handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = (request, cert, chain, errors) =>
                {
                    return true;
                };
                HttpClient client = new HttpClient(handler);

                client.DefaultRequestHeaders.Authorization = authentication;

                var task = client.GetStreamAsync(url);
                Task.WaitAll(task);

                task.Result.CopyTo(stream);
            }
            catch (Exception e)
            {
                throw new Exception("Ocorreu um problema de conexão com servidor (Jasper).");
            }
        }

        public async Task<T> Get<T>(string endpoint) where T : JasperModel
        {
            T result = null;

            try
            {
                var serializer = new DataContractJsonSerializer(typeof(T));

                var handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = (request, cert, chain, errors) =>
                {
                    return true;
                };
                HttpClient client = new HttpClient(handler);

                client.DefaultRequestHeaders.Authorization = authentication;
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var streamTask = client.GetStreamAsync(this.baseurl + endpoint);

                result = serializer.ReadObject(await streamTask) as T;
            }
            catch (SerializationException)
            {
                throw new Exception("Ocorreu um problema de conexão com servidor (Jasper).");
            }
            catch (Exception err)
            {
                throw new Exception("Ocorreu um problema de conexão com servidor (Jasper).");
            }

            return result;
        }
    }
}
