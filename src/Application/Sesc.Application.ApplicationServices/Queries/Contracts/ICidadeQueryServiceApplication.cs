using Sesc.Domain.Habilitacao.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sesc.Application.ApplicationServices.Queries.Contracts
{
    public interface ICidadeQueryServiceApplication : IQueryServiceApplication
    {
        IList<CidadeViewModel> GetCidadesByUf(string uf);
        IList<CidadeViewModel> GetListaMunicipiosResponsaveis();
    }
}
