using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SISCOMBUST.Migrations
{
    /// <inheritdoc />
    public partial class AddStockCombustible : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockCombustible",
                columns: table => new
                {
                    IdStock = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GalonesDisponibles = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    FechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockCombustible", x => x.IdStock);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockCombustible");
        }
    }
}
