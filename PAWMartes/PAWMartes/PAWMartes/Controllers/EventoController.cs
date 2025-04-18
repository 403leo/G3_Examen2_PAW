using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PAWMartes.Models;

namespace PAWMartes.Controllers
{
    public class EventoController : Controller
    {
        private readonly PAWContext _context;

        public EventoController(PAWContext context)
        {
            _context = context;
        }

        // GET: Evento
        public async Task<IActionResult> Index()
        {
            var pAWContext = _context.Evento.Include(e => e.Categoria).Include(e => e.Usuario);
            return View(await pAWContext.ToListAsync());
        }

        // GET: Evento/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Evento
                .Include(e => e.Categoria)
                .Include(e => e.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // GET: Evento/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "Id", "Descripcion");
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Contraseña");
            return View();
        }

        // POST: Evento/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Descripcion,Fecha,Hora,Duracion,Ubicacion,CategoriaId,UsuarioId,CapacidadMaxima")] Evento evento)
        {
            // Validación: La fecha no debe ser en el pasado
            if (evento.Fecha < DateTime.Now.Date)
            {
                ModelState.AddModelError("Fecha", "La fecha no puede ser en el pasado.");
            }

            // Validación: La duración debe ser positiva
            if (evento.Duracion <= 0)
            {
                ModelState.AddModelError("Duracion", "La duración debe ser un valor positivo.");
            }

            // Validación: El cupo máximo debe ser mayor a 0
            if (evento.CapacidadMaxima <= 0)
            {
                ModelState.AddModelError("CapacidadMaxima", "El cupo máximo debe ser mayor a 0.");
            }
            evento.fechaRegistro = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(evento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "Id", "Descripcion", evento.CategoriaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Contraseña", evento.UsuarioId);
            return View(evento);
        }

        // GET: Evento/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Evento.FindAsync(id);
            if (evento == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "Id", "Descripcion", evento.CategoriaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Contraseña", evento.UsuarioId);
            return View(evento);
        }

        // POST: Evento/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Descripcion,Fecha,Hora,Duracion,Ubicacion,CategoriaId,UsuarioId,CapacidadMaxima,fechaRegistro")] Evento evento)
        {
            if (id != evento.Id)
            {
                return NotFound();
            }
            // Validación: La fecha no debe ser en el pasado
            if (evento.Fecha < DateTime.Now.Date)
            {
                ModelState.AddModelError("Fecha", "La fecha no puede ser en el pasado.");
            }

            // Validación: La duración debe ser positiva
            if (evento.Duracion <= 0)
            {
                ModelState.AddModelError("Duracion", "La duración debe ser un valor positivo.");
            }

            // Validación: El cupo máximo debe ser mayor a 0
            if (evento.CapacidadMaxima <= 0)
            {
                ModelState.AddModelError("CapacidadMaxima", "El cupo máximo debe ser mayor a 0.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(evento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventoExists(evento.Id))
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
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "Id", "Descripcion", evento.CategoriaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Contraseña", evento.UsuarioId);
            return View(evento);
        }

        // GET: Evento/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Evento
                .Include(e => e.Categoria)
                .Include(e => e.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // POST: Evento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var evento = await _context.Evento.FindAsync(id);
            if (evento != null)
            {
                _context.Evento.Remove(evento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventoExists(int id)
        {
            return _context.Evento.Any(e => e.Id == id);
        }
    }
}
