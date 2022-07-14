using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sesc.Domain.Habilitacao.Entities;
using Sesc.MeuSesc.Infrastructure.EntityFramework.Extensions;

namespace Sesc.MeuSesc.Infrastructure.EntityFramework.Persistence.Mappings
{
    public class DependenteMap : EntityTypeConfiguration<Dependente>
    {
        public override void Map(EntityTypeBuilder<Dependente> builder)
        {
            builder.ToTable("DEPENDENTE");

            builder.Property(s => s.Parentesco).HasColumnName("PARENTESCO").HasColumnType("int");

            builder.Property(s => s.Acao).HasColumnName("ACAO").HasColumnType("int");

            //builder.Property(s => s.Renovar).HasColumnName("RENOVAR").HasColumnType("int");

            builder.Property(sp => sp.TitularId).HasColumnName("TITULAR_ID").HasColumnType("int");
            builder.HasOne(sp => sp.Titular)
                    .WithMany(x => x.Dependentes)
                    .HasForeignKey(s => s.TitularId)
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
