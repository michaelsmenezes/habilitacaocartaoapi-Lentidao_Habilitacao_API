using Microsoft.EntityFrameworkCore.Migrations;

namespace Sesc.MeuSesc.Infrastructure.EntityFramework.Migrations
{
    public partial class SITUACAO_PESSOA_CHANGES : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CATEGORIA",
                table: "SOLICITACAO",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "NOME_SOCIAL",
                table: "PESSOA",
                type: "varchar(250)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(250)");

            migrationBuilder.AlterColumn<string>(
                name: "EMAIL",
                table: "PESSOA",
                type: "varchar(250)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(250)");

            migrationBuilder.AddColumn<int>(
                name: "ESTADO_CIVIL",
                table: "PESSOA",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ULTIMA_SERIE",
                table: "PESSOA",
                type: "varchar(80)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CATEGORIA",
                table: "SOLICITACAO");

            migrationBuilder.DropColumn(
                name: "ESTADO_CIVIL",
                table: "PESSOA");

            migrationBuilder.DropColumn(
                name: "ULTIMA_SERIE",
                table: "PESSOA");

            migrationBuilder.AlterColumn<string>(
                name: "NOME_SOCIAL",
                table: "PESSOA",
                type: "varchar(250)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EMAIL",
                table: "PESSOA",
                type: "varchar(250)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldNullable: true);
        }
    }
}
