using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Startly.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TB_Atuacao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Descricao = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Atuacao", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TB_Startup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descricao = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Metas = table.Column<string>(type: "varchar(3000)", maxLength: 3000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CNPJ = table.Column<string>(type: "varchar(14)", maxLength: 14, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Cep = table.Column<string>(type: "varchar(9)", maxLength: 9, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Logradouro = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Numero = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Bairro = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Municipio = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UF = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SiteStartup = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    QuantidadeFuncionario = table.Column<int>(type: "int", nullable: false),
                    EnumTicket = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EnumTipoDeAtendimento = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ResponsavelCadastro = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Login = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Senha = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Startup", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TB_StartupAtuacao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    StartupId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    AtuacaoId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_StartupAtuacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_StartupAtuacao_TB_Atuacao_AtuacaoId",
                        column: x => x.AtuacaoId,
                        principalTable: "TB_Atuacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_StartupAtuacao_TB_Startup_StartupId",
                        column: x => x.StartupId,
                        principalTable: "TB_Startup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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

            migrationBuilder.CreateTable(
                name: "TB_StartupImagens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    StartupId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TipoImagem = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Imagem = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_StartupImagens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_StartupImagens_TB_Startup_StartupId",
                        column: x => x.StartupId,
                        principalTable: "TB_Startup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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
                name: "IX_TB_StartupAtuacao_AtuacaoId",
                table: "TB_StartupAtuacao",
                column: "AtuacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_StartupAtuacao_StartupId",
                table: "TB_StartupAtuacao",
                column: "StartupId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_StartupContato_StartupId",
                table: "TB_StartupContato",
                column: "StartupId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_StartupImagens_StartupId",
                table: "TB_StartupImagens",
                column: "StartupId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_StartupVideo_StartupId",
                table: "TB_StartupVideo",
                column: "StartupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_StartupAtuacao");

            migrationBuilder.DropTable(
                name: "TB_StartupContato");

            migrationBuilder.DropTable(
                name: "TB_StartupImagens");

            migrationBuilder.DropTable(
                name: "TB_StartupVideo");

            migrationBuilder.DropTable(
                name: "TB_Atuacao");

            migrationBuilder.DropTable(
                name: "TB_Startup");
        }
    }
}
