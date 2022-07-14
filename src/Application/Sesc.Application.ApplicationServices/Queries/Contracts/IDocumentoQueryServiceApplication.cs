using Sesc.Domain.Habilitacao.ViewModel;
using System.IO;
using System.Threading.Tasks;

namespace Sesc.Application.ApplicationServices.Queries.Contracts
{
    public interface IDocumentoQueryServiceApplication : IQueryServiceApplication
    {
        Task<DocumentoDownloadViewModel> Download(int Id);
    }
}