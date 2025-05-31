using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Startly.Migrations
{
    /// <inheritdoc />
    public partial class AtualizacaoDaTabelaStartup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_StartupContato");

            migrationBuilder.AddColumn<string>(
                name: "EmailCorporativo",
                table: "TB_Startup",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "EmailPessoal",
                table: "TB_Startup",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "LinkedIn",
                table: "TB_Startup",
                type: "varchar(300)",
                maxLength: 300,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TelefoneFixo",
                table: "TB_Startup",
                type: "varchar(14)",
                maxLength: 14,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailCorporativo",
                table: "TB_Startup");

            migrationBuilder.DropColumn(
                name: "EmailPessoal",
                table: "TB_Startup");

            migrationBuilder.DropColumn(
                name: "LinkedIn",
                table: "TB_Startup");

            migrationBuilder.DropColumn(
                name: "TelefoneFixo",
                table: "TB_Startup");

            migrationBuilder.CreateTable(
                name: "TB_StartupContato",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    StartupId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Contato = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Conteudo = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_StartupContato", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_StartupContato_TB_Startup_StartupId",
                        column: x => x.StartupId,
                        principalTable: "TB_Startup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_TB_StartupContato_StartupId",
                table: "TB_StartupContato",
                column: "StartupId");
        }
    }
}
