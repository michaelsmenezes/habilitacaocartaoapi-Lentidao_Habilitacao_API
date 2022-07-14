using Microsoft.AspNetCore.Http;
using Sesc.Domain.Habilitacao.ViewModel;

namespace Sesc.Application.ApplicationServices.Commands.Contracts
{
    public interface IDocumentoCommandServiceApplication
    {
        bool AlteraSituacao(DocumentoViewModel documentoViewModel);
        void SalvarArquivoUsuarioLogado(UploadDocumentoViewModel documento);        
    }
}
