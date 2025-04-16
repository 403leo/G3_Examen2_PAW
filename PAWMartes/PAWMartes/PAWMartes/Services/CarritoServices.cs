using PAWMartes.Models;

namespace PAWMartes.Services
{
    public class CarritoServices : ICarritoServices
    {
        private readonly List<CarritoItem> _items = new List<CarritoItem>();

        //public CarritoServices()
        //{
        //    _items = new List<CarritoItem>();
        //}

        public void AgregarItem(Evento evento, int Cantidad)
        {
            // Verifica que el evento no exista en el carrito
            var existeItem = _items.FirstOrDefault(i => i.evento.Id == evento.Id);
            // Si existe, incrementa la cantidad
            if (existeItem != null)
            {
                // Se la va a sumar a la cantidad que se esta pasando
                existeItem.Cantidad += Cantidad ;
            }
            // En caso de que no exista, lo agrega
            else
            {
                // Se crea un nuevo CarritoItem
                _items.Add(new CarritoItem { evento = evento, Cantidad = Cantidad });
            }
        }

        public void EliminarItem(Evento evento)
        {
            // Todos los eventos que sean iguales al que me estan pasando se eliminan
            _items.RemoveAll(i => i.evento.Id == evento.Id);
        }
        public void LimpiarCarrito()
        {
            _items.Clear();
        }

        public IEnumerable<CarritoItem> ObtenerItems()
        {
            return _items;
        }

        

        public decimal ObtenerTotal()
        {
            var total = _items.Sum(i => i.Cantidad);
            return total;
        }
    }
}

