using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.BaseDatos
{
    [MetadataType(typeof(PropuestasMetadatos))]

    public partial class Propuestas
    {
        public string NombreSignificativoImagen
        {
            get
            {
                //en caso de ambos null, devuelve "Apellido"
                return string.Format("{0}", this.Nombre ?? "Nombre");
            }
        }

      
    }
}
