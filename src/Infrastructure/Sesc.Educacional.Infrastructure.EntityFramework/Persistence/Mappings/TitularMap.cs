using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sesc.Domain.Habilitacao.Entities;
using Sesc.MeuSesc.Infrastructure.EntityFramework.Extensions;

namespace Sesc.MeuSesc.Infrastructure.EntityFramework.Persistence.Mappings
{
    public class TitularMap : EntityTypeConfiguration<Titular>
    {
        public override void Map(EntityTypeBuilder<Titular> builder)
        {
            builder.ToTable("TITULAR");

            builder.Property(s => s.ResponsavelId).HasColumnName("RESPONSAVEL_ID").HasColumnType("int");
            builder.HasOne(a => a.Responsavel)
                    .WithMany()
                    .HasForeignKey(p => p.ResponsavelId)
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
