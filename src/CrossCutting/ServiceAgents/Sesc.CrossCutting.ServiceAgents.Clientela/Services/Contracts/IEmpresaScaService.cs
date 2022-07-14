using Sesc.CrossCutting.ServiceAgents.Clientela.Dto;
using System.Collections.Generic;

namespace Sesc.CrossCutting.ServiceAgents.Clientela.Services.Contracts
{
    public interface IEmpresaScaService
    {
        EmpresaScaDto GetEmpresa(string cnpj);
    }
}