using Microsoft.EntityFrameworkCore.Migrations;

namespace Sesc.MeuSesc.Infrastructure.EntityFramework.Migrations
{
    public partial class CriacaoCidadeResponsavel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CIDADE_RESPONSAVEL_ID",
                table: "CIDADE",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CIDADE_CIDADE_RESPONSAVEL_ID",
                table: "CIDADE",
                column: "CIDADE_RESPONSAVEL_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CIDADE_CIDADE_CIDADE_RESPONSAVEL_ID",
                table: "CIDADE",
                column: "CIDADE_RESPONSAVEL_ID",
                principalTable: "CIDADE",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CIDADE_CIDADE_CIDADE_RESPONSAVEL_ID",
                table: "CIDADE");

            migrationBuilder.DropIndex(
                name: "IX_CIDADE_CIDADE_RESPONSAVEL_ID",
                table: "CIDADE");

            migrationBuilder.DropColumn(
                name: "CIDADE_RESPONSAVEL_ID",
                table: "CIDADE");
        }
    }
}
