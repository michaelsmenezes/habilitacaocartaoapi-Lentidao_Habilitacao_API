using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sesc.Domain.Habilitacao.Entities;
using Sesc.MeuSesc.Infrastructure.EntityFramework.Extensions;

namespace Sesc.MeuSesc.Infrastructure.EntityFramework.Persistence.Mappings
{
    public class ContatoMap : EntityTypeConfiguration<Contato>
    {
        public override void Map(EntityTypeBuilder<Contato> builder)
        {
            builder.ToTable("CONTATO");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).HasColumnName("ID").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();

            builder.Property(s => s.Logradouro).HasColumnName("LOGRADOURO").HasColumnType("varchar(250)");
            builder.Property(s => s.Complemento).HasColumnName("COMPLEMENTO").HasColumnType("varchar(250)");
            builder.Property(s => s.Numero).HasColumnName("NUMERO").HasColumnType("varchar(10)");
            builder.Property(s => s.Cep).HasColumnName("CEP").HasColumnType("varchar(10)");
            builder.Property(s => s.Bairro).HasColumnName("BAIRRO").HasColumnType("varchar(250)");

            builder.Property(s => s.CidadeId).HasColumnName("CIDADE_ID").HasColumnType("int");
            builder.HasOne(s => s.Cidade)
                    .WithMany()
                    .HasForeignKey(s => s.CidadeId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.Property(s => s.TelefonePrincipal).HasColumnName("TELEFONE_PRINCIPAL").HasColumnType("varchar(80)");
            builder.Property(s => s.TelefoneSecundario).HasColumnName("TELEFONE_SECUNDARIO").HasColumnType("varchar(80)");

        }
    }
}
