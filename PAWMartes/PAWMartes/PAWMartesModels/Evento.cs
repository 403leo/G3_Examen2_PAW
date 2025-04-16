namespace PAWMartes.Models
{
    public class Evento
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateOnly Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public string Lugar { get; set; }
        public int CapacidadMaxima { get; set; }

        // Relaciones objetos
        public IEnumerable<Reserva>? Reservas { get; set; }

    }
}
