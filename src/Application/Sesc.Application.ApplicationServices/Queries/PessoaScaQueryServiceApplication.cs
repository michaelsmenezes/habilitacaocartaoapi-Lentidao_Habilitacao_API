using Sesc.Application.ApplicationServices.Queries.Contracts;
using Sesc.CrossCutting.ServiceAgents.Clientela.Services.Contracts;
using Sesc.CrossCutting.ServiceAgents.Clientela.Dto;
using System;
using Sesc.CrossCutting.Validation.BaseException;
using Sesc.CrossCutting.ServiceAgents.AuthServer.Services.Contracts;
using System.Collections.Generic;
using System.Linq;
using Sesc.CrossCutting.ServiceAgents.AntiCorruption.Sca2Habilitacao;

namespace Sesc.Application.ApplicationServices.Queries
{
    public class PessoaScaQueryServiceApplication : IPessoaScaQueryServiceApplication
    {
        protected readonly IPessoaScaService _pessoaScaService;
        private readonly IUserAuthenticatedAuthService _userAuthenticatedAuthService;

        public PessoaScaQueryServiceApplication(
            IUserAuthenticatedAuthService userAuthenticatedAuthService,
            IPessoaScaService pessoaScaService
        ) {
            _pessoaScaService = pessoaScaService;
            _userAuthenticatedAuthService = userAuthenticatedAuthService;
        }

        public PessoaScaDto GetByCpf(string cpf)
        {
            try
            {
                Int64.TryParse(cpf, out Int64 t);
            } catch (Exception e)
            {
                ContentSingleton.AddMessage("CPF inválido!");
                ContentSingleton.Dispatch();
            }

            var pessoaSca = _pessoaScaService.GetPessoa(cpf);

            if (pessoaSca == null)
            {
                ContentSingleton.AddMessage("CPF não encontrado na base.");
                ContentSingleton.Dispatch();
            }

            return pessoaSca;
        }

        public IList<PessoaScaDto> GetDependentesUsuarioLogado()
        {
            var cpf = _userAuthenticatedAuthService.GetUserAuthenticated().CpfCnpj;
            var pessoaSca = _pessoaScaService.GetGrupoFamiliar(cpf);

            if (pessoaSca == null)
            {
                ContentSingleton.AddMessage("CPF não encontrado na base.");
                ContentSingleton.Dispatch();
            }

            pessoaSca.ToList().ForEach(sca =>
            {
                sca.dsparentsc = PessoaAntiCorruption.ParentescoFromSca2Habilitacao(
                    int.Parse(sca.dsparentsc.Trim())
                ).ToString();
            });

            return pessoaSca;
        }
    }
}
