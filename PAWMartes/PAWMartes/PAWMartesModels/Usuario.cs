using PAWMarte.Models;

namespace PAWMartes.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Contraseña { get; set; }
        public string Rol { get; set; }
        // Relaciones objetos
        // Se pone un ? porque un usuario no tiene que necesariamente hacer una categoria
        public IEnumerable<Categoria>? Categorias { get; set; }
        public IEnumerable<Asistente>? Asistentes { get; set; }
        public IEnumerable<Evento>? Eventos { get; set; }

    }
}
