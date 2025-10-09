using Microsoft.EntityFrameworkCore;
using SISCOMBUST.Models;

namespace SISCOMBUST.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<SolicitudCompra> SolicitudesCompra { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<Consumo> Consumos { get; set; }
        public DbSet<StockCombustible> StockCombustible { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 🔹 Usuarios iniciales
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    IdUsuario = 20250001,
                    NombreUsuario = "admin",
                    Password = "admin",
                    Rol = "Administrador"
                },
                new Usuario
                {
                    IdUsuario = 2025002,
                    NombreUsuario = "operador",
                    Password = "operador",
                    Rol = "Operador"
                }
            );
        }
    }
}
