using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SISCOMBUST.Migrations
{
    /// <inheritdoc />
    public partial class CrearTablaSolicitudCompra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SolicitudesCompra_Usuarios_IdUsuario",
                table: "SolicitudesCompra");

            migrationBuilder.RenameColumn(
                name: "IdUsuario",
                table: "SolicitudesCompra",
                newName: "IdProveedor");

            migrationBuilder.RenameIndex(
                name: "IX_SolicitudesCompra_IdUsuario",
                table: "SolicitudesCompra",
                newName: "IX_SolicitudesCompra_IdProveedor");

            migrationBuilder.AddColumn<string>(
                name: "Observacion",
                table: "SolicitudesCompra",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PrecioReferencial",
                table: "SolicitudesCompra",
                type: "decimal(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Solicitante",
                table: "SolicitudesCompra",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitudesCompra_Proveedores_IdProveedor",
                table: "SolicitudesCompra",
                column: "IdProveedor",
                principalTable: "Proveedores",
                principalColumn: "IdProveedor",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SolicitudesCompra_Proveedores_IdProveedor",
                table: "SolicitudesCompra");

            migrationBuilder.DropColumn(
                name: "Observacion",
                table: "SolicitudesCompra");

            migrationBuilder.DropColumn(
                name: "PrecioReferencial",
                table: "SolicitudesCompra");

            migrationBuilder.DropColumn(
                name: "Solicitante",
                table: "SolicitudesCompra");

            migrationBuilder.RenameColumn(
                name: "IdProveedor",
                table: "SolicitudesCompra",
                newName: "IdUsuario");

            migrationBuilder.RenameIndex(
                name: "IX_SolicitudesCompra_IdProveedor",
                table: "SolicitudesCompra",
                newName: "IX_SolicitudesCompra_IdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitudesCompra_Usuarios_IdUsuario",
                table: "SolicitudesCompra",
                column: "IdUsuario",
                principalTable: "Usuarios",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
