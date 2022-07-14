using Sesc.Domain.Habilitacao.Dto;
using Sesc.Domain.Habilitacao.Entities;
using Sesc.Domain.Habilitacao.ViewModel;
using Sesc.MeuSesc.SharedKernel.Application.Services;
using System.Threading.Tasks;

namespace Sesc.Domain.Habilitacao.Services.Contracts
{
    public interface IDocumentoService : IService<Documento>
    {
        Task<Documento> GetById(int id);
        Task<DocumentoDto> Alterar(Documento documento);
        Task Incluir(Pessoa pessoa, UploadDocumentoViewModel documentoViewModel);
        Task Deletar(Documento documento);
    }
}
