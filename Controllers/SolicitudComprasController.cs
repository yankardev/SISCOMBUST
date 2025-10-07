using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SISCOMBUST.Data;
using SISCOMBUST.Models;

namespace SISCOMBUST.Controllers
{
    public class SolicitudComprasController : Controller
    {
        private readonly AppDbContext _context;

        public SolicitudComprasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: SolicitudCompras
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.SolicitudesCompra.Include(s => s.Proveedor);
            return View(await appDbContext.ToListAsync());
        }

        // GET: SolicitudCompras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitudCompra = await _context.SolicitudesCompra
                .Include(s => s.Proveedor)
                .FirstOrDefaultAsync(m => m.IdSolicitud == id);
            if (solicitudCompra == null)
            {
                return NotFound();
            }

            return View(solicitudCompra);
        }

        // GET: SolicitudCompras/Create
        public IActionResult Create()
        {
            ViewData["IdProveedor"] = new SelectList(_context.Proveedores, "IdProveedor", "NombreProveedor");
            return View();
        }

        // POST: SolicitudCompras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSolicitud,FechaSolicitud,IdProveedor,GalonesSolicitados,PrecioReferencial,Estado,Solicitante,Observacion")] SolicitudCompra solicitudCompra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(solicitudCompra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProveedor"] = new SelectList(_context.Proveedores, "IdProveedor", "NombreProveedor", solicitudCompra.IdProveedor);
            return View(solicitudCompra);
        }

        // GET: SolicitudCompras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitudCompra = await _context.SolicitudesCompra.FindAsync(id);
            if (solicitudCompra == null)
            {
                return NotFound();
            }
            ViewData["IdProveedor"] = new SelectList(_context.Proveedores, "IdProveedor", "NombreProveedor", solicitudCompra.IdProveedor);
            return View(solicitudCompra);
        }

        // POST: SolicitudCompras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdSolicitud,FechaSolicitud,IdProveedor,GalonesSolicitados,PrecioReferencial,Estado,Solicitante,Observacion")] SolicitudCompra solicitudCompra)
        {
            if (id != solicitudCompra.IdSolicitud)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(solicitudCompra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SolicitudCompraExists(solicitudCompra.IdSolicitud))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProveedor"] = new SelectList(_context.Proveedores, "IdProveedor", "NombreProveedor", solicitudCompra.IdProveedor);
            return View(solicitudCompra);
        }

        // GET: SolicitudCompras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitudCompra = await _context.SolicitudesCompra
                .Include(s => s.Proveedor)
                .FirstOrDefaultAsync(m => m.IdSolicitud == id);
            if (solicitudCompra == null)
            {
                return NotFound();
            }

            return View(solicitudCompra);
        }

        // POST: SolicitudCompras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var solicitudCompra = await _context.SolicitudesCompra.FindAsync(id);
            if (solicitudCompra != null)
            {
                _context.SolicitudesCompra.Remove(solicitudCompra);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool SolicitudCompraExists(int id)
        {
            return _context.SolicitudesCompra.Any(e => e.IdSolicitud == id);
        }
    }
}
