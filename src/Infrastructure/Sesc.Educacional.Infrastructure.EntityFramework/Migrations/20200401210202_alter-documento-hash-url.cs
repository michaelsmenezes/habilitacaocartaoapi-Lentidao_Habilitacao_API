using Microsoft.EntityFrameworkCore.Migrations;

namespace Sesc.MeuSesc.Infrastructure.EntityFramework.Migrations
{
    public partial class alterdocumentohashurl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HASH",
                table: "DOCUMENTO");

            migrationBuilder.AddColumn<string>(
                name: "URL",
                table: "DOCUMENTO",
                type: "varchar(255)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "URL",
                table: "DOCUMENTO");

            migrationBuilder.AddColumn<string>(
                name: "HASH",
                table: "DOCUMENTO",
                type: "varchar(80)",
                nullable: true);
        }
    }
}
