using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Startly.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoCampoLogoTabelaStartup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoImagem",
                table: "TB_StartupImagens");

            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "TB_Startup",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Logo",
                table: "TB_Startup");

            migrationBuilder.AddColumn<string>(
                name: "TipoImagem",
                table: "TB_StartupImagens",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
