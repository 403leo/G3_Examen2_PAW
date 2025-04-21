using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PAWMartes.Models;

namespace PAWMartesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        // Contexto de la base de datos
        private readonly PAWContext _context;
        // Constructor
        public EventoController(PAWContext context)
        {
            _context = context;
        }


        // i. GET: /api/events - Lista de eventos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Evento>>> GetAllEventos()
        {
            var eventos = await _context.Evento
                .Select(e => new {
                    e.Titulo,
                    e.Fecha,
                    e.Hora,
                    e.CapacidadMaxima,
                    e.Ubicacion
                })
                .ToListAsync();

            return Ok(eventos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventoById(int id)
        {
            var evento = await _context.Evento
                .Where(e => e.Id == id)
                .Select(e => new {
                    e.Titulo,
                    e.Fecha,
                    e.Hora,
                    e.Ubicacion,
                    e.CapacidadMaxima,
                    e.Descripcion
                })
                .FirstOrDefaultAsync();

            if (evento == null)
            {
                return NotFound();
            }

            return Ok(evento);
        }




    }
}
