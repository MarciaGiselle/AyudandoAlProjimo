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
    
    public partial class PropuestasDonacionesMonetarias
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PropuestasDonacionesMonetarias()
        {
            this.DonacionesMonetarias = new HashSet<DonacionesMonetarias>();
        }
    
        public int IdPropuestaDonacionMonetaria { get; set; }
        public int IdPropuesta { get; set; }
        public decimal Dinero { get; set; }
        public string CBU { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonacionesMonetarias> DonacionesMonetarias { get; set; }
        public virtual Propuestas Propuestas { get; set; }
    }
}
