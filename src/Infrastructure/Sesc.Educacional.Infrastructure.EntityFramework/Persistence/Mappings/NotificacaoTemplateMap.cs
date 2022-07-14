using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sesc.Domain.Habilitacao.Entities;
using Sesc.MeuSesc.Infrastructure.EntityFramework.Extensions;

namespace Sesc.MeuSesc.Infrastructure.EntityFramework.Persistence.Mappings
{
    public class NotificacaoTemplateMap : EntityTypeConfiguration<NotificacaoTemplate>
    {
        public override void Map(EntityTypeBuilder<NotificacaoTemplate> builder)
        {
            builder.ToTable("NOTIFICACAO_TEMPLATE");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).HasColumnName("ID").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();

            builder.Property(a => a.AssuntoModelo).HasColumnName("ASSUNTO_MODELO").HasColumnType("varchar(80)").IsRequired();
            builder.Property(a => a.Identificador).HasColumnName("IDENTIFICADOR").HasColumnType("varchar(42)").IsRequired();
            builder.Property(a => a.TextoModelo).HasColumnName("TEXTO_MODELO").HasColumnType("varchar(max)").IsRequired();
        }
    }
}
