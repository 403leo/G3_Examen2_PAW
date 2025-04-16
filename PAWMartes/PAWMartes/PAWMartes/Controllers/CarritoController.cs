using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PAWMartes.Migrations;
using PAWMartes.Models;
using PAWMartes.Services;

namespace PAWMartes.Controllers
{
    public class CarritoController : Controller
    {
        private readonly PAWContext _context;
        private readonly ICarritoServices _carrito;
        public CarritoController(PAWContext context, ICarritoServices carrito)
        {
            _context = context;
            _carrito = carrito;
        }
        public IActionResult Index()
        {
            var Items = _carrito.ObtenerItems();
			return View(Items);
        }

        [HttpPost]
        public async Task<IActionResult> AgregarCarrito(int eventoId, int cantidad)
        {
            // Se busca el evento por id
            var evento = await _context.Evento.FindAsync(eventoId);
            // Si no existe el evento, se redirige a la vista de error
            if (evento != null)
            {
                // Se agrega el evento al carrito
                _carrito.AgregarItem(evento, cantidad);
            }

            // Se redirige a la vista del carrito
            return RedirectToAction("Index", "Eventos");
        }

        [HttpPost]
        public async Task<IActionResult> FinalizarCompra() 
        { 
            var Items = _carrito.ObtenerItems();
            using var transaccion = await _context.Database.BeginTransactionAsync();

            try
            {
                foreach (var item in Items)
                {
					var reservasDB = await _context.Reserva.Where(x => x.EventoId == item.evento.Id).SumAsync(x => x.Cantidad);
                    if(reservasDB + item.Cantidad > item.evento.CapacidadMaxima)
                    {
                        throw new InvalidOperationException($"No hay suficiente capacidad para {item.evento.Nombre}");

					}

                    var reserva = new Reserva
					{
                        Fecha = DateTime.Now,
                        Estado = true,
                        Cantidad = item.Cantidad,
						EventoId = item.evento.Id,
						UsuarioId = 1, // Cambiar por el id del usuario logueado
						
					};

                    _context.Reserva.Add(reserva);
				}
				await _context.SaveChangesAsync();
				await transaccion.CommitAsync();

				// Limpiar el carrito
				_carrito.LimpiarCarrito();

				return RedirectToAction("Confirmacion");
			}
            catch (Exception ex)
            {
                await transaccion.RollbackAsync();
                return View("Index");

			}


		}

		public IActionResult Confirmacion()
		{
			return View();
		}

		//public IActionResult EliminarItem(int eventoId)
		//{
		//	var evento = _context.Evento.Find(eventoId);
		//	if (evento != null)
		//	{
		//		_carrito.EliminarItem(evento);
		//	}
		//	return RedirectToAction("Index");
		//}
	}
}
