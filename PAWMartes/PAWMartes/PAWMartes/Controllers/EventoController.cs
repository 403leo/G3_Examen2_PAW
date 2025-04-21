using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PAWMartes.Models;
using PAWMartes.Services;

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
            //ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Contraseña");
            return View();
        }

        // POST: Evento/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Descripcion,Fecha,Hora,Duracion,Ubicacion,CategoriaId,CapacidadMaxima")] Evento evento)
        {
            // Se tiene que obtener el usuario desde la sesion
            var username = HttpContext.Session.GetString("usuario");
            var usuario = _context.Usuario.FirstOrDefault(u => u.Username == username);
            if (usuario == null)
            {
                return RedirectToAction("Index", "Evento");
            }
            // Se asigna el usuario actual en la sesion
            evento.UsuarioId = usuario.Id;

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
            //ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Contraseña", evento.UsuarioId);
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
            //ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Contraseña", evento.UsuarioId);
            return View(evento);
        }

        // POST: Evento/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Descripcion,Fecha,Hora,Duracion,Ubicacion,CategoriaId,CapacidadMaxima,fechaRegistro")] Evento evento)
        {
            if (id != evento.Id)
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
            evento.UsuarioId = usuario.Id;


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
            //ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Contraseña", evento.UsuarioId);
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

        public async Task<IActionResult> Inscripcion(int eventoId)
        {
            var evento = await _context.Evento
                .Include(e => e.Asistentes)
                .FirstOrDefaultAsync(e => e.Id == eventoId);

            if (evento == null)
            {
                return NotFound();
            }

            var usuario = _context.Usuario.FirstOrDefault(u => u.Username == HttpContext.Session.GetString("usuario"));

            if (usuario == null)
            {
                return RedirectToAction("Index", "Usuario");  // Redirige al login si no está logueado
            }

            // Verificamos si el usuario ya tiene un evento en la misma fecha y hora
            var overlappingEvent = await _context.Asistente
                .Include(a => a.Evento)
                .Where(a => a.UsuarioId == usuario.Id && a.Evento.Fecha == evento.Fecha && a.Evento.Hora == evento.Hora)
                .FirstOrDefaultAsync();

            if (overlappingEvent != null)
            {
                TempData["Error"] = "Ya tienes un evento en la misma fecha y hora.";
                return RedirectToAction("Index", "Evento");
            }

            // Verificamos si el evento tiene capacidad
            if (evento.Asistentes.Count() >= evento.CapacidadMaxima)
            {
                TempData["Error"] = "El evento ya está lleno.";
                return RedirectToAction("Index", "Evento");  // Redirige si está lleno
            }

            // Registrar al usuario en el evento
            var asistente = new Asistente
            {
                UsuarioId = usuario.Id,
                EventoId = eventoId,
                FechaInscripcion = DateTime.Now,
                FechaEvento = evento.Fecha,
                Asistencia = false
            };

            _context.Add(asistente);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Te has inscrito exitosamente al evento.";
            return RedirectToAction("Index", "Evento");
        }

        public async Task<IActionResult> VerInscritos()
        {
            // Obtener el nombre del usuario logueado desde la sesión
            var username = HttpContext.Session.GetString("usuario");

            // Buscar el usuario en la base de datos
            var usuario = await _context.Usuario.FirstOrDefaultAsync(u => u.Username == username);

            if (usuario == null)
            {
                return RedirectToAction("Index", "Usuario");  // Si no hay usuario logueado, redirige al login
            }

            // Obtener los eventos en los que el usuario está inscrito
            var eventosInscritos = await _context.Evento
                .Include(e => e.Asistentes)
                .Where(e => e.Asistentes.Any(a => a.UsuarioId == usuario.Id))  // Filtrar solo los eventos del usuario
                .Include(e => e.Categoria)
                .ToListAsync();

            return View(eventosInscritos);  // Pasamos los eventos a la vista
        }

        public async Task<IActionResult> VerInscritosAdmin()
        {
            var eventos = await _context.Evento
                .Include(e => e.Asistentes)
                .ThenInclude(a => a.Usuario)
                .ToListAsync();

            return View(eventos);  // Devuelve la lista de eventos con los usuarios inscritos
        }
        public async Task<IActionResult> VerAsistentes(int eventoId)
        {
            var evento = await _context.Evento
                .Include(e => e.Asistentes)
                .ThenInclude(a => a.Usuario)
                .FirstOrDefaultAsync(e => e.Id == eventoId);

            if (evento == null)
            {
                return NotFound();  // Si no se encuentra el evento
            }

            return View(evento);  // Devuelve la información del evento y los asistentes
        }
        public async Task<IActionResult> MarcarAsistencia(int eventoId, int asistenteId)
        {
            var asistente = await _context.Asistente
                .FirstOrDefaultAsync(a => a.Id == asistenteId && a.EventoId == eventoId);

            if (asistente == null)
            {
                return NotFound();  // Si no se encuentra el asistente
            }

            // Marcar la asistencia como "Asistió"
            asistente.Asistencia = true;

            _context.Update(asistente);
            await _context.SaveChangesAsync();

            // Usamos TempData para mostrar un mensaje de éxito
            TempData["Success"] = "La asistencia ha sido marcada como 'Asistió'.";

            return RedirectToAction("VerAsistentesAdmin");  // Redirigir para recargar la página
        }
        public async Task<IActionResult> MarcarNoAsistencia(int eventoId, int asistenteId)
        {
            var asistente = await _context.Asistente
                .FirstOrDefaultAsync(a => a.Id == asistenteId && a.EventoId == eventoId);

            if (asistente == null)
            {
                return NotFound();  // Si no se encuentra el asistente
            }

            // Marcar la asistencia como "No Asistió"
            asistente.Asistencia = false;

            _context.Update(asistente);
            await _context.SaveChangesAsync();

            // Usamos TempData para mostrar un mensaje de éxito
            TempData["Success"] = "La asistencia ha sido marcada como 'No Asistió'.";

            return RedirectToAction("VerAsistentesAdmin");  // Redirigir para recargar la página
        }
        public async Task<IActionResult> VerAsistentesAdmin()
        {
            var eventos = await _context.Evento
                .Include(e => e.Asistentes)
                .ThenInclude(a => a.Usuario)
                .ToListAsync();

            return View(eventos);  // Devuelve la lista de eventos con los usuarios inscritos
        }


    }
}

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using PAWMartes.Models;
//using PAWMartes.Services;

//namespace PAWMartes.Controllers
//{
//    public class EventoController : Controller
//    {
//        private readonly IEventosServices _eventoService;

//        public EventoController(IEventosServices eventoService)
//        {
//            _eventoService = eventoService;
//        }

//        // GET: Evento
//        public async Task<IActionResult> Index()
//        {
//            var eventos = await _eventoService.obtenerTodosEventos();
//            return View(eventos);
//        }

//        // GET: Evento/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var evento = await _eventoService.obtenerEvento(id);
//            if (evento == null)
//            {
//                return NotFound();
//            }

//            return View(evento);
//        }

//        // GET: Evento/Create
//        public async Task<IActionResult> Create()
//        {
//            var categorias = await _eventoService.obtenerCategorias();
//            var usuarios = await _eventoService.obtenerUsuarios();

//            ViewData["IdCategoria"] = new SelectList(categorias, "IdCategoria", "NombreCategoria");
//            ViewData["IdUsuario"] = new SelectList(usuarios, "IdUsuario", "Nombre");

//            return View();
//        }

//        // POST: Evento/Create
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("Id,NombreEvento,DescripcionEvento,FechaEvento,IdCategoria,IdUsuario")] Evento evento)
//        {
//            if (ModelState.IsValid)
//            {
//                await _eventoService.crearEvento(evento);
//                return RedirectToAction(nameof(Index));
//            }

//            var categorias = await _eventoService.obtenerCategorias();
//            var usuarios = await _eventoService.obtenerUsuarios();

//            ViewData["IdCategoria"] = new SelectList(categorias, "IdCategoria", "NombreCategoria", evento.CategoriaId);
//            ViewData["IdUsuario"] = new SelectList(usuarios, "IdUsuario", "Nombre", evento.UsuarioId);
//            return View(evento);
//        }

//        // GET: Evento/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var evento = await _eventoService.obtenerEventoPorId(id);
//            if (evento == null)
//            {
//                return NotFound();
//            }

//            var categorias = await _eventoService.obtenerCategorias();
//            var usuarios = await _eventoService.obtenerUsuarios();

//            ViewData["IdCategoria"] = new SelectList(categorias, "IdCategoria", "NombreCategoria", evento.CategoriaId);
//            ViewData["IdUsuario"] = new SelectList(usuarios, "IdUsuario", "Nombre", evento.UsuarioId);

//            return View(evento);
//        }

//        // POST: Evento/Edit/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreEvento,DescripcionEvento,FechaEvento,IdCategoria,IdUsuario")] Evento evento)
//        {
//            if (id != evento.Id)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                var actualizado = await _eventoService.actualizarEvento(evento);
//                if (!actualizado)
//                {
//                    return NotFound();
//                }
//                return RedirectToAction(nameof(Index));
//            }

//            var categorias = await _eventoService.obtenerCategorias();
//            var usuarios = await _eventoService.obtenerUsuarios();

//            ViewData["IdCategoria"] = new SelectList(categorias, "IdCategoria", "NombreCategoria", evento.CategoriaId);
//            ViewData["IdUsuario"] = new SelectList(usuarios, "IdUsuario", "Nombre", evento.UsuarioId);

//            return View(evento);
//        }

//        // GET: Evento/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var evento = await _eventoService.obtenerEvento(id);
//            if (evento == null)
//            {
//                return NotFound();
//            }

//            return View(evento);
//        }

//        // POST: Evento/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            await _eventoService.eliminarEvento(id);
//            return RedirectToAction(nameof(Index));
//        }
//    }
//}

