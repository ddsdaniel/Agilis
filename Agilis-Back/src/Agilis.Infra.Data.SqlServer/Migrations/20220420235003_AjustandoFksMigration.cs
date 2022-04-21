using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agilis.Infra.Data.SqlServer.Migrations
{
    public partial class AjustandoFksMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Epicos_Produtos_ProdutoId",
                table: "Epicos");

            migrationBuilder.DropForeignKey(
                name: "FK_Features_Epicos_EpicoId",
                table: "Features");

            migrationBuilder.DropForeignKey(
                name: "FK_Tarefas_Features_FeatureId",
                table: "Tarefas");

            migrationBuilder.AlterColumn<Guid>(
                name: "FeatureId",
                table: "Tarefas",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "RelatorId",
                table: "Tarefas",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SolucionadorId",
                table: "Tarefas",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "EpicoId",
                table: "Features",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProdutoId",
                table: "Epicos",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Tarefas_RelatorId",
                table: "Tarefas",
                column: "RelatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Tarefas_SolucionadorId",
                table: "Tarefas",
                column: "SolucionadorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Epicos_Produtos_ProdutoId",
                table: "Epicos",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Features_Epicos_EpicoId",
                table: "Features",
                column: "EpicoId",
                principalTable: "Epicos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tarefas_Features_FeatureId",
                table: "Tarefas",
                column: "FeatureId",
                principalTable: "Features",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tarefas_Usuarios_RelatorId",
                table: "Tarefas",
                column: "RelatorId",
                principalTable: "Usuarios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tarefas_Usuarios_SolucionadorId",
                table: "Tarefas",
                column: "SolucionadorId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Epicos_Produtos_ProdutoId",
                table: "Epicos");

            migrationBuilder.DropForeignKey(
                name: "FK_Features_Epicos_EpicoId",
                table: "Features");

            migrationBuilder.DropForeignKey(
                name: "FK_Tarefas_Features_FeatureId",
                table: "Tarefas");

            migrationBuilder.DropForeignKey(
                name: "FK_Tarefas_Usuarios_RelatorId",
                table: "Tarefas");

            migrationBuilder.DropForeignKey(
                name: "FK_Tarefas_Usuarios_SolucionadorId",
                table: "Tarefas");

            migrationBuilder.DropIndex(
                name: "IX_Tarefas_RelatorId",
                table: "Tarefas");

            migrationBuilder.DropIndex(
                name: "IX_Tarefas_SolucionadorId",
                table: "Tarefas");

            migrationBuilder.DropColumn(
                name: "RelatorId",
                table: "Tarefas");

            migrationBuilder.DropColumn(
                name: "SolucionadorId",
                table: "Tarefas");

            migrationBuilder.AlterColumn<Guid>(
                name: "FeatureId",
                table: "Tarefas",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "EpicoId",
                table: "Features",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ProdutoId",
                table: "Epicos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Epicos_Produtos_ProdutoId",
                table: "Epicos",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Features_Epicos_EpicoId",
                table: "Features",
                column: "EpicoId",
                principalTable: "Epicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tarefas_Features_FeatureId",
                table: "Tarefas",
                column: "FeatureId",
                principalTable: "Features",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
