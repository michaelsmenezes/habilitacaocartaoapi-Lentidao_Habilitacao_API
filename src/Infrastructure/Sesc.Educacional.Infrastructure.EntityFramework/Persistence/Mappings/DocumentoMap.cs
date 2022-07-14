using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sesc.Domain.Habilitacao.Entities;
using Sesc.MeuSesc.Infrastructure.EntityFramework.Extensions;

namespace Sesc.MeuSesc.Infrastructure.EntityFramework.Persistence.Mappings
{
    public class DocumentoMap : EntityTypeConfiguration<Documento>
    {
        public override void Map(EntityTypeBuilder<Documento> builder)
        {
            builder.ToTable("DOCUMENTO");
            builder.HasKey(p => p.Id);
            builder.Property(t => t.Id).HasColumnName("ID").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();

            builder.Property(t => t.PessoaId).HasColumnName("PESSOA_ID").HasColumnType("int").IsRequired();
            builder.HasOne(p => p.Pessoa)
                    .WithMany(d => d.Documentos)
                    .HasForeignKey(p => p.PessoaId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.Property(t => t.Tipo).HasColumnName("TIPO").HasColumnType("int").IsRequired();
            builder.Property(t => t.Extensao).HasColumnName("EXTENSAO").HasColumnType("varchar(10)");
            builder.Property(t => t.Url).HasColumnName("URL").HasColumnType("varchar(255)");
            builder.Property(t => t.MimeType).HasColumnName("MIME_TYPE").HasColumnType("varchar(80)");
            builder.Property(t => t.Nome).HasColumnName("NOME").HasColumnType("varchar(250)");
            builder.Property(t => t.DataRegistro).HasColumnName("DATA_REGISTRO").HasColumnType("datetime").HasDefaultValueSql("getdate()").IsRequired();
            builder.Property(t => t.Situacao).HasColumnName("SITUACAO").HasColumnType("int").IsRequired();

        }
    }
}
