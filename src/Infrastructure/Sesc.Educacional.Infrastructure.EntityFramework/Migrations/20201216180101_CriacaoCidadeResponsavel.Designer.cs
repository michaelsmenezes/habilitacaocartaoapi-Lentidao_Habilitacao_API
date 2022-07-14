﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sesc.MeuSesc.Infrastructure.EntityFramework.Context;

namespace Sesc.MeuSesc.Infrastructure.EntityFramework.Migrations
{
    [DbContext(typeof(SescContext))]
    [Migration("20201216180101_CriacaoCidadeResponsavel")]
    partial class CriacaoCidadeResponsavel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Sesc.Domain.Habilitacao.Entities.Atendimento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DataHoraFim")
                        .HasColumnName("DATA_HORA_FIM")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DataHoraInicio")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("DATA_HORA_INICIO")
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnName("NOME")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Observacao")
                        .HasColumnName("OBSERVACAO")
                        .HasColumnType("varchar(max)");

                    b.Property<int>("SituacaoSolicitacao")
                        .HasColumnName("SITUACAO_SOLICITACAO")
                        .HasColumnType("int");

                    b.Property<int>("SolicitacaoId")
                        .HasColumnName("SOLICITACAO_ID")
                        .HasColumnType("int");

                    b.Property<string>("Usuario")
                        .IsRequired()
                        .HasColumnName("USUARIO")
                        .HasColumnType("varchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("SolicitacaoId");

                    b.ToTable("ATENDIMENTO");
                });

            modelBuilder.Entity("Sesc.Domain.Habilitacao.Entities.Cidade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CidadeResponsavelId")
                        .HasColumnName("CIDADE_RESPONSAVEL_ID")
                        .HasColumnType("int");

                    b.Property<string>("CodigoIBGE")
                        .HasColumnName("CODIGO_IBGE")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Descricao")
                        .HasColumnName("DESCRICAO")
                        .HasColumnType("varchar(150)");

                    b.Property<int>("EstadoId")
                        .HasColumnName("ESTADO_ID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CidadeResponsavelId");

                    b.HasIndex("EstadoId");

                    b.ToTable("CIDADE");
                });

            modelBuilder.Entity("Sesc.Domain.Habilitacao.Entities.Contato", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Bairro")
                        .HasColumnName("BAIRRO")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Cep")
                        .HasColumnName("CEP")
                        .HasColumnType("varchar(10)");

                    b.Property<int?>("CidadeId")
                        .HasColumnName("CIDADE_ID")
                        .HasColumnType("int");

                    b.Property<string>("Complemento")
                        .HasColumnName("COMPLEMENTO")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Email");

                    b.Property<string>("Logradouro")
                        .HasColumnName("LOGRADOURO")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Numero")
                        .HasColumnName("NUMERO")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("TelefonePrincipal")
                        .HasColumnName("TELEFONE_PRINCIPAL")
                        .HasColumnType("varchar(80)");

                    b.Property<string>("TelefoneSecundario")
                        .HasColumnName("TELEFONE_SECUNDARIO")
                        .HasColumnType("varchar(80)");

                    b.HasKey("Id");

                    b.HasIndex("CidadeId");

                    b.ToTable("CONTATO");
                });

            modelBuilder.Entity("Sesc.Domain.Habilitacao.Entities.Documento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataRegistro")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("DATA_REGISTRO")
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Extensao")
                        .HasColumnName("EXTENSAO")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("MimeType")
                        .HasColumnName("MIME_TYPE")
                        .HasColumnType("varchar(80)");

                    b.Property<string>("Nome")
                        .HasColumnName("NOME")
                        .HasColumnType("varchar(250)");

                    b.Property<int>("PessoaId")
                        .HasColumnName("PESSOA_ID")
                        .HasColumnType("int");

                    b.Property<int>("Situacao")
                        .HasColumnName("SITUACAO")
                        .HasColumnType("int");

                    b.Property<int>("Tipo")
                        .HasColumnName("TIPO")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .HasColumnName("URL")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("PessoaId");

                    b.ToTable("DOCUMENTO");
                });

            modelBuilder.Entity("Sesc.Domain.Habilitacao.Entities.Estado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .HasColumnName("DESCRICAO")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Uf")
                        .IsRequired()
                        .HasColumnName("UF")
                        .HasColumnType("varchar(2)");

                    b.HasKey("Id");

                    b.ToTable("ESTADO");
                });

            modelBuilder.Entity("Sesc.Domain.Habilitacao.Entities.InformacaoProfissional", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CNPJ")
                        .HasColumnName("CNPJ")
                        .HasColumnType("varchar(14)");

                    b.Property<DateTime?>("DataAdmissao")
                        .HasColumnName("DATA_ADMISSAO")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DataDemissao")
                        .HasColumnName("DATA_DEMISSAO")
                        .HasColumnType("datetime");

                    b.Property<string>("NomeEmpresa")
                        .HasColumnName("NOME_EMPRESA")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("NumeroCTPS")
                        .HasColumnName("NUMERO_CTPS")
                        .HasColumnType("varchar(80)");

                    b.Property<string>("Ocupacao")
                        .HasColumnName("OCUPACAO")
                        .HasColumnType("varchar(80)");

                    b.Property<decimal>("Renda")
                        .HasColumnName("RENDA")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("SerieCTPS")
                        .HasColumnName("SERIE_CTPS")
                        .HasColumnType("varchar(80)");

                    b.HasKey("Id");

                    b.ToTable("INFORMACAO_PROFISSIONAL");
                });

            modelBuilder.Entity("Sesc.Domain.Habilitacao.Entities.NotificacaoTemplate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AssuntoModelo")
                        .IsRequired()
                        .HasColumnName("ASSUNTO_MODELO")
                        .HasColumnType("varchar(80)");

                    b.Property<string>("Identificador")
                        .IsRequired()
                        .HasColumnName("IDENTIFICADOR")
                        .HasColumnType("varchar(42)");

                    b.Property<string>("TextoModelo")
                        .IsRequired()
                        .HasColumnName("TEXTO_MODELO")
                        .HasColumnType("varchar(max)");

                    b.HasKey("Id");

                    b.ToTable("NOTIFICACAO_TEMPLATE");
                });

            modelBuilder.Entity("Sesc.Domain.Habilitacao.Entities.Pessoa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ContatoId")
                        .HasColumnName("CONTATO_ID")
                        .HasColumnType("int");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnName("CPF")
                        .HasColumnType("varchar(11)");

                    b.Property<DateTime?>("DataEmissao")
                        .HasColumnName("DATA_EMISSAO")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnName("DATA_NASCIMENTO")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DataVencimento")
                        .HasColumnName("DATA_VENCIMENTO")
                        .HasColumnType("datetime");

                    b.Property<string>("Email")
                        .HasColumnName("EMAIL")
                        .HasColumnType("varchar(250)");

                    b.Property<int>("Escolaridade")
                        .HasColumnName("ESCOLARIDADE")
                        .HasColumnType("int");

                    b.Property<int>("EstadoCivil")
                        .HasColumnName("ESTADO_CIVIL")
                        .HasColumnType("int");

                    b.Property<int?>("InformacaoProfissionalId")
                        .HasColumnName("INFORMACAO_PROFISSIONAL_ID")
                        .HasColumnType("int");

                    b.Property<string>("Nacionalidade")
                        .IsRequired()
                        .HasColumnName("NACIONALIDADE")
                        .HasColumnType("varchar(80)");

                    b.Property<string>("Naturalidade")
                        .IsRequired()
                        .HasColumnName("NATURALIDADE")
                        .HasColumnType("varchar(80)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnName("NOME")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("NomeMae")
                        .IsRequired()
                        .HasColumnName("NOME_MAE")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("NomePai")
                        .HasColumnName("NOME_PAI")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("NomeSocial")
                        .HasColumnName("NOME_SOCIAL")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Numero")
                        .HasColumnName("NUMERO")
                        .HasColumnType("varchar(30)");

                    b.Property<string>("OrgaoEmissor")
                        .HasColumnName("ORGAO_EMISSOR")
                        .HasColumnType("varchar(80)");

                    b.Property<string>("Sexo")
                        .IsRequired()
                        .HasColumnName("SEXO")
                        .HasColumnType("char(1)");

                    b.Property<string>("TIPO")
                        .IsRequired();

                    b.Property<int>("TipoDocumento")
                        .HasColumnName("TIPO_DOCUMENTO")
                        .HasColumnType("int");

                    b.Property<string>("UltimaSerie")
                        .HasColumnName("ULTIMA_SERIE")
                        .HasColumnType("varchar(80)");

                    b.HasKey("Id");

                    b.HasIndex("ContatoId");

                    b.HasIndex("InformacaoProfissionalId");

                    b.ToTable("PESSOA");

                    b.HasDiscriminator<string>("TIPO").HasValue("Pessoa");
                });

            modelBuilder.Entity("Sesc.Domain.Habilitacao.Entities.Solicitacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Categoria")
                        .HasColumnName("CATEGORIA")
                        .HasColumnType("int");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnName("CPF")
                        .HasColumnType("varchar(11)");

                    b.Property<DateTime?>("DataEnvio")
                        .HasColumnName("DATA_ENVIO")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DataRegistro")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("DATA_REGISTRO")
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("getdate()");

                    b.Property<bool>("EmAtendimento")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("EM_ATENDIMENTO")
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<int?>("Plataforma")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("PLATAFORMA")
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<int>("Situacao")
                        .HasColumnName("SITUACAO")
                        .HasColumnType("int");

                    b.Property<int>("Tipo")
                        .HasColumnName("TIPO")
                        .HasColumnType("int");

                    b.Property<int>("TitularId")
                        .HasColumnName("TITULAR_ID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TitularId");

                    b.ToTable("SOLICITACAO");
                });

            modelBuilder.Entity("Sesc.Domain.Habilitacao.Entities.Dependente", b =>
                {
                    b.HasBaseType("Sesc.Domain.Habilitacao.Entities.Pessoa");

                    b.Property<int?>("Acao")
                        .HasColumnName("ACAO")
                        .HasColumnType("int");

                    b.Property<int>("Parentesco")
                        .HasColumnName("PARENTESCO")
                        .HasColumnType("int");

                    b.Property<int>("TitularId")
                        .HasColumnName("TITULAR_ID")
                        .HasColumnType("int");

                    b.HasIndex("TitularId");

                    b.ToTable("DEPENDENTE");

                    b.HasDiscriminator().HasValue("DEPENDENTE");
                });

            modelBuilder.Entity("Sesc.Domain.Habilitacao.Entities.Responsavel", b =>
                {
                    b.HasBaseType("Sesc.Domain.Habilitacao.Entities.Pessoa");

                    b.Property<int?>("Parentesco")
                        .HasColumnName("PARENTESCO")
                        .HasColumnType("int");

                    b.ToTable("RESPONSAVEL");

                    b.HasDiscriminator().HasValue("RESPONSAVEL");
                });

            modelBuilder.Entity("Sesc.Domain.Habilitacao.Entities.Titular", b =>
                {
                    b.HasBaseType("Sesc.Domain.Habilitacao.Entities.Pessoa");

                    b.Property<int?>("ResponsavelId")
                        .HasColumnName("RESPONSAVEL_ID")
                        .HasColumnType("int");

                    b.HasIndex("ResponsavelId");

                    b.ToTable("TITULAR");

                    b.HasDiscriminator().HasValue("TITULAR");
                });

            modelBuilder.Entity("Sesc.Domain.Habilitacao.Entities.Atendimento", b =>
                {
                    b.HasOne("Sesc.Domain.Habilitacao.Entities.Solicitacao", "Solicitacao")
                        .WithMany("Atendimentos")
                        .HasForeignKey("SolicitacaoId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Sesc.Domain.Habilitacao.Entities.Cidade", b =>
                {
                    b.HasOne("Sesc.Domain.Habilitacao.Entities.Cidade", "CidadeResponsavel")
                        .WithMany()
                        .HasForeignKey("CidadeResponsavelId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Sesc.Domain.Habilitacao.Entities.Estado", "Estado")
                        .WithMany()
                        .HasForeignKey("EstadoId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Sesc.Domain.Habilitacao.Entities.Contato", b =>
                {
                    b.HasOne("Sesc.Domain.Habilitacao.Entities.Cidade", "Cidade")
                        .WithMany()
                        .HasForeignKey("CidadeId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Sesc.Domain.Habilitacao.Entities.Documento", b =>
                {
                    b.HasOne("Sesc.Domain.Habilitacao.Entities.Pessoa", "Pessoa")
                        .WithMany("Documentos")
                        .HasForeignKey("PessoaId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Sesc.Domain.Habilitacao.Entities.Pessoa", b =>
                {
                    b.HasOne("Sesc.Domain.Habilitacao.Entities.Contato", "Contato")
                        .WithMany()
                        .HasForeignKey("ContatoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Sesc.Domain.Habilitacao.Entities.InformacaoProfissional", "InformacaoProfissional")
                        .WithMany()
                        .HasForeignKey("InformacaoProfissionalId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Sesc.Domain.Habilitacao.Entities.Solicitacao", b =>
                {
                    b.HasOne("Sesc.Domain.Habilitacao.Entities.Titular", "Titular")
                        .WithMany()
                        .HasForeignKey("TitularId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Sesc.Domain.Habilitacao.Entities.Dependente", b =>
                {
                    b.HasOne("Sesc.Domain.Habilitacao.Entities.Titular", "Titular")
                        .WithMany("Dependentes")
                        .HasForeignKey("TitularId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Sesc.Domain.Habilitacao.Entities.Titular", b =>
                {
                    b.HasOne("Sesc.Domain.Habilitacao.Entities.Responsavel", "Responsavel")
                        .WithMany()
                        .HasForeignKey("ResponsavelId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
