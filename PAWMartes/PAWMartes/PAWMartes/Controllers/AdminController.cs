using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PAWMartes.Models;
using PAWMartes.ViewModels;


namespace PAWMartes.Controllers
{

    public class AdminController : Controller
    {

        private readonly PAWContext _context;

        public AdminController(PAWContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var mesActual = DateTime.Now.Month;
            var anioActual = DateTime.Now.Year;

            var totalEventos = await _context.Evento.CountAsync();

            // Contar los asistentes y no asistentes este mes
            var asistentesEsteMes = await _context.Asistente
                .Where(a => a.FechaInscripcion.Month == mesActual && a.FechaInscripcion.Year == anioActual)
                .GroupBy(a => a.Asistencia)  // Asumimos que 'Asistio' es un booleano o enumeración
                .Select(g => new
                {
                    Asistio = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();

            var asistentesCount = asistentesEsteMes.FirstOrDefault(a => a.Asistio == true)?.Count ?? 0; // Asistieron
            var noAsistentesCount = asistentesEsteMes.FirstOrDefault(a => a.Asistio == false)?.Count ?? 0; // No asistieron

            var totalUsuariosActivos = await _context.Usuario.CountAsync(u => u.IsActive);
            var totalUsuariosInactivos = await _context.Usuario.CountAsync(u => !u.IsActive);

            var topEventos = await _context.Evento
                .Select(e => new TopEventoViewModel
                {
                    Titulo = e.Titulo,
                    TotalAsistentes = e.Asistentes.Count(),
                })
                .OrderByDescending(e => e.TotalAsistentes)
                .Take(5)
                .ToListAsync();

            var vm = new DashboardViewModel
            {
                TotalEventos = totalEventos,
                TotalUsuariosActivos = totalUsuariosActivos,
                TotalUsuariosInactivos = totalUsuariosInactivos,
                AsistentesMesActual = asistentesCount,  // Asistentes este mes
                NoAsistentesMesActual = noAsistentesCount,  // No asistieron este mes
                TopEventos = topEventos
            };

            return View(vm);
        }

    }
}

