using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Org.BouncyCastle.Crypto.Tls;
using Sesc.Domain.Habilitacao.Entities;
using Sesc.MeuSesc.Infrastructure.EntityFramework.Extensions;

namespace Sesc.MeuSesc.Infrastructure.EntityFramework.Persistence.Mappings
{
    public class CidadeMap : EntityTypeConfiguration<Cidade>
    {
        public override void Map(EntityTypeBuilder<Cidade> builder)
        {
            builder.ToTable("CIDADE");
            
            builder.HasKey(p => p.Id);
            builder.Property(t => t.Id).HasColumnName("ID").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();

            builder.Property(t => t.CodigoIBGE).HasColumnName("CODIGO_IBGE").HasColumnType("varchar(20)");
            builder.Property(t => t.Descricao).HasColumnName("DESCRICAO").HasColumnType("varchar(150)");

            builder.Property(t => t.EstadoId).HasColumnName("ESTADO_ID").HasColumnType("int").IsRequired();
            builder.HasOne(p => p.Estado)
                    .WithMany()
                    .HasForeignKey(p => p.EstadoId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.Property(c => c.CidadeResponsavelId).HasColumnName("CIDADE_RESPONSAVEL_ID").HasColumnType("int");
            builder.HasOne(cr => cr.CidadeResponsavel)
                .WithMany()
                .HasForeignKey(cr => cr.CidadeResponsavelId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
