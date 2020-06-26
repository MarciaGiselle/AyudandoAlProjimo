using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Proyecto.BaseDatos
{
    public class DonacionesInsumosMetadatos
    {
        public int IdDonacionInsumo { get; set; }
        public int IdPropuestaDonacionInsumo { get; set; }
        public int IdUsuario { get; set; }
        [Required(ErrorMessage = "Ingrese Cantidad")]
        public int Cantidad { get; set; }

        public virtual PropuestasDonacionesInsumos PropuestasDonacionesInsumos { get; set; }
        public virtual Usuarios Usuarios { get; set; }
    }
}
