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
    public class ConsumosController : Controller
    {
        private readonly AppDbContext _context;

        public ConsumosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Consumos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Consumos.ToListAsync());
        }

        // GET: Consumos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumo = await _context.Consumos
                .FirstOrDefaultAsync(m => m.IdConsumo == id);
            if (consumo == null)
            {
                return NotFound();
            }

            return View(consumo);
        }

        // GET: Consumos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Consumos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdConsumo,Fecha,Operacion,Ruta,Unidades,GalonesPorUnidad,GalonesConsumidos,FechaSalida,Supervisor,Observacion")] Consumo consumo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(consumo);
                await _context.SaveChangesAsync();

                // 🔻 Descontar del stock
                var stock = await _context.StockCombustible.FirstOrDefaultAsync();
                if (stock != null)
                {
                    stock.GalonesDisponibles -= consumo.GalonesConsumidos;
                    stock.FechaActualizacion = DateTime.Now;
                    _context.StockCombustible.Update(stock);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(consumo);
        }

        // GET: Consumos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumo = await _context.Consumos.FindAsync(id);
            if (consumo == null)
            {
                return NotFound();
            }
            return View(consumo);
        }

        // POST: Consumos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdConsumo,Fecha,Operacion,Ruta,Unidades,GalonesPorUnidad,GalonesConsumidos,FechaSalida,Supervisor,Observacion")] Consumo consumo)
        {
            if (id != consumo.IdConsumo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consumo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsumoExists(consumo.IdConsumo))
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
            return View(consumo);
        }

        // GET: Consumos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumo = await _context.Consumos
                .FirstOrDefaultAsync(m => m.IdConsumo == id);
            if (consumo == null)
            {
                return NotFound();
            }

            return View(consumo);
        }

        // POST: Consumos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var consumo = await _context.Consumos.FindAsync(id);
            if (consumo != null)
            {
                _context.Consumos.Remove(consumo);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }


        private bool ConsumoExists(int id)
        {
            return _context.Consumos.Any(e => e.IdConsumo == id);
        }

        [HttpGet]
        public async Task<IActionResult> GetResumen(DateTime? fechaInicio, DateTime? fechaFin)
        {
            var consumos = await _context.Consumos.ToListAsync();

            if (fechaInicio.HasValue && fechaFin.HasValue)
            {
                consumos = consumos
                    .Where(c => c.Fecha >= fechaInicio.Value && c.Fecha <= fechaFin.Value)
                    .ToList();
            }

            var resumen = new
            {
                totalRegistros = consumos.Count,
                totalUnidades = consumos.Sum(c => c.Unidades),
                totalGalones = consumos.Sum(c => c.GalonesConsumidos),
                operaciones = consumos.Select(c => c.Operacion).Distinct().Count(),
                graficoOperacion = consumos
                    .GroupBy(c => c.Operacion)
                    .Select(g => new { Operacion = g.Key, Galones = g.Sum(x => x.GalonesConsumidos) })
                    .ToList(),
                graficoRuta = consumos
                    .GroupBy(c => c.Ruta)
                    .Select(g => new { Ruta = g.Key, Galones = g.Sum(x => x.GalonesConsumidos) })
                    .ToList()
            };

            return Json(resumen);
        }

    }
}
