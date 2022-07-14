using Newtonsoft.Json;
using Sesc.CrossCutting.ServiceAgents.Clientela.Contracts;
using Sesc.CrossCutting.ServiceAgents.Clientela.Dto;
using Sesc.CrossCutting.ServiceAgents.Clientela.Enums;
using Sesc.CrossCutting.ServiceAgents.Clientela.Services.Contracts;
using System;
using System.Collections.Generic;

namespace Sesc.CrossCutting.ServiceAgents.Clientela.Services
{
    public class PessoaScaService : IPessoaScaService
    {
        protected readonly ISescApiCentralAtendimento _sescApiCentralAtendimento;

        public PessoaScaService(
            ISescApiCentralAtendimento sescApiCentralAtendimento
        ) {
            _sescApiCentralAtendimento = sescApiCentralAtendimento;
        }

        public PessoaScaDto GetPessoa(string cpf)
        {
            var api = _sescApiCentralAtendimento.GetHttpClient();

            var response = api.GetAsync(string.Format(_sescApiCentralAtendimento.GetUrlBase() + "Clientela/GetInfoClienteAsync/{0}", cpf)).Result;

            if (!response.IsSuccessStatusCode) return null;

            var responseCliente = JsonConvert.DeserializeObject<PessoaScaDto>(response.Content.ReadAsStringAsync().Result);

            if (responseCliente == null) return null;

            return responseCliente;
        }

        public IList<PessoaScaDto> GetGrupoFamiliar(string cpf)
        {
            var api = _sescApiCentralAtendimento.GetHttpClient();

            var response = api.GetAsync(string.Format(_sescApiCentralAtendimento.GetUrlBase() + "Clientela/grupofamiliartitular/{0}", cpf)).Result;

            if (!response.IsSuccessStatusCode) return null;

            var responseCliente = JsonConvert.DeserializeObject<List<PessoaScaDto>>(response.Content.ReadAsStringAsync().Result);

            if (responseCliente == null) return null;

            responseCliente.RemoveAll(x => x.nucpf == cpf);

            return responseCliente;
        }
    }
}
