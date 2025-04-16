using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAWMartes.Models
{
    public class Asistente
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; } // FK a Usuario
        public int EventoId { get; set; } // FK a Evento
        public bool Asistencia { get; set; } // Marca si asistió o no
        public DateTime FechaInscripcion { get; set; }
        public DateTime FechaEvento { get; set; }

        // Navegación
        public Usuario? Usuario { get; set; }
        public Evento? Evento { get; set; }
    }
}
