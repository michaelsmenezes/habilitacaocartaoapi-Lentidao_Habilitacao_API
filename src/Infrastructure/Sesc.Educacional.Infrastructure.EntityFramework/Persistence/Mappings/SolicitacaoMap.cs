using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sesc.Domain.Habilitacao.Entities;
using Sesc.Domain.Habilitacao.Enum;
using Sesc.MeuSesc.Infrastructure.EntityFramework.Extensions;

namespace Sesc.MeuSesc.Infrastructure.EntityFramework.Persistence.Mappings
{
    public class SolicitacaoMap : EntityTypeConfiguration<Solicitacao>
    {
        public override void Map(EntityTypeBuilder<Solicitacao> builder)
        {
            builder.ToTable("SOLICITACAO");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).HasColumnName("ID").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();

            builder.Property(s => s.TitularId).HasColumnName("TITULAR_ID").HasColumnType("int").IsRequired();
            builder.HasOne(a => a.Titular)
                    .WithMany()
                    .HasForeignKey(p => p.TitularId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.Property(s => s.Situacao).HasColumnName("SITUACAO").HasColumnType("int").IsRequired();
            builder.Property(s => s.Cpf).HasColumnName("CPF").HasColumnType("varchar(11)").IsRequired();
            builder.Property(s => s.Tipo).HasColumnName("TIPO").HasColumnType("int").IsRequired();
            builder.Property(s => s.Categoria).HasColumnName("CATEGORIA").HasColumnType("int").IsRequired();
            builder.Property(s => s.DataRegistro).HasColumnName("DATA_REGISTRO").HasColumnType("datetime").HasDefaultValueSql("getdate()").IsRequired();
            builder.Property(s => s.DataEnvio).HasColumnName("DATA_ENVIO").HasColumnType("datetime").IsRequired(false);
            builder.Property(s => s.EmAtendimento).HasColumnName("EM_ATENDIMENTO").HasColumnType("bit").IsRequired().HasDefaultValue(0);
            builder.Property(s => s.Plataforma).HasColumnName("PLATAFORMA").HasColumnType("int").HasDefaultValue(SolicitacaoPlataformaEnum.NaoClassificado);
        }
    }
}
