using Newtonsoft.Json.Linq;
using Sesc.Domain.Habilitacao.Dto;
using Sesc.Domain.Habilitacao.Services.Contracts;
using System;
using System.Collections.Generic;
using Sesc.CrossCutting.Validation.CommonValidation;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Sesc.CrossCutting.ServiceAgents.AuthServer.Services.Contracts;
using Sesc.CrossCutting.ServiceAgents.Clientela.Contracts;
using System.Linq;

namespace Sesc.Domain.Habilitacao.Services
{
    public class PessoaService : IPessoaService
    {
        protected readonly IUserAuthenticatedAuthService _userAuthenticatedAuthService;
        protected readonly ISescApiCentralAtendimento _sescApiCentralAtendimento;

        public PessoaService(
            IUserAuthenticatedAuthService userAuthenticatedAuthService,
            //IPessoaRepository repository,
            ISescApiCentralAtendimento sescApiCentralAtendimento
        ) {
            _userAuthenticatedAuthService = userAuthenticatedAuthService;
            _sescApiCentralAtendimento = sescApiCentralAtendimento;
        }

        public IList<ClienteScaDto> GetGrupoFamiliar()
        {
            string cpf = _userAuthenticatedAuthService.GetUserAuthenticated().CpfCnpj;

            var api = _sescApiCentralAtendimento.GetHttpClient();

            var response = api.GetAsync(string.Format(_sescApiCentralAtendimento.GetUrlBase() + "Clientela/grupofamiliar/{0}", cpf)).Result;

            if (!response.IsSuccessStatusCode) return new List<ClienteScaDto>();

            var responseClientes = (JArray)JsonConvert.DeserializeObject<object>(response.Content.ReadAsStringAsync().Result);

            if (responseClientes == null) return null;

            var grupoFamiliar = new List<ClienteScaDto>();
            foreach (var responseCliente in responseClientes)
            {
                // Nao incluir duplicadas (mesmo cpf)
                if (grupoFamiliar.Where(x => x.Cpf.Equals(responseCliente?["nucpf"].ToString().Trim())).FirstOrDefault() == null)
                {
                    grupoFamiliar.Add(MapResponse2Dto((JObject)responseCliente));
                }
            }

            return grupoFamiliar;
        }

        public async Task<ClienteScaDto> GetByCpfAsync(string cpf)
        {
            if (String.IsNullOrEmpty(cpf) || !CpfValidation.IsCpf(cpf))
            {
                return null;
            }

            var api = _sescApiCentralAtendimento.GetHttpClient();

            var response = await api.GetAsync(string.Format(_sescApiCentralAtendimento.GetUrlBase() + "Clientela/GetInfoClienteAsync/{0}", cpf));

            if (!response.IsSuccessStatusCode) return null;

            var responseCliente = (JObject)JsonConvert.DeserializeObject<object>(await response.Content.ReadAsStringAsync());

            if (responseCliente == null) return null;

            return this.MapResponse2Dto(responseCliente);
        }

        public async Task<string> GetFotoByCpf(string cpf)
        {
            if (String.IsNullOrEmpty(cpf) || !CpfValidation.IsCpf(cpf))
            {
                return null;
            }

            var api = _sescApiCentralAtendimento.GetHttpClient();

            var response = await api.GetAsync(string.Format(_sescApiCentralAtendimento.GetUrlBase() + "Clientela/GetFoto/{0}", cpf));

            if (!response.IsSuccessStatusCode) return null;

            var responseCliente = (string)JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());

            if (responseCliente == null) return null;

            return responseCliente;
        }

        private ClienteScaDto MapResponse2Dto(JObject responseCliente)
        {
            return new ClienteScaDto
            {
                Nome = responseCliente?["nmcliente"].ToString().Trim(),
                Email = responseCliente?["email"].ToString().Trim(),
                Telefone = responseCliente?["telefone"].ToString().Trim(),
                Celular = responseCliente?["celular"].ToString().Trim(),
                Cpf = responseCliente?["nucpf"].ToString().Trim(),
                CodSexo = responseCliente?["cdsexo"].ToString().Trim() == "0" ? "M" : "F",
                DataNascimento = responseCliente?["dtnascimen"].ToString().Trim()
            };
        }

        public bool SalvarPessoa(ClienteScaDto pessoa)
        {
            //var pessoaScaDto = new ClienteScaDto
            //{
            //    Id = pessoa.Id,
            //    Telefone = pessoa.Telefone,
            //    Cpf = pessoa.Cpf,
            //    DataNascimento = pessoa.DataNascimento.ToString("Y/m/d"),
            //    Email = pessoa.Email,
            //    Nome = pessoa.Nome,
            //    CodSexo = pessoa.CodSexo
            //};

            return true;
        }
    }
}
