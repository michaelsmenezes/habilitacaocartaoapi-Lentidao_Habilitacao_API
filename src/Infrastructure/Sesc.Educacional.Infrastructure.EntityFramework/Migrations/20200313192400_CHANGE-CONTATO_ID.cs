using Microsoft.EntityFrameworkCore.Migrations;

namespace Sesc.MeuSesc.Infrastructure.EntityFramework.Migrations
{
    public partial class CHANGECONTATO_ID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CONTATO_ID",
                table: "PESSOA",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CONTATO_ID",
                table: "PESSOA",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
