using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClosedXML.Excel;
using Rotativa.AspNetCore;
using SISCOMBUST.Data;
using SISCOMBUST.Filters;
using SISCOMBUST.Models;




namespace SISCOMBUST.Controllers
{
    [RolAuthorize("Logística", "Administrador")] // Solo estos roles pueden acceder
    public class ComprasController : Controller
    {
        private readonly AppDbContext _context;

        public ComprasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Compras
        public async Task<IActionResult> Index()
        {
            var compras = await _context.Compras
                .Include(c => c.Proveedor)
                .OrderByDescending(c => c.FechaCompra)
                .ToListAsync();

            var solicitudesPendientes = await _context.SolicitudesCompra
             .CountAsync(s => s.Estado == "Pendiente");

            ViewBag.SolicitudesPendientes = solicitudesPendientes;

            return View(compras);
        }


        // GET: Compras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var compra = await _context.Compras
                .Include(c => c.Proveedor)
                .FirstOrDefaultAsync(m => m.IdCompra == id);

            if (compra == null) return NotFound();

            return View(compra);
        }

        // GET: Compras/Create
        public IActionResult Create()
        {
            // 🔸 Solo solicitudes pendientes
            var solicitudesPendientes = _context.SolicitudesCompra
                .Where(s => s.Estado == "Pendiente")
                .Include(s => s.Proveedor)
                .Select(s => new
                {
                    s.IdSolicitud,
                    Descripcion = "Solicitud #" + s.IdSolicitud + " - " + s.Proveedor.NombreProveedor + " (" + s.GalonesSolicitados + " gal)"
                })
                .ToList();

            ViewData["IdSolicitud"] = new SelectList(solicitudesPendientes, "IdSolicitud", "Descripcion");
            ViewData["IdProveedor"] = new SelectList(_context.Proveedores, "IdProveedor", "NombreProveedor");
            return View();
        }


        // POST: Compras/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCompra,FechaCompra,IdProveedor,GalonesComprados,PrecioPorGalon,NumeroFactura,Observacion,IdSolicitud")] Compra compra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(compra);

                // 🔄 Si tiene una solicitud asociada, se marca como "Atendida"
                if (compra.IdSolicitud.HasValue)
                {
                    var solicitud = await _context.SolicitudesCompra.FindAsync(compra.IdSolicitud.Value);
                    if (solicitud != null)
                    {
                        solicitud.Estado = "Atendida";
                        _context.Update(solicitud);
                    }
                }

                // 🔹 Actualizar stock
                var stock = await _context.StockCombustible.FirstOrDefaultAsync();
                if (stock == null)
                {
                    stock = new StockCombustible { GalonesDisponibles = compra.GalonesComprados };
                    _context.StockCombustible.Add(stock);
                }
                else
                {
                    stock.GalonesDisponibles += compra.GalonesComprados;
                    stock.FechaActualizacion = DateTime.Now;
                    _context.StockCombustible.Update(stock);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdSolicitud"] = new SelectList(_context.SolicitudesCompra.Where(s => s.Estado == "Pendiente"), "IdSolicitud", "IdSolicitud", compra.IdSolicitud);
            ViewData["IdProveedor"] = new SelectList(_context.Proveedores, "IdProveedor", "NombreProveedor", compra.IdProveedor);
            return View(compra);
        }


        // GET: Compras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var compra = await _context.Compras.FindAsync(id);
            if (compra == null) return NotFound();

            ViewData["IdProveedor"] = new SelectList(_context.Proveedores, "IdProveedor", "NombreProveedor", compra.IdProveedor);
            return View(compra);
        }

        // POST: Compras/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCompra,FechaCompra,IdProveedor,GalonesComprados,PrecioPorGalon,NumeroFactura,Observacion")] Compra compra)
        {
            if (id != compra.IdCompra) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(compra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Compras.Any(e => e.IdCompra == compra.IdCompra))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdProveedor"] = new SelectList(_context.Proveedores, "IdProveedor", "NombreProveedor", compra.IdProveedor);
            return View(compra);
        }

        // GET: Compras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var compra = await _context.Compras
                .Include(c => c.Proveedor)
                .FirstOrDefaultAsync(m => m.IdCompra == id);

            if (compra == null) return NotFound();

            return View(compra);
        }

        // POST: Compras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var compra = await _context.Compras.FindAsync(id);
            if (compra != null)
            {
                _context.Compras.Remove(compra);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Reporte()
        {
            ViewData["IdProveedor"] = new SelectList(_context.Proveedores, "IdProveedor", "NombreProveedor");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GenerarReporte(DateTime? fechaInicio, DateTime? fechaFin, int? IdProveedor, string formato)
        {
            var compras = _context.Compras.Include(c => c.Proveedor).AsQueryable();

            if (fechaInicio.HasValue)
                compras = compras.Where(c => c.FechaCompra >= fechaInicio.Value);
            if (fechaFin.HasValue)
                compras = compras.Where(c => c.FechaCompra <= fechaFin.Value);
            if (IdProveedor.HasValue)
                compras = compras.Where(c => c.IdProveedor == IdProveedor.Value);

            var lista = await compras.OrderByDescending(c => c.FechaCompra).ToListAsync();

            if (formato == "pdf")
            {
                return new ViewAsPdf("ReportePDF", lista)
                {
                    FileName = $"Reporte_Compras_{DateTime.Now:yyyyMMdd}.pdf"
                };
            }
            else if (formato == "excel")
            {
                // Usa un nombre de archivo con la extensión .xlsx
                string fileName = $"Reporte_Compras_{DateTime.Now:yyyyMMdd}.xlsx";

                // Crea un nuevo libro de Excel
                using (var workbook = new XLWorkbook())
                {
                    // Agrega una hoja de cálculo llamada "Compras"
                    var worksheet = workbook.Worksheets.Add("Compras");

                    // --- Encabezados ---
                    var currentRow = 1;
                    worksheet.Cell(currentRow, 1).Value = "Fecha";
                    worksheet.Cell(currentRow, 2).Value = "Proveedor";
                    worksheet.Cell(currentRow, 3).Value = "Galones";
                    worksheet.Cell(currentRow, 4).Value = "Precio";
                    worksheet.Cell(currentRow, 5).Value = "Monto Total";

                    // Opcional: Poner los encabezados en negrita
                    worksheet.Row(currentRow).Style.Font.Bold = true;

                    // --- Datos ---
                    foreach (var compra in lista)
                    {
                        currentRow++;
                        worksheet.Cell(currentRow, 1).Value = compra.FechaCompra;
                        worksheet.Cell(currentRow, 2).Value = compra.Proveedor.NombreProveedor;
                        worksheet.Cell(currentRow, 3).Value = compra.GalonesComprados;
                        worksheet.Cell(currentRow, 4).Value = compra.PrecioPorGalon;
                        worksheet.Cell(currentRow, 5).Value = compra.MontoTotal;
                    }

                    // Opcional: Ajustar el ancho de las columnas al contenido
                    worksheet.Columns().AdjustToContents();

                    // --- Guardar y devolver el archivo ---
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();

                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            fileName);
                    }
                }
            }

            return View("Reporte", lista);
        }
    }
}
