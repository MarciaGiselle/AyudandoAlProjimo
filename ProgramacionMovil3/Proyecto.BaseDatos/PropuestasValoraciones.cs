//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Proyecto.BaseDatos
{
    using System;
    using System.Collections.Generic;
    
    public partial class PropuestasValoraciones
    {
        public int IdValoracion { get; set; }
        public int IdUsuario { get; set; }
        public int IdPropuesta { get; set; }
        public bool Valoracion { get; set; }
    
        public virtual Propuestas Propuestas { get; set; }
        public virtual Usuarios Usuarios { get; set; }
    }
}
