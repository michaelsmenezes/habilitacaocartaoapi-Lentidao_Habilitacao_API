using Microsoft.EntityFrameworkCore.Migrations;

namespace Sesc.MeuSesc.Infrastructure.EntityFramework.Migrations
{
    public partial class altercontatotelefoneremoveddd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DDD_PRINCIPAL",
                table: "CONTATO");

            migrationBuilder.DropColumn(
                name: "DDD_SECUNDARIO",
                table: "CONTATO");

            migrationBuilder.AlterColumn<string>(
                name: "TELEFONE_SECUNDARIO",
                table: "CONTATO",
                type: "varchar(20)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TELEFONE_PRINCIPAL",
                table: "CONTATO",
                type: "varchar(20)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TELEFONE_SECUNDARIO",
                table: "CONTATO",
                type: "varchar(10)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TELEFONE_PRINCIPAL",
                table: "CONTATO",
                type: "varchar(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)");

            migrationBuilder.AddColumn<string>(
                name: "DDD_PRINCIPAL",
                table: "CONTATO",
                type: "varchar(2)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DDD_SECUNDARIO",
                table: "CONTATO",
                type: "varchar(2)",
                nullable: true);
        }
    }
}
