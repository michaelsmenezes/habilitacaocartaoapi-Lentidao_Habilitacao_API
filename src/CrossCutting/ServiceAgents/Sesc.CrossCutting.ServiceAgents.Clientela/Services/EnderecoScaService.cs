using Newtonsoft.Json;
using Sesc.CrossCutting.ServiceAgents.Clientela.Contracts;
using Sesc.CrossCutting.ServiceAgents.Clientela.Dto;
using Sesc.CrossCutting.ServiceAgents.Clientela.Services.Contracts;

namespace Sesc.CrossCutting.ServiceAgents.Clientela.Services
{
    public class EnderecoScaService : IEnderecoScaService
    {
        protected readonly ISescApiCentralAtendimento _sescApiCentralAtendimento;

        public EnderecoScaService(
            ISescApiCentralAtendimento sescApiCentralAtendimento
        ) {
            _sescApiCentralAtendimento = sescApiCentralAtendimento;
        }

        public EnderecoScaDto GetEndereco(string cpf)
        {
            var api = _sescApiCentralAtendimento.GetHttpClient();

            var response = api.GetAsync(string.Format(_sescApiCentralAtendimento.GetUrlBase() + "Clientela/GetEndereco/{0}", cpf)).Result;

            if (!response.IsSuccessStatusCode) return null;

            var responseCliente = JsonConvert.DeserializeObject<EnderecoScaDto>(response.Content.ReadAsStringAsync().Result);

            if (responseCliente == null) return null;

            return responseCliente;
        }
    }
}
