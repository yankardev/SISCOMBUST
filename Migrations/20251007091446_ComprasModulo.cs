using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SISCOMBUST.Migrations
{
    /// <inheritdoc />
    public partial class ComprasModulo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PrecioPorGalon",
                table: "Compras",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,3)",
                oldPrecision: 10,
                oldScale: 3);

            migrationBuilder.AddColumn<string>(
                name: "NumeroFactura",
                table: "Compras",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Observacion",
                table: "Compras",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumeroFactura",
                table: "Compras");

            migrationBuilder.DropColumn(
                name: "Observacion",
                table: "Compras");

            migrationBuilder.AlterColumn<decimal>(
                name: "PrecioPorGalon",
                table: "Compras",
                type: "decimal(10,3)",
                precision: 10,
                scale: 3,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");
        }
    }
}
