using Agilis.Core.Domain.Enums;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agilis.Infra.Data.SqlServer.Migrations
{
    public partial class TipoTarefaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Tipo",
                table: "Tarefas",
                type: "int",
                nullable: false,
                defaultValue: (int)TipoTarefa.Novidade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Tarefas");
        }
    }
}
