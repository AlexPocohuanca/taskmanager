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
    public class TareasController : Controller
    {
        private readonly TaskmanagerContext _context;

        public TareasController(TaskmanagerContext context)
        {
            _context = context;
        }

        // GET: Tareas
        public async Task<IActionResult> Index()
        {
            var taskmanagerContext = _context.Tareas.Include(t => t.IdListaNavigation).Include(t => t.IdResponsableNavigation);
            return View(await taskmanagerContext.ToListAsync());
        }

        // GET: Tareas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tareas == null)
            {
                return NotFound();
            }

            var tarea = await _context.Tareas
                .Include(t => t.IdListaNavigation)
                .Include(t => t.IdResponsableNavigation)
                .FirstOrDefaultAsync(m => m.IdTarea == id);
            if (tarea == null)
            {
                return NotFound();
            }

            return View(tarea);
        }

        // GET: Tareas/Create
        public IActionResult Create()
        {
            ViewData["IdLista"] = new SelectList(_context.Lista, "IdLista", "NombreLista");
            ViewData["IdResponsable"] = new SelectList(_context.Responsables, "IdResponsable", "NombreCompletoResponsable");
            return View();
        }

        // POST: Tareas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTarea,IdLista,NombreTarea,DescTarea,Dificultad,IdResponsable,Tiempo")] Tarea tarea)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tarea);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Tarea agregada satisfactoriamente";
                return RedirectToAction("Index", "Home");
            }
            
            ViewData["IdLista"] = new SelectList(_context.Lista, "IdLista", "IdLista", tarea.IdLista);
            ViewData["IdResponsable"] = new SelectList(_context.Responsables, "IdResponsable", "IdResponsable", tarea.IdResponsable);
            return View(tarea);
        }

        // GET: Tareas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tareas == null)
            {
                return NotFound();
            }

            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea == null)
            {
                return NotFound();
            }
            ViewData["IdLista"] = new SelectList(_context.Lista, "IdLista", "NombreLista");
            ViewData["IdResponsable"] = new SelectList(_context.Responsables, "IdResponsable", "NombreCompletoResponsable");
            return View(tarea);
        }

        // POST: Tareas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTarea,IdLista,NombreTarea,DescTarea,Dificultad,IdResponsable,Tiempo")] Tarea tarea)
        {
            if (id != tarea.IdTarea)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tarea);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TareaExists(tarea.IdTarea))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["Message"] = "Tarea editada satisfactoriamente";
                return RedirectToAction("Index", "Home");
            }
            ViewData["IdLista"] = new SelectList(_context.Lista, "IdLista", "IdLista", tarea.IdLista);
            ViewData["IdResponsable"] = new SelectList(_context.Responsables, "IdResponsable", "IdResponsable", tarea.IdResponsable);
            return View(tarea);
        }

        // GET: Tareas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tareas == null)
            {
                return NotFound();
            }

            var tarea = await _context.Tareas
                .Include(t => t.IdListaNavigation)
                .Include(t => t.IdResponsableNavigation)
                .FirstOrDefaultAsync(m => m.IdTarea == id);
            if (tarea == null)
            {
                return NotFound();
            }

            return View(tarea);
        }

        // POST: Tareas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tareas == null)
            {
                return Problem("Entity set 'TaskmanagerContext.Tareas'  is null.");
            }
            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea != null)
            {
                _context.Tareas.Remove(tarea);
            }
            
            await _context.SaveChangesAsync();
            TempData["Message"] = "Tarea eliminada satisfactoriamente";
            return RedirectToAction("Index", "Home");
        }

        private bool TareaExists(int id)
        {
          return (_context.Tareas?.Any(e => e.IdTarea == id)).GetValueOrDefault();
        }
    }
}
