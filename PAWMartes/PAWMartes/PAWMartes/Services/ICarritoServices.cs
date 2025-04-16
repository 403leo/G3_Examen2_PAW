using PAWMartes.Models;

namespace PAWMartes.Services
{
    public interface ICarritoServices
    {
        void AgregarItem(Evento evento, int Cantidad);
        void EliminarItem(Evento evento);
        IEnumerable<CarritoItem> ObtenerItems();
        void LimpiarCarrito();
        decimal ObtenerTotal();

    }
}
