using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sesc.Domain.Habilitacao.Entities;
using Sesc.MeuSesc.Infrastructure.EntityFramework.Extensions;

namespace Sesc.MeuSesc.Infrastructure.EntityFramework.Persistence.Mappings
{
    public class PessoaMap : EntityTypeConfiguration<Pessoa>
    {
        public override void Map(EntityTypeBuilder<Pessoa> builder)
        {
            builder.ToTable("PESSOA");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).HasColumnName("ID").HasColumnType("int").IsRequired().UseSqlServerIdentityColumn();
            
            builder.Property(s => s.Nome).HasColumnName("NOME").HasColumnType("varchar(250)").IsRequired();
            builder.Property(s => s.Sexo).HasColumnName("SEXO").HasColumnType("char(1)").IsRequired();
            builder.Property(s => s.NomeSocial).HasColumnName("NOME_SOCIAL").HasColumnType("varchar(250)");
            builder.Property(s => s.DataNascimento).HasColumnName("DATA_NASCIMENTO").HasColumnType("datetime").IsRequired();
            builder.Property(s => s.Cpf).HasColumnName("CPF").HasColumnType("varchar(11)").IsRequired();
            builder.Property(s => s.Email).HasColumnName("EMAIL").HasColumnType("varchar(250)");
            builder.Property(s => s.NomeMae).HasColumnName("NOME_MAE").HasColumnType("varchar(250)");
            builder.Property(s => s.NomePai).HasColumnName("NOME_PAI").HasColumnType("varchar(250)");

            builder.Property(s => s.ContatoId).HasColumnName("CONTATO_ID").HasColumnType("int");
            builder.HasOne(a => a.Contato)
                    .WithMany()
                    .HasForeignKey(p => p.ContatoId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.Property(s => s.InformacaoProfissionalId).HasColumnName("INFORMACAO_PROFISSIONAL_ID").HasColumnType("int");
            builder.HasOne(a => a.InformacaoProfissional)
                    .WithMany()
                    .HasForeignKey(p => p.InformacaoProfissionalId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.Property(s => s.EstadoCivil).HasColumnName("ESTADO_CIVIL").HasColumnType("int");
            builder.Property(s => s.Escolaridade).HasColumnName("ESCOLARIDADE").HasColumnType("int");
            builder.Property(s => s.UltimaSerie).HasColumnName("ULTIMA_SERIE").HasColumnType("varchar(80)");
            builder.Property(s => s.Nacionalidade).HasColumnName("NACIONALIDADE").HasColumnType("varchar(80)").IsRequired();
            builder.Property(s => s.Naturalidade).HasColumnName("NATURALIDADE").HasColumnType("varchar(80)").IsRequired();
            builder.Property(s => s.TipoDocumento).HasColumnName("TIPO_DOCUMENTO").HasColumnType("int");
            builder.Property(s => s.Numero).HasColumnName("NUMERO").HasColumnType("varchar(30)");
            builder.Property(s => s.OrgaoEmissor).HasColumnName("ORGAO_EMISSOR").HasColumnType("varchar(80)");
            builder.Property(s => s.DataEmissao).HasColumnName("DATA_EMISSAO").HasColumnType("datetime");
            builder.Property(s => s.DataVencimento).HasColumnName("DATA_VENCIMENTO").HasColumnType("datetime");

            builder.HasDiscriminator<string>("TIPO")
                    .HasValue<Titular>("TITULAR")
                    .HasValue<Responsavel>("RESPONSAVEL")
                    .HasValue<Dependente>("DEPENDENTE");
        }
    }
}
