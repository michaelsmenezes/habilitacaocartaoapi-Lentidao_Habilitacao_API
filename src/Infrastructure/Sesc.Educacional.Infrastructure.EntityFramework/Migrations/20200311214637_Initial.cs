using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sesc.MeuSesc.Infrastructure.EntityFramework.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ESTADO",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UF = table.Column<string>(type: "varchar(2)", nullable: false),
                    DESCRICAO = table.Column<string>(type: "varchar(150)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ESTADO", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CIDADE",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CODIGO_IBGE = table.Column<string>(type: "varchar(20)", nullable: true),
                    DESCRICAO = table.Column<string>(type: "varchar(150)", nullable: true),
                    ESTADO_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CIDADE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CIDADE_ESTADO_ESTADO_ID",
                        column: x => x.ESTADO_ID,
                        principalTable: "ESTADO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CONTATO",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LOGRADOURO = table.Column<string>(type: "varchar(250)", nullable: false),
                    COMPLEMENTO = table.Column<string>(type: "varchar(250)", nullable: false),
                    NUMERO = table.Column<string>(type: "varchar(10)", nullable: false),
                    CEP = table.Column<string>(type: "varchar(10)", nullable: false),
                    BAIRRO = table.Column<string>(type: "varchar(250)", nullable: false),
                    CIDADE_ID = table.Column<int>(type: "int", nullable: false),
                    DDD_PRINCIPAL = table.Column<string>(type: "varchar(2)", nullable: false),
                    TELEFONE_PRINCIPAL = table.Column<string>(type: "varchar(10)", nullable: false),
                    DDD_SECUNDARIO = table.Column<string>(type: "varchar(2)", nullable: true),
                    TELEFONE_SECUNDARIO = table.Column<string>(type: "varchar(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CONTATO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CONTATO_CIDADE_CIDADE_ID",
                        column: x => x.CIDADE_ID,
                        principalTable: "CIDADE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PESSOA",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NOME = table.Column<string>(type: "varchar(250)", nullable: false),
                    SEXO = table.Column<string>(type: "char(1)", nullable: false),
                    NOME_SOCIAL = table.Column<string>(type: "varchar(250)", nullable: false),
                    DATA_NASCIMENTO = table.Column<DateTime>(type: "datetime", nullable: false),
                    CPF = table.Column<string>(type: "varchar(11)", nullable: false),
                    EMAIL = table.Column<string>(type: "varchar(250)", nullable: false),
                    NOME_MAE = table.Column<string>(type: "varchar(250)", nullable: false),
                    CONTATO_ID = table.Column<int>(type: "int", nullable: false),
                    ESCOLARIDADE = table.Column<int>(type: "int", nullable: false),
                    NACIONALIDADE = table.Column<string>(type: "varchar(80)", nullable: false),
                    NATURALIDADE = table.Column<string>(type: "varchar(80)", nullable: false),
                    TIPO_DOCUMENTO = table.Column<int>(type: "int", nullable: false),
                    NUMERO = table.Column<string>(type: "varchar(30)", nullable: false),
                    ORGAO_EMISSOR = table.Column<string>(type: "varchar(80)", nullable: false),
                    DATA_EMISSAO = table.Column<DateTime>(type: "datetime", nullable: false),
                    TIPO = table.Column<string>(nullable: false),
                    PARENTESCO = table.Column<int>(type: "int", nullable: true),
                    TITULAR_ID = table.Column<int>(type: "int", nullable: true),
                    RESPONSAVEL_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PESSOA", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PESSOA_PESSOA_TITULAR_ID",
                        column: x => x.TITULAR_ID,
                        principalTable: "PESSOA",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PESSOA_CONTATO_CONTATO_ID",
                        column: x => x.CONTATO_ID,
                        principalTable: "CONTATO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PESSOA_PESSOA_RESPONSAVEL_ID",
                        column: x => x.RESPONSAVEL_ID,
                        principalTable: "PESSOA",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DOCUMENTO",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PESSOA_ID = table.Column<int>(type: "int", nullable: false),
                    TIPO = table.Column<int>(type: "int", nullable: false),
                    EXTENSAO = table.Column<string>(type: "varchar(10)", nullable: true),
                    HASH = table.Column<string>(type: "varchar(80)", nullable: true),
                    MIME_TYPE = table.Column<string>(type: "varchar(80)", nullable: true),
                    NOME = table.Column<string>(type: "varchar(250)", nullable: true),
                    DATA_REGISTRO = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    SITUACAO = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DOCUMENTO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DOCUMENTO_PESSOA_PESSOA_ID",
                        column: x => x.PESSOA_ID,
                        principalTable: "PESSOA",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "INFORMACAO_PROFISSIONAL",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PESSOA_ID = table.Column<int>(type: "int", nullable: false),
                    CNPJ = table.Column<string>(type: "varchar(14)", nullable: false),
                    NOME_EMPRESA = table.Column<string>(type: "varchar(250)", nullable: true),
                    DATA_ADMISSAO = table.Column<DateTime>(type: "datetime", nullable: false),
                    OCUPACAO = table.Column<string>(type: "varchar(80)", nullable: false),
                    NUMERO_CTPS = table.Column<string>(type: "varchar(80)", nullable: false),
                    SERIE_CTPS = table.Column<string>(type: "varchar(80)", nullable: false),
                    RENDA = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INFORMACAO_PROFISSIONAL", x => x.ID);
                    table.ForeignKey(
                        name: "FK_INFORMACAO_PROFISSIONAL_PESSOA_PESSOA_ID",
                        column: x => x.PESSOA_ID,
                        principalTable: "PESSOA",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SOLICITACAO",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TITULAR_ID = table.Column<int>(type: "int", nullable: false),
                    SITUACAO = table.Column<int>(type: "int", nullable: false),
                    CPF = table.Column<string>(type: "varchar(11)", nullable: false),
                    TIPO = table.Column<int>(type: "int", nullable: false),
                    DATA_REGISTRO = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SOLICITACAO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SOLICITACAO_PESSOA_TITULAR_ID",
                        column: x => x.TITULAR_ID,
                        principalTable: "PESSOA",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ATENDIMENTO",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SOLICITACAO_ID = table.Column<int>(type: "int", nullable: false),
                    USUARIO = table.Column<string>(type: "varchar(250)", nullable: false),
                    DATA_HORA_INICIO = table.Column<DateTime>(type: "datetime", nullable: false),
                    DATA_HORA_FIM = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ATENDIMENTO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ATENDIMENTO_SOLICITACAO_SOLICITACAO_ID",
                        column: x => x.SOLICITACAO_ID,
                        principalTable: "SOLICITACAO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ATENDIMENTO_SOLICITACAO_ID",
                table: "ATENDIMENTO",
                column: "SOLICITACAO_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CIDADE_ESTADO_ID",
                table: "CIDADE",
                column: "ESTADO_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CONTATO_CIDADE_ID",
                table: "CONTATO",
                column: "CIDADE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_DOCUMENTO_PESSOA_ID",
                table: "DOCUMENTO",
                column: "PESSOA_ID");

            migrationBuilder.CreateIndex(
                name: "IX_INFORMACAO_PROFISSIONAL_PESSOA_ID",
                table: "INFORMACAO_PROFISSIONAL",
                column: "PESSOA_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PESSOA_TITULAR_ID",
                table: "PESSOA",
                column: "TITULAR_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PESSOA_CONTATO_ID",
                table: "PESSOA",
                column: "CONTATO_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PESSOA_RESPONSAVEL_ID",
                table: "PESSOA",
                column: "RESPONSAVEL_ID");

            migrationBuilder.CreateIndex(
                name: "IX_SOLICITACAO_TITULAR_ID",
                table: "SOLICITACAO",
                column: "TITULAR_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ATENDIMENTO");

            migrationBuilder.DropTable(
                name: "DOCUMENTO");

            migrationBuilder.DropTable(
                name: "INFORMACAO_PROFISSIONAL");

            migrationBuilder.DropTable(
                name: "SOLICITACAO");

            migrationBuilder.DropTable(
                name: "PESSOA");

            migrationBuilder.DropTable(
                name: "CONTATO");

            migrationBuilder.DropTable(
                name: "CIDADE");

            migrationBuilder.DropTable(
                name: "ESTADO");
        }
    }
}
