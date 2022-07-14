using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sesc.MeuSesc.Infrastructure.EntityFramework.Migrations
{
    public partial class createnotificacaotemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NOTIFICACAO_TEMPLATE",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ASSUNTO_MODELO = table.Column<string>(type: "varchar(80)", nullable: false),
                    IDENTIFICADOR = table.Column<string>(type: "varchar(42)", nullable: false),
                    TEXTO_MODELO = table.Column<string>(type: "varchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NOTIFICACAO_TEMPLATE", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NOTIFICACAO_TEMPLATE");
        }
    }
}
