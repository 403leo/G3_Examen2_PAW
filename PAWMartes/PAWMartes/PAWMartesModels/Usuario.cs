namespace PAWMartes.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Contraseña { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaRegistro { get; set; }
        // Relaciones objetos
        // Se pone un ? porque un usuario no tiene que necesariamente hacer una reserva
        public IEnumerable<Reserva>? Reservas { get; set; }

    }
}
