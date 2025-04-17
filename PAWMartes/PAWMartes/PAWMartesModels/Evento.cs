using PAWMarte.Models;

namespace PAWMartes.Models
{
    public class Evento
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime Hora { get; set; }
        public string Duracion { get; set; }
        public string Ubicacion { get; set; }
        public int CategoriaId { get; set; }
        public int CapacidadMaxima { get; set; }

        // Atributos FK
        public Categoria? Categoria { get; set; }


        // Relaciones objetos
        public IEnumerable<Asistente>? Asistentes { get; set; }

    }
}
