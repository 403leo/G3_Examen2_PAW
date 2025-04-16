namespace PAWMartes.Models
{
    public class Reserva
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public bool Estado { get; set; }
        public int EventoId { get; set; }
        public int UsuarioId { get; set; }
        public int Cantidad { get; set; } // Cantidad de entradas que se reservan



		// Relaciones de la tablas 
		// Modelos de Navegacion 

		public Evento? Evento { get; set; }
        public Usuario? Usuario { get; set; }


    }
}
