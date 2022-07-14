using Microsoft.EntityFrameworkCore.Migrations;

namespace Sesc.MeuSesc.Infrastructure.EntityFramework.Migrations
{
    public partial class altercontatocidadenullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CIDADE_ID",
                table: "CONTATO",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CIDADE_ID",
                table: "CONTATO",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
