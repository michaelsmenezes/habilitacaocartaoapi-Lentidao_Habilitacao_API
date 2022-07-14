using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sesc.MeuSesc.Infrastructure.EntityFramework.Migrations
{
    public partial class AlterInformacaoProfissionalAddDataDemissao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DATA_DEMISSAO",
                table: "INFORMACAO_PROFISSIONAL",
                type: "datetime",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DATA_DEMISSAO",
                table: "INFORMACAO_PROFISSIONAL");
        }
    }
}
