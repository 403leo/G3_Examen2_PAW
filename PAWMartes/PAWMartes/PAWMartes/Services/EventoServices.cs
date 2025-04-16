using Microsoft.EntityFrameworkCore;
using PAWMartes.Models;

namespace PAWMartes.Services
{
	public class EventoServices : IEventosServices
	{

		// Llamar a la base de datos
		private readonly PAWContext _context;
		public EventoServices(PAWContext context) {

			_context = context;

		}
		public async Task<bool> eliminar(int id)
		{
			try
			{
				var evento = await obtenerEvento(id);
				if (evento != null) {
					_context.Evento.Remove(evento);

				}

				await _context.SaveChangesAsync();
				return true;

			}
			catch (Exception ex) {
				return false;
			}
		}

		public async Task<bool> guardar(Evento evento)
		{
			try
			{
				_context.Add(evento);
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex) {
				return false;
			}
		}

		public async Task<IEnumerable<Evento>> listado()
		{
			var lista = await _context.Evento.ToListAsync();
			return lista;
		}

		public async Task<bool> modificar(Evento evento)
		{
			try
			{
				_context.Update(evento);
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public async Task<Evento> obtenerEvento(int? id)
		{
			var evento = await _context.Evento.FindAsync(id);
			return evento;
		}

		public async Task<bool> ExiteEvento(int id)
		{
			return await _context.Evento.AnyAsync(e => e .Id == id);
		}
	}
}
