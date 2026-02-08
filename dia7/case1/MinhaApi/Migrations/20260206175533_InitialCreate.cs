using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Minhaapi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "lotes_minerio",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CodigoLote = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    MinaOrigem = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    TeorFe = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    Umidade = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    SiO2 = table.Column<decimal>(type: "numeric(5,2)", nullable: true),
                    P = table.Column<decimal>(type: "numeric(5,3)", nullable: true),
                    Toneladas = table.Column<decimal>(type: "numeric(12,3)", nullable: false),
                    DataProducao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    LocalizacaoAtual = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lotes_minerio", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_lotes_minerio_CodigoLote",
                schema: "public",
                table: "lotes_minerio",
                column: "CodigoLote",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "lotes_minerio",
                schema: "public");
        }
    }
}
