using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agilis.Infra.Data.SqlServer.Migrations
{
    public partial class TarefaTagsV4Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TagTarefas",
                columns: table => new
                {
                    TagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TarefaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagTarefas", x => new { x.TarefaId, x.TagId });
                    table.ForeignKey(
                        name: "FK_TagTarefas_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagTarefas_Tarefas_TarefaId",
                        column: x => x.TarefaId,
                        principalTable: "Tarefas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TagTarefas_TagId",
                table: "TagTarefas",
                column: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TagTarefas");
        }
    }
}
