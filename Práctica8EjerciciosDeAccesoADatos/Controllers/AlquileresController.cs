using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Práctica8EjerciciosDeAccesoADatos.Models;

namespace Práctica8EjerciciosDeAccesoADatos.Controllers
{
    public class AlquileresController : Controller
    {
        private readonly Prg3EfPr1Context _context;

        public AlquileresController(Prg3EfPr1Context context)
        {
            _context = context;
        }

        // GET: Alquileres
        public async Task<IActionResult> Index()
        {
            var prg3EfPr1Context = _context.Alquileres
                .Include(a => a.IdClienteNavigation)
                .Include(a => a.IdCopiaNavigation)
                .ThenInclude(c => c.IdPeliculaNavigation);
            return View(await prg3EfPr1Context.ToListAsync());
        }

        // GET: Alquileres/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alquilere = await _context.Alquileres
                .Include(a => a.IdClienteNavigation)
                .Include(a => a.IdCopiaNavigation)
                .ThenInclude(c => c.IdPeliculaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alquilere == null)
            {
                return NotFound();
            }

            return View(alquilere);
        }

        // GET: Alquileres/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Nombre");
            ViewData["IdCopia"] = new SelectList(_context.Copias
                .Include(c => c.IdPeliculaNavigation)
                .Where(c => !c.Deteriorada.HasValue || !c.Deteriorada.Value)
                .Where(c => !c.Alquileres.Any(a => a.FechaEntregada == null) && c.IdPeliculaNavigation != null), "Id", "IdPeliculaNavigation.Titulo");
            return View();
        }

        // POST: Alquileres/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdCopia,IdCliente")] Alquilere alquilere)
        {
            if (ModelState.IsValid)
            {
                var copiaDisponible = await _context.Copias
                    .Include(c => c.Alquileres)
                    .FirstOrDefaultAsync(c => c.Id == alquilere.IdCopia && (!c.Deteriorada.HasValue || !c.Deteriorada.Value) && !c.Alquileres.Any(a => a.FechaEntregada == null));

                if (copiaDisponible == null)
                {
                    ModelState.AddModelError("", "La copia seleccionada no está disponible para alquilar.");
                }
                else
                {
                    alquilere.FechaAlquiler = DateTime.Now;
                    alquilere.FechaTope = DateTime.Now.AddDays(3);

                    _context.Add(alquilere);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Nombre", alquilere.IdCliente);
            ViewData["IdCopia"] = new SelectList(_context.Copias
                .Include(c => c.IdPeliculaNavigation)
                .Where(c => !c.Deteriorada.HasValue || !c.Deteriorada.Value)
                .Where(c => !c.Alquileres.Any(a => a.FechaEntregada == null) && c.IdPeliculaNavigation != null), "Id", "IdPeliculaNavigation.Titulo", alquilere.IdCopia);
            return View(alquilere);
        }

        // GET: Alquileres/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alquilere = await _context.Alquileres.FindAsync(id);
            if (alquilere == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Nombre", alquilere.IdCliente);
            ViewData["IdCopia"] = new SelectList(_context.Copias.Include(c => c.IdPeliculaNavigation).Where(c => !c.Deteriorada.HasValue || !c.Deteriorada.Value).Where(c => c.IdPeliculaNavigation != null), "Id", "IdPeliculaNavigation.Titulo", alquilere.IdCopia);
            return View(alquilere);
        }

        // POST: Alquileres/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,IdCopia,IdCliente,FechaAlquiler,FechaTope,FechaEntregada")] Alquilere alquilere)
        {
            if (id != alquilere.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alquilere);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlquilereExists(alquilere.Id))
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
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Nombre", alquilere.IdCliente);
            ViewData["IdCopia"] = new SelectList(_context.Copias.Include(c => c.IdPeliculaNavigation).Where(c => !c.Deteriorada.HasValue || !c.Deteriorada.Value).Where(c => c.IdPeliculaNavigation != null), "Id", "IdPeliculaNavigation.Titulo", alquilere.IdCopia);
            return View(alquilere);
        }

        // GET: Alquileres/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alquilere = await _context.Alquileres
                .Include(a => a.IdClienteNavigation)
                .Include(a => a.IdCopiaNavigation)
                .ThenInclude(c => c.IdPeliculaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alquilere == null)
            {
                return NotFound();
            }

            return View(alquilere);
        }

        // POST: Alquileres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var alquilere = await _context.Alquileres.FindAsync(id);
            if (alquilere != null)
            {
                _context.Alquileres.Remove(alquilere);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlquilereExists(long id)
        {
            return _context.Alquileres.Any(e => e.Id == id);
        }

        // GET: Alquileres/MarcarEntregado/5
        public async Task<IActionResult> MarcarEntregado(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alquilere = await _context.Alquileres
                .Include(a => a.IdClienteNavigation)
                .Include(a => a.IdCopiaNavigation)
                .ThenInclude(c => c.IdPeliculaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alquilere == null)
            {
                return NotFound();
            }

            return View(alquilere);
        }

        // POST: Alquileres/MarcarEntregado/5
        [HttpPost, ActionName("MarcarEntregado")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarcarEntregadoConfirmed(long id)
        {
            var alquilere = await _context.Alquileres.FindAsync(id);
            if (alquilere == null)
            {
                return NotFound();
            }

            alquilere.FechaEntregada = DateTime.Now;

            try
            {
                _context.Update(alquilere);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlquilereExists(alquilere.Id))
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
    }
}
