using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PAWMarte.Models;
using PAWMartes.Models;

namespace PAWMartes.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly PAWContext _context;

        public CategoriaController(PAWContext context)
        {
            _context = context;
        }

        // GET: Categoria
        public async Task<IActionResult> Index()
        {
            var pAWContext = _context.Categoria.Include(c => c.Usuario);
            return View(await pAWContext.ToListAsync());
        }

        // GET: Categoria/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categoria
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // GET: Categoria/Create
        public IActionResult Create()
        {
           // Se tiene que obtener el usuario actual AQUI para ponerlo automaticamente
            //ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Contraseña");
            return View();
        }

        // POST: Categoria/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,Estado")] Categoria categoria)
        {
            // Se tiene que obtener el usuario desde la sesion
            var username = HttpContext.Session.GetString("usuario");
            var usuario = _context.Usuario.FirstOrDefault(u => u.Username == username);
            if (usuario == null)
            {
                return RedirectToAction("Index", "Evento");
            }
            // Se asigna el usuario actual en la sesion
            categoria.UsuarioId = usuario.Id;

            categoria.FechaRegistro = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(categoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Contraseña", categoria.UsuarioId);
            return View(categoria);
        }

        // GET: Categoria/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var categoria = await _context.Categoria.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }
            //ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Contraseña", categoria.UsuarioId);
            return View(categoria);
        }

        // POST: Categoria/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Descripcion,Estado,FechaRegistro,UsuarioId")] Categoria categoria)
        {
            if (id != categoria.Id)
            {
                return NotFound();
            }
            // Se tiene que obtener el usuario desde la sesion
            var username = HttpContext.Session.GetString("usuario");
            var usuario = _context.Usuario.FirstOrDefault(u => u.Username == username);
            if (usuario == null)
            {
                return RedirectToAction("Index", "Evento");
            }
            // Se asigna el usuario actual en la sesion
            categoria.UsuarioId = usuario.Id;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaExists(categoria.Id))
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
            //ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Contraseña", categoria.UsuarioId);
            return View(categoria);
        }

        // GET: Categoria/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categoria
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // POST: Categoria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoria = await _context.Categoria.FindAsync(id);
            if (categoria != null)
            {
                _context.Categoria.Remove(categoria);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriaExists(int id)
        {
            return _context.Categoria.Any(e => e.Id == id);
        }
    }
}
