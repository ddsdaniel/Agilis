using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agilis.Infra.Data.SqlServer.Migrations
{
    public partial class HorasMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "HorasPrevistas",
                table: "Tarefas",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "HorasRealizadas",
                table: "Tarefas",
                type: "bigint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HorasPrevistas",
                table: "Tarefas");

            migrationBuilder.DropColumn(
                name: "HorasRealizadas",
                table: "Tarefas");
        }
    }
}
