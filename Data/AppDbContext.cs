using Microsoft.EntityFrameworkCore;
using SISCOMBUST.Models;

namespace SISCOMBUST.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<SolicitudCompra> SolicitudesCompra { get; set; }
        public DbSet<Consumo> Consumos { get; set; }
        public DbSet<Unidad> Unidades { get; set; }
        public DbSet<Operacion> Operaciones { get; set; }
    }
}
