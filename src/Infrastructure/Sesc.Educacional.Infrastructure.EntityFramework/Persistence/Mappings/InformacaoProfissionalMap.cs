using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sesc.Domain.Habilitacao.Entities;
using Sesc.MeuSesc.Infrastructure.EntityFramework.Extensions;

namespace Sesc.MeuSesc.Infrastructure.EntityFramework.Persistence.Mappings
{
    public class InformacaoProfissionalMap : EntityTypeConfiguration<InformacaoProfissional>
    {
        public override void Map(EntityTypeBuilder<InformacaoProfissional> builder)
        {
            builder.ToTable("INFORMACAO_PROFISSIONAL");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("ID").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();

            builder.Property(e => e.CNPJ).HasColumnName("CNPJ").HasColumnType("varchar(14)");
            builder.Property(e => e.NomeEmpresa).HasColumnName("NOME_EMPRESA").HasColumnType("varchar(250)");
            builder.Property(s => s.DataAdmissao).HasColumnName("DATA_ADMISSAO").HasColumnType("datetime");
            builder.Property(s => s.DataDemissao).HasColumnName("DATA_DEMISSAO").HasColumnType("datetime");
            builder.Property(s => s.Ocupacao).HasColumnName("OCUPACAO").HasColumnType("varchar(80)");
            builder.Property(s => s.NumeroCTPS).HasColumnName("NUMERO_CTPS").HasColumnType("varchar(80)");
            builder.Property(s => s.SerieCTPS).HasColumnName("SERIE_CTPS").HasColumnType("varchar(80)");
            builder.Property(s => s.Renda).HasColumnName("RENDA").HasColumnType("decimal(18,2)");
        }
    }
}
