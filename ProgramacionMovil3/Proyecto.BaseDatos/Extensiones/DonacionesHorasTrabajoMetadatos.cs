using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Proyecto.BaseDatos
{
    public class DonacionesHorasTrabajoMetadatos
    {
        public int IdDonacionHorasTrabajo { get; set; }
        public int IdPropuestaDonacionHorasTrabajo { get; set; }
        public int IdUsuario { get; set; }
        [Required(ErrorMessage = "Ingrese Cantidad")]
        public int Cantidad { get; set; }

        public virtual PropuestasDonacionesHorasTrabajo PropuestasDonacionesHorasTrabajo { get; set; }
        public virtual Usuarios Usuarios { get; set; }
    }
}
