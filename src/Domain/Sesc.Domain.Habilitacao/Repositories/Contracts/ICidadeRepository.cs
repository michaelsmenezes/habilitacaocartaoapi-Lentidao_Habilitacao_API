using Sesc.Domain.Habilitacao.Entities;
using Sesc.MeuSesc.SharedKernel.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sesc.Domain.Habilitacao.Repositories.Contracts
{
    public interface ICidadeRepository : IRepository<Cidade>
    {
        IList<Cidade> GetListaMunicipiosResponsaveis();
    }
}
