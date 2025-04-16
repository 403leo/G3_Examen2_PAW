using Microsoft.EntityFrameworkCore;
using PAWMartes.Models;

namespace PAWMartes.Services
{
	public interface IEventosServices
	{
		Task<bool> guardar(Evento evento);
		Task<bool> modificar(Evento evento);
		Task<IEnumerable<Evento>> listado();
		Task<bool> eliminar(int id);
		Task<Evento> obtenerEvento(int? id);
		Task<bool> ExiteEvento(int id);
    }
}
