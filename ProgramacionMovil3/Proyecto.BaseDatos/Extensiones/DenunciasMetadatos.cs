using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.BaseDatos
{
    public class DenunciasMetadatos
    {
        public int IdDenuncia { get; set; }

        public int IdPropuesta { get; set; }
        [Required(ErrorMessage = "El Motivo es Obigatorio")]
        public int IdMotivo { get; set; }
        [Required(ErrorMessage = "El Comentario es Obligatorio")]
        [Display(Name = "Comente...")]
        [StringLength(300, MinimumLength = 5, ErrorMessage = "El comentario no puede superar los 300 caracteres")]

        public string Comentarios { get; set; }
        public int IdUsuario { get; set; }
        public System.DateTime FechaCreacion { get; set; }
        public int Estado { get; set; }

        public virtual MotivoDenuncia MotivoDenuncia { get; set; }
        public virtual Propuestas Propuestas { get; set; }
        public virtual Usuarios Usuarios { get; set; }
    }
}
