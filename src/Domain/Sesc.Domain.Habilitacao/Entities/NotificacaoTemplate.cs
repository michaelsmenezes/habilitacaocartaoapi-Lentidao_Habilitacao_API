using Sesc.MeuSesc.SharedKernel.Infrastructure.Entities;

namespace Sesc.Domain.Habilitacao.Entities
{
    public class NotificacaoTemplate : EntityBase
    {
        public string AssuntoModelo { get; set; }
        public string Identificador { get; set; }
        public string TextoModelo { get; set; }
    }
}
