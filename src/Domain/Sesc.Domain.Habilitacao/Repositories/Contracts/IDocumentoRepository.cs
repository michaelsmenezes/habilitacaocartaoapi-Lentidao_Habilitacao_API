using Sesc.Domain.Habilitacao.Entities;
using Sesc.MeuSesc.SharedKernel.Infrastructure.Repositories;

namespace Sesc.Domain.Habilitacao.Repositories.Contracts
{
    public interface IDocumentoRepository : IRepository<Documento>
    {
        Documento GetById(int id);
        Documento Alterar(Documento Documento);
        Documento Incluir(Documento Documento);
        void Deletar(Documento Documento);
    }
}
