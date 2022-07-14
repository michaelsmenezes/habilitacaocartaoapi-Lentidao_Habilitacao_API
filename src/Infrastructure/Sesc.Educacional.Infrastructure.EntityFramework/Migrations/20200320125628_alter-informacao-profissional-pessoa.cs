using Microsoft.EntityFrameworkCore.Migrations;

namespace Sesc.MeuSesc.Infrastructure.EntityFramework.Migrations
{
    public partial class alterinformacaoprofissionalpessoa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_INFORMACAO_PROFISSIONAL_PESSOA_PESSOA_ID",
                table: "INFORMACAO_PROFISSIONAL");

            migrationBuilder.DropIndex(
                name: "IX_INFORMACAO_PROFISSIONAL_PESSOA_ID",
                table: "INFORMACAO_PROFISSIONAL");

            migrationBuilder.DropColumn(
                name: "PESSOA_ID",
                table: "INFORMACAO_PROFISSIONAL");

            migrationBuilder.AddColumn<int>(
                name: "INFORMACAO_PROFISSIONAL_ID",
                table: "PESSOA",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PESSOA_INFORMACAO_PROFISSIONAL_ID",
                table: "PESSOA",
                column: "INFORMACAO_PROFISSIONAL_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PESSOA_INFORMACAO_PROFISSIONAL_INFORMACAO_PROFISSIONAL_ID",
                table: "PESSOA",
                column: "INFORMACAO_PROFISSIONAL_ID",
                principalTable: "INFORMACAO_PROFISSIONAL",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PESSOA_INFORMACAO_PROFISSIONAL_INFORMACAO_PROFISSIONAL_ID",
                table: "PESSOA");

            migrationBuilder.DropIndex(
                name: "IX_PESSOA_INFORMACAO_PROFISSIONAL_ID",
                table: "PESSOA");

            migrationBuilder.DropColumn(
                name: "INFORMACAO_PROFISSIONAL_ID",
                table: "PESSOA");

            migrationBuilder.AddColumn<int>(
                name: "PESSOA_ID",
                table: "INFORMACAO_PROFISSIONAL",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_INFORMACAO_PROFISSIONAL_PESSOA_ID",
                table: "INFORMACAO_PROFISSIONAL",
                column: "PESSOA_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_INFORMACAO_PROFISSIONAL_PESSOA_PESSOA_ID",
                table: "INFORMACAO_PROFISSIONAL",
                column: "PESSOA_ID",
                principalTable: "PESSOA",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
