using Microsoft.EntityFrameworkCore;
using Sesc.Domain.Habilitacao.Entities;
using Sesc.Domain.Habilitacao.Repositories.Contracts;
using Sesc.MeuSesc.Infrastructure.EntityFramework.Base;
using Sesc.MeuSesc.Infrastructure.EntityFramework.Context;
using Sesc.MeuSesc.SharedKernel.Infrastructure.DataContext;
using System;
using System.Linq;

namespace Sesc.MeuSesc.Infrastructure.EntityFramework.Persistence.Repositories
{
    public class DocumentoRepository : Repository<Documento>, IDocumentoRepository
    {
        public DocumentoRepository(
            IDataContext context,
            IServiceProvider provider
        ) : base(context)
        {
            _context = context;
        }

        public Documento GetById(int id)
        {

            var Documento = _dbSet.Select(x => x)
                .Include(p => p.Pessoa)
                .Where(s => s.Id == id)
                .FirstOrDefault();

            return Documento;
        }

        public Documento Alterar(Documento Documento)
        {
            SescContext context = (SescContext)_context;

            context.Update(Documento);
            context.SaveChanges();

            return Documento;
        }

        public Documento Incluir(Documento Documento)
        {
            SescContext context = (SescContext)_context;

            context.Update(Documento);
            context.SaveChanges();

            return Documento;
        }

        public void Deletar(Documento Documento)
        {
            SescContext context = (SescContext)_context;

            context.Remove(Documento);
            context.SaveChanges();
        }
    }
}
