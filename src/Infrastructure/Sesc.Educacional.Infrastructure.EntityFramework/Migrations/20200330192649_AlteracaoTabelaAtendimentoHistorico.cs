using Microsoft.EntityFrameworkCore.Migrations;

namespace Sesc.MeuSesc.Infrastructure.EntityFramework.Migrations
{
    public partial class AlteracaoTabelaAtendimentoHistorico : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EM_ATENDIMENTO",
                table: "SOLICITACAO",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PLATAFORMA",
                table: "SOLICITACAO",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NOME",
                table: "ATENDIMENTO",
                type: "varchar(250)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OBSERVACAO",
                table: "ATENDIMENTO",
                type: "varchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SITUACAO_SOLICITACAO",
                table: "ATENDIMENTO",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EM_ATENDIMENTO",
                table: "SOLICITACAO");

            migrationBuilder.DropColumn(
                name: "PLATAFORMA",
                table: "SOLICITACAO");

            migrationBuilder.DropColumn(
                name: "NOME",
                table: "ATENDIMENTO");

            migrationBuilder.DropColumn(
                name: "OBSERVACAO",
                table: "ATENDIMENTO");

            migrationBuilder.DropColumn(
                name: "SITUACAO_SOLICITACAO",
                table: "ATENDIMENTO");
        }
    }
}
