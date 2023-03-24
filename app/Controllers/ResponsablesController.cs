using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    public class ResponsablesController : Controller
    {
        private readonly TaskmanagerContext _context;

        public ResponsablesController(TaskmanagerContext context)
        {
            _context = context;
        }

        // GET: Responsables
        public async Task<IActionResult> Index()
        {
              return _context.Responsables != null ? 
                          View(await _context.Responsables.ToListAsync()) :
                          Problem("Entity set 'TaskmanagerContext.Responsables'  is null.");
        }

        // GET: Responsables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Responsables == null)
            {
                return NotFound();
            }

            var responsable = await _context.Responsables
                .FirstOrDefaultAsync(m => m.IdResponsable == id);
            if (responsable == null)
            {
                return NotFound();
            }

            return View(responsable);
        }

        // GET: Responsables/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Responsables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdResponsable,NombreResponsable,ApellidoResponsable,Telefono")] Responsable responsable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(responsable);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Responsable agregado satisfactoriamente";
                return RedirectToAction("Index", "Home");
            }
            return View(responsable);
        }

        // GET: Responsables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Responsables == null)
            {
                return NotFound();
            }

            var responsable = await _context.Responsables.FindAsync(id);
            if (responsable == null)
            {
                return NotFound();
            }
            return View(responsable);
        }

        // POST: Responsables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdResponsable,NombreResponsable,ApellidoResponsable,Telefono")] Responsable responsable)
        {
            if (id != responsable.IdResponsable)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(responsable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResponsableExists(responsable.IdResponsable))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["Message"] = "Datos del Responsable editados satisfactoriamente";
                return RedirectToAction("Index", "Home");
            }
            return View(responsable);
        }

        // GET: Responsables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Responsables == null)
            {
                return NotFound();
            }

            var responsable = await _context.Responsables
                .FirstOrDefaultAsync(m => m.IdResponsable == id);
            if (responsable == null)
            {
                return NotFound();
            }

            return View(responsable);
        }

        // POST: Responsables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Responsables == null)
            {
                return Problem("Entity set 'TaskmanagerContext.Responsables'  is null.");
            }
            var responsable = await _context.Responsables.FindAsync(id);
            if (responsable != null)
            {
                var tareasaEliminar = _context.Tareas.Where(t => t.IdResponsable == id);
                foreach (var tarea in tareasaEliminar)
                {
                    _context.Tareas.Remove(tarea);
                }
                _context.Responsables.Remove(responsable);
            }
            
            await _context.SaveChangesAsync();
            TempData["Message"] = "Responsable eliminado satisfactoriamente";
            return RedirectToAction("Index", "Home");
        }

        private bool ResponsableExists(int id)
        {
          return (_context.Responsables?.Any(e => e.IdResponsable == id)).GetValueOrDefault();
        }
    }
}
