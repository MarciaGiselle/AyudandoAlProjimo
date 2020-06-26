using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Proyecto.BaseDatos
{ 
     public class DonacionesMonetariasMetadatos
    {
        public string NombreParaElComprobantes
        {
            get
            {
                return string.Format("{0}{1}{2}", Convert.ToString(this.IdDonacionMonetaria) ?? "IdDonacionMonetaria", Convert.ToString(this.IdPropuestaDonacionMonetaria) ?? "IdPropuestaDonacionMonetaria", Convert.ToString(this.IdUsuario) ?? "IdUsuario");
            }

        }
        public int IdDonacionMonetaria { get; set; }
        public int IdPropuestaDonacionMonetaria { get; set; }
        public int IdUsuario { get; set; }
        [Required(ErrorMessage = "Ingrese un monto")]
        //[RegularExpression(@"^(\d{1}\.)?(\d+\.?)+(,\d{2})?$", ErrorMessage = "Maximo dos punos decimales")]
        //[Range(0, 9999999999999999.99, ErrorMessage = "Maximo 18 cifras")]
        public decimal Dinero { get; set; }
        [Required(ErrorMessage = "Es necesario el comprabante")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Nombre del archivo muy largo, menos de 200 caracteres")]
        public string ArchivoTransferencia { get; set; }
        public System.DateTime FechaCreacion { get; set; }

        public virtual PropuestasDonacionesMonetarias PropuestasDonacionesMonetarias { get; set; }
        public virtual Usuarios Usuarios { get; set; }
    }
}
