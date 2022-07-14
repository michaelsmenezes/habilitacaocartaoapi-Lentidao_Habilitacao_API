using Sesc.Domain.Habilitacao.ViewModel;
using System.Collections.Generic;

namespace Sesc.Application.ApplicationServices.Queries.Contracts
{
    public interface IEstadoQueryServiceApplication : IQueryServiceApplication
    {
        IList<EstadoViewModel> GetEstados();
    }
}
