using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Proyecto.BaseDatos
{
    [MetadataType(typeof(DonacionesMonetariasMetadatos))]
    public partial class DonacionesMonetarias
    {
        
        public string NombreParaElComprobantes 
        {
            get
            {
                return string.Format("{0}{1}{2}", Convert.ToString(this.IdDonacionMonetaria) ?? "IdDonacionMonetaria", Convert.ToString(this.IdPropuestaDonacionMonetaria) ?? "IdPropuestaDonacionMonetaria", Convert.ToString(this.IdUsuario) ?? "IdUsuario");
            }
            
        }
        
    }
}
