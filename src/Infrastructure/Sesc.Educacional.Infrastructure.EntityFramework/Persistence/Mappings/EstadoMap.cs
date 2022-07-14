using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sesc.Domain.Habilitacao.Entities;
using Sesc.MeuSesc.Infrastructure.EntityFramework.Extensions;

namespace Sesc.MeuSesc.Infrastructure.EntityFramework.Persistence.Mappings
{
    public class EstadoMap : EntityTypeConfiguration<Estado>
    {
        public override void Map(EntityTypeBuilder<Estado> builder)
        {
            builder.ToTable("ESTADO");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("ID").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();

            builder.Property(e => e.Uf).HasColumnName("UF").HasColumnType("varchar(2)").IsRequired();
            builder.Property(e => e.Descricao).HasColumnName("DESCRICAO").HasColumnType("varchar(150)");
        }
    }
}
