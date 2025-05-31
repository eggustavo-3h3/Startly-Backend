using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Startly.Migrations
{
    /// <inheritdoc />
    public partial class AjusteEntidadeStartup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_StartupVideo");

            migrationBuilder.AddColumn<string>(
                name: "UrlVideo",
                table: "TB_Startup",
                type: "varchar(300)",
                maxLength: 300,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlVideo",
                table: "TB_Startup");

            migrationBuilder.CreateTable(
                name: "TB_StartupVideo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    StartupId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    LinkVideo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_StartupVideo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_StartupVideo_TB_Startup_StartupId",
                        column: x => x.StartupId,
                        principalTable: "TB_Startup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_TB_StartupVideo_StartupId",
                table: "TB_StartupVideo",
                column: "StartupId");
        }
    }
}
