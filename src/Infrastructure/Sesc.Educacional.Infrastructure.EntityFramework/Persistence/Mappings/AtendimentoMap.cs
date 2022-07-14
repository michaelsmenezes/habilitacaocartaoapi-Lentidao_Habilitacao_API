using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sesc.Domain.Habilitacao.Entities;
using Sesc.MeuSesc.Infrastructure.EntityFramework.Extensions;

namespace Sesc.MeuSesc.Infrastructure.EntityFramework.Persistence.Mappings
{
    public class AtendimentoMap : EntityTypeConfiguration<Atendimento>
    {
        public override void Map(EntityTypeBuilder<Atendimento> builder)
        {
            builder.ToTable("ATENDIMENTO");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).HasColumnName("ID").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();

            builder.Property(a => a.SolicitacaoId).HasColumnName("SOLICITACAO_ID").HasColumnType("int").IsRequired();
            builder.HasOne(a => a.Solicitacao)
                    .WithMany(a => a.Atendimentos)
                    .HasForeignKey(p => p.SolicitacaoId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.Property(a => a.Nome).HasColumnName("NOME").HasColumnType("varchar(250)").IsRequired();
            builder.Property(a => a.Usuario).HasColumnName("USUARIO").HasColumnType("varchar(250)").IsRequired();
            builder.Property(a => a.SituacaoSolicitacao).HasColumnName("SITUACAO_SOLICITACAO").HasColumnType("int").IsRequired();
            builder.Property(a => a.Observacao).HasColumnName("OBSERVACAO").HasColumnType("varchar(max)");
            builder.Property(a => a.DataHoraInicio).HasColumnName("DATA_HORA_INICIO").HasColumnType("datetime").IsRequired().HasDefaultValueSql("getdate()");
            builder.Property(a => a.DataHoraFim).HasColumnName("DATA_HORA_FIM").HasColumnType("datetime");
        }
    }
}
