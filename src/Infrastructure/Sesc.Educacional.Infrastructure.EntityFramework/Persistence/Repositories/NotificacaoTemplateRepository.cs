using Sesc.Domain.Habilitacao.Entities;
using Sesc.Domain.Habilitacao.Repositories.Contracts;
using Sesc.MeuSesc.Infrastructure.EntityFramework.Base;
using Sesc.MeuSesc.Infrastructure.EntityFramework.Context;
using Sesc.MeuSesc.SharedKernel.Infrastructure.DataContext;
using System.Linq;

namespace Sesc.MeuSesc.Infrastructure.EntityFramework.Persistence.Repositories
{
    public class NotificacaoTemplateRepository : Repository<NotificacaoTemplate>, INotificacaoTemplateRepository
    {
        public NotificacaoTemplateRepository(IDataContext context) : base(context)
        {
        }

        public NotificacaoTemplate GetNotificacaoTemplateByIdentificador(string identificador)
        {
            return ((SescContext)_context).NotificacaoTemplate.Where(n => n.Identificador == identificador).FirstOrDefault();
        }
    }
}
