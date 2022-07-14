using Sesc.Domain.Habilitacao.Entities;
using Sesc.MeuSesc.SharedKernel.Infrastructure.Repositories;

namespace Sesc.Domain.Habilitacao.Repositories.Contracts
{
    public interface INotificacaoTemplateRepository : IRepository<NotificacaoTemplate>
    {
        NotificacaoTemplate GetNotificacaoTemplateByIdentificador(string identificador);
    }
}
