using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agilis.Infra.Data.SqlServer.Migrations
{
    public partial class EpicoFeatureMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tarefas_Produtos_ProdutoId",
                table: "Tarefas");

            migrationBuilder.RenameColumn(
                name: "ProdutoId",
                table: "Tarefas",
                newName: "FeatureId");

            migrationBuilder.RenameIndex(
                name: "IX_Tarefas_ProdutoId",
                table: "Tarefas",
                newName: "IX_Tarefas_FeatureId");

            migrationBuilder.CreateTable(
                name: "Epicos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    ProdutoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataUltimaAlteracao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Epicos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Epicos_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    EpicoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataUltimaAlteracao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Features_Epicos_EpicoId",
                        column: x => x.EpicoId,
                        principalTable: "Epicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Epicos_ProdutoId",
                table: "Epicos",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_Features_EpicoId",
                table: "Features",
                column: "EpicoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tarefas_Features_FeatureId",
                table: "Tarefas",
                column: "FeatureId",
                principalTable: "Features",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tarefas_Features_FeatureId",
                table: "Tarefas");

            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropTable(
                name: "Epicos");

            migrationBuilder.RenameColumn(
                name: "FeatureId",
                table: "Tarefas",
                newName: "ProdutoId");

            migrationBuilder.RenameIndex(
                name: "IX_Tarefas_FeatureId",
                table: "Tarefas",
                newName: "IX_Tarefas_ProdutoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tarefas_Produtos_ProdutoId",
                table: "Tarefas",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
