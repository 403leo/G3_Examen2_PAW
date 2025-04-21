using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAWMartesModels
{
    public class DashboardViewModel
    {
        public int TotalEventos { get; set; }
        public int TotalUsuariosActivos { get; set; }
        public int AsistentesMesActual { get; set; }
        public int TotalActivos { get; set; }
        public int TotalInactivos { get; set; }
        public List<TopEventoViewModel> TopEventos { get; set; }

    }

    public class TopEventoViewModel
    {
        public string Titulo { get; set; }
        public int TotalAsistentes { get; set; }
    }
}
