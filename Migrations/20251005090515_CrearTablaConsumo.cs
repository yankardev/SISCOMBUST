using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SISCOMBUST.Migrations
{
    /// <inheritdoc />
    public partial class CrearTablaConsumo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consumos_Unidades_IdUnidad",
                table: "Consumos");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumos_Usuarios_IdUsuario",
                table: "Consumos");

            migrationBuilder.DropIndex(
                name: "IX_Consumos_IdUnidad",
                table: "Consumos");

            migrationBuilder.DropIndex(
                name: "IX_Consumos_IdUsuario",
                table: "Consumos");

            migrationBuilder.DropColumn(
                name: "IdUnidad",
                table: "Consumos");

            migrationBuilder.RenameColumn(
                name: "IdUsuario",
                table: "Consumos",
                newName: "Unidades");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaSalida",
                table: "Consumos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "GalonesPorUnidad",
                table: "Consumos",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Observacion",
                table: "Consumos",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Operacion",
                table: "Consumos",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Ruta",
                table: "Consumos",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Supervisor",
                table: "Consumos",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaSalida",
                table: "Consumos");

            migrationBuilder.DropColumn(
                name: "GalonesPorUnidad",
                table: "Consumos");

            migrationBuilder.DropColumn(
                name: "Observacion",
                table: "Consumos");

            migrationBuilder.DropColumn(
                name: "Operacion",
                table: "Consumos");

            migrationBuilder.DropColumn(
                name: "Ruta",
                table: "Consumos");

            migrationBuilder.DropColumn(
                name: "Supervisor",
                table: "Consumos");

            migrationBuilder.RenameColumn(
                name: "Unidades",
                table: "Consumos",
                newName: "IdUsuario");

            migrationBuilder.AddColumn<int>(
                name: "IdUnidad",
                table: "Consumos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Consumos_IdUnidad",
                table: "Consumos",
                column: "IdUnidad");

            migrationBuilder.CreateIndex(
                name: "IX_Consumos_IdUsuario",
                table: "Consumos",
                column: "IdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumos_Unidades_IdUnidad",
                table: "Consumos",
                column: "IdUnidad",
                principalTable: "Unidades",
                principalColumn: "IdUnidad",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Consumos_Usuarios_IdUsuario",
                table: "Consumos",
                column: "IdUsuario",
                principalTable: "Usuarios",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
