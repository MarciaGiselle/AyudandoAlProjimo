using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.ModelView
{
    public class DonacionesMV
    {
        public DateTime? Fecha;
        public string Nombre { get; set; }
        public string TipoDonacion { get; set; }

        public string Estado { get; set; }
        public double TotalRecaudado { get; set; }
        public double MiDonacion { get; set; }
        public int IdPropuesta { get; set; }

    }
}
