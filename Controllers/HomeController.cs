using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SISCOMBUST.Data;

namespace SISCOMBUST.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var añoActual = DateTime.Now.Year;

            // 📊 Totales generales
            var totalProveedores = await _context.Proveedores.CountAsync();
            var solicitudesPendientes = await _context.SolicitudesCompra.CountAsync(s => s.Estado == "Pendiente");
            var comprasTotales = await _context.Compras.CountAsync();
            var consumosTotales = await _context.Consumos.CountAsync();
            var stock = await _context.StockCombustible.FirstOrDefaultAsync();
            var stockActual = stock?.GalonesDisponibles ?? 0;
            var stockCritico = 2000; //VALOR UNMBRAL DE ALERTA


            // 📈 Gráfico mensual de consumo
            var consumoMensual = await _context.Consumos
                .Where(c => c.Fecha.Year == añoActual)
                .GroupBy(c => c.Fecha.Month)
                .Select(g => new { Mes = g.Key, Total = g.Sum(c => c.GalonesConsumidos) })
                .ToListAsync();

            // 🥧 Gráfico de compras por proveedor
            var comprasProveedor = await _context.Compras
                .Include(c => c.Proveedor)
                .GroupBy(c => c.Proveedor.NombreProveedor)
                .Select(g => new { Proveedor = g.Key, Total = g.Sum(c => c.GalonesComprados) })
                .ToListAsync();

            // 📅 Resumen anual
            var galonesAnuales = await _context.Consumos
                .Where(c => c.Fecha.Year == añoActual)
                .SumAsync(c => (decimal?)c.GalonesConsumidos) ?? 0;

            var operacionesUnicas = await _context.Consumos
                .Where(c => c.Fecha.Year == añoActual)
                .Select(c => c.Operacion)
                .Distinct()
                .CountAsync();

            var proveedoresActivos = await _context.Compras
                .Where(c => c.FechaCompra.Year == añoActual)
                .Select(c => c.IdProveedor)
                .Distinct()
                .CountAsync();

            var promedioMensual = consumoMensual.Any() ? consumoMensual.Average(c => c.Total) : 0;

            // 📦 Pasamos datos a la vista
            ViewBag.TotalProveedores = totalProveedores;
            ViewBag.SolicitudesPendientes = solicitudesPendientes;
            ViewBag.ComprasTotales = comprasTotales;
            ViewBag.ConsumosTotales = consumosTotales;
            ViewBag.ConsumoMensual = consumoMensual;
            ViewBag.ComprasProveedor = comprasProveedor;
            ViewBag.StockActual = stock?.GalonesDisponibles ?? 0;

            ViewBag.GalonesAnuales = galonesAnuales;
            ViewBag.OperacionesUnicas = operacionesUnicas;
            ViewBag.ProveedoresActivos = proveedoresActivos;
            ViewBag.PromedioMensual = Math.Round(promedioMensual, 2);
            ViewBag.StockActual = stockActual;
            ViewBag.EsCritico = stockActual < stockCritico;

            return View();
        }
    }
}
