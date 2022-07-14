using Sesc.MeuSesc.SharedKernel.Infrastructure.Entities;
using System;

namespace Sesc.Domain.Habilitacao.Entities
{
    public class Estado : EntityBase
    {
        public string Uf { get; set; }
        public string Descricao { get; set; }
    }
}
