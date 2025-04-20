using Microsoft.EntityFrameworkCore;
using PAWMarte.Models;
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

		//Task<List<Evento>> obtenerTodosEventos();
  //      Task<Evento> obtenerEvento(int? id);
  //      Task<Evento> obtenerEventoPorId(int? id);
  //      Task crearEvento(Evento evento);
  //      Task<bool> actualizarEvento(Evento evento);
  //      Task eliminarEvento(int id);
  //      Task<List<Categoria>> obtenerCategorias();
  //      Task<List<Usuario>> obtenerUsuarios();



    }
}
