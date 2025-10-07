using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SISCOMBUST.Data;
using SISCOMBUST.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SISCOMBUST.Controllers
{
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
            ViewData["IdProveedor"] = new SelectList(_context.Proveedores, "IdProveedor", "NombreProveedor");
            return View();
        }

        // POST: Compras/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCompra,FechaCompra,IdProveedor,GalonesComprados,PrecioPorGalon,NumeroFactura,Observacion")] Compra compra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(compra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

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
    }
}
