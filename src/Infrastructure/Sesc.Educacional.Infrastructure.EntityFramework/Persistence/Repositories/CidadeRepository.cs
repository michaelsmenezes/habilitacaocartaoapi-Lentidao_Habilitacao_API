using Sesc.Domain.Habilitacao.Entities;
using Sesc.Domain.Habilitacao.Repositories.Contracts;
using Sesc.MeuSesc.Infrastructure.EntityFramework.Base;
using Sesc.MeuSesc.Infrastructure.EntityFramework.Context;
using Sesc.MeuSesc.SharedKernel.Infrastructure.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sesc.MeuSesc.Infrastructure.EntityFramework.Persistence.Repositories
{
    public class CidadeRepository : Repository<Cidade>, ICidadeRepository
    {
        public CidadeRepository(IDataContext context, IServiceProvider provider) : base(context)
        {
            _context = context;
        }

        public IList<Cidade> GetListaMunicipiosResponsaveis()
        {
            SescContext context = (SescContext)_context;

            var subquery = (
                from cidadeResp in context.Cidade
                where cidadeResp.CidadeResponsavelId != null
                select cidadeResp.CidadeResponsavelId
            ).ToList();

            var query = (
                from cidade in context.Cidade
                where subquery.Contains(cidade.Id)
                select cidade
            );
            
            return query.ToList();
        }
    }
}
