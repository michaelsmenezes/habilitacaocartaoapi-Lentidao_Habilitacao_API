using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sesc.Domain.Habilitacao.Entities;
using Sesc.MeuSesc.Infrastructure.EntityFramework.Extensions;

namespace Sesc.MeuSesc.Infrastructure.EntityFramework.Persistence.Mappings
{
    public class ResponsavelMap : EntityTypeConfiguration<Responsavel>
    {
        public override void Map(EntityTypeBuilder<Responsavel> builder)
        {
            builder.ToTable("RESPONSAVEL");

            builder.Property(s => s.Parentesco).HasColumnName("PARENTESCO").HasColumnType("int");
        }
    }
}
