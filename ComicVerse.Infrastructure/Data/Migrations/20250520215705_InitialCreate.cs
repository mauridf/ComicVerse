using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComicVerse.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Editoras",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Imagem = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Descricao = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CriadaEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AlteradaEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Editoras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Equipes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Imagem = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Descricao = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CriadaEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AlteradaEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HQs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Titulo = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Imagem = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Descricao = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    CriadaEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AlteradaEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HQs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Personagens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Imagem = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Descricao = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CriadaEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AlteradaEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personagens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Edicoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    HQId = table.Column<Guid>(type: "uuid", nullable: false),
                    Numero = table.Column<int>(type: "integer", nullable: false),
                    Titulo = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    DataLancamento = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Imagem = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Observacoes = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    Lida = table.Column<bool>(type: "boolean", nullable: false),
                    Nota = table.Column<decimal>(type: "numeric(3,1)", nullable: true),
                    CriadaEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AlteradaEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Edicoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Edicoes_HQs_HQId",
                        column: x => x.HQId,
                        principalTable: "HQs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HQEditoras",
                columns: table => new
                {
                    HQId = table.Column<Guid>(type: "uuid", nullable: false),
                    EditoraId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HQEditoras", x => new { x.HQId, x.EditoraId });
                    table.ForeignKey(
                        name: "FK_HQEditoras_Editoras_EditoraId",
                        column: x => x.EditoraId,
                        principalTable: "Editoras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HQEditoras_HQs_HQId",
                        column: x => x.HQId,
                        principalTable: "HQs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HQEquipes",
                columns: table => new
                {
                    HQId = table.Column<Guid>(type: "uuid", nullable: false),
                    EquipeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HQEquipes", x => new { x.HQId, x.EquipeId });
                    table.ForeignKey(
                        name: "FK_HQEquipes_Equipes_EquipeId",
                        column: x => x.EquipeId,
                        principalTable: "Equipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HQEquipes_HQs_HQId",
                        column: x => x.HQId,
                        principalTable: "HQs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HQPersonagens",
                columns: table => new
                {
                    HQId = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonagemId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HQPersonagens", x => new { x.HQId, x.PersonagemId });
                    table.ForeignKey(
                        name: "FK_HQPersonagens_HQs_HQId",
                        column: x => x.HQId,
                        principalTable: "HQs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HQPersonagens_Personagens_PersonagemId",
                        column: x => x.PersonagemId,
                        principalTable: "Personagens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonagemEquipes",
                columns: table => new
                {
                    PersonagemId = table.Column<Guid>(type: "uuid", nullable: false),
                    EquipeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonagemEquipes", x => new { x.PersonagemId, x.EquipeId });
                    table.ForeignKey(
                        name: "FK_PersonagemEquipes_Equipes_EquipeId",
                        column: x => x.EquipeId,
                        principalTable: "Equipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonagemEquipes_Personagens_PersonagemId",
                        column: x => x.PersonagemId,
                        principalTable: "Personagens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Edicoes_HQId_Numero",
                table: "Edicoes",
                columns: new[] { "HQId", "Numero" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HQEditoras_EditoraId",
                table: "HQEditoras",
                column: "EditoraId");

            migrationBuilder.CreateIndex(
                name: "IX_HQEquipes_EquipeId",
                table: "HQEquipes",
                column: "EquipeId");

            migrationBuilder.CreateIndex(
                name: "IX_HQPersonagens_PersonagemId",
                table: "HQPersonagens",
                column: "PersonagemId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonagemEquipes_EquipeId",
                table: "PersonagemEquipes",
                column: "EquipeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Edicoes");

            migrationBuilder.DropTable(
                name: "HQEditoras");

            migrationBuilder.DropTable(
                name: "HQEquipes");

            migrationBuilder.DropTable(
                name: "HQPersonagens");

            migrationBuilder.DropTable(
                name: "PersonagemEquipes");

            migrationBuilder.DropTable(
                name: "Editoras");

            migrationBuilder.DropTable(
                name: "HQs");

            migrationBuilder.DropTable(
                name: "Equipes");

            migrationBuilder.DropTable(
                name: "Personagens");
        }
    }
}
