using Newtonsoft.Json;
using Sesc.CrossCutting.ServiceAgents.Clientela.Contracts;
using Sesc.CrossCutting.ServiceAgents.Clientela.Dto;
using Sesc.CrossCutting.ServiceAgents.Clientela.Enums;
using Sesc.CrossCutting.ServiceAgents.Clientela.Services.Contracts;
using System;
using System.Collections.Generic;

namespace Sesc.CrossCutting.ServiceAgents.Clientela.Services
{
    public class EmpresaScaService : IEmpresaScaService
    {
        protected readonly ISescApiCentralAtendimento _sescApiCentralAtendimento;

        public EmpresaScaService(
            ISescApiCentralAtendimento sescApiCentralAtendimento
        ) {
            _sescApiCentralAtendimento = sescApiCentralAtendimento;
        }

        public EmpresaScaDto GetEmpresa(string cnpj)
        {
            var api = _sescApiCentralAtendimento.GetHttpClient();

            var response = api.GetAsync(string.Format(
                _sescApiCentralAtendimento.GetUrlBase() + "Clientela/GetEmpresa/{0}"
                , cnpj
            )).Result;

            if (!response.IsSuccessStatusCode) return null;

            var responseClient = JsonConvert.DeserializeObject<EmpresaScaDto>(
                response.Content.ReadAsStringAsync().Result
            );

            if (responseClient == null) return null;

            return responseClient;
        }
    }
}
