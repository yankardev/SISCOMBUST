using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SISCOMBUST.Migrations
{
    /// <inheritdoc />
    public partial class AddRelacionSolicitudCompra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdSolicitud",
                table: "Compras",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Compras_IdSolicitud",
                table: "Compras",
                column: "IdSolicitud");

            migrationBuilder.AddForeignKey(
                name: "FK_Compras_SolicitudesCompra_IdSolicitud",
                table: "Compras",
                column: "IdSolicitud",
                principalTable: "SolicitudesCompra",
                principalColumn: "IdSolicitud");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compras_SolicitudesCompra_IdSolicitud",
                table: "Compras");

            migrationBuilder.DropIndex(
                name: "IX_Compras_IdSolicitud",
                table: "Compras");

            migrationBuilder.DropColumn(
                name: "IdSolicitud",
                table: "Compras");
        }
    }
}
