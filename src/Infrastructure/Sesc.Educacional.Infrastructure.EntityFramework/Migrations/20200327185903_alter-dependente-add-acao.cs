using Microsoft.EntityFrameworkCore.Migrations;

namespace Sesc.MeuSesc.Infrastructure.EntityFramework.Migrations
{
    public partial class alterdependenteaddacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ACAO",
                table: "PESSOA",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ACAO",
                table: "PESSOA");
        }
    }
}
