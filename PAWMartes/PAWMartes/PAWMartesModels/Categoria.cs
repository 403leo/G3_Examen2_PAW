using PAWMartes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAWMarte.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaRegistro { get; set; }
        
        public int UsuarioId { get; set; }

        // Atributos FK
        public Usuario? Usuario { get; set; }

        // Relaciones objetos
        public IEnumerable<Evento>? Eventos { get; set; }
    }
}
