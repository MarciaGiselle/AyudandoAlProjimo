using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto.BaseDatos
{
    public class PropuestasMetadatos
    {
        [Display(Name = "Fecha de Creación")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaCreacion { get; set; }

        [Required(ErrorMessage= "Ingrese un título.")]
        [Display(Name="Título")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Ingrese un título entre 5 y 50 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Ingrese una descripción.")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Display(Name = "Fecha de Fin")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "Ingrese una fecha de fin.")]
        public System.DateTime FechaFin { get; set; }


        [Required(ErrorMessage = "Ingrese un téléfono de contacto.")]
        [Display(Name = "Teléfono de contacto")]
        [Phone(ErrorMessage = "Formato de teléfono no válido.")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "El teléfono debe tener entre 6 y 30 caracteres.")]
        public string TelefonoContacto { get; set; }


        [Required(ErrorMessage = "Seleccione un tipo de donación.")]
        [Display(Name = "Tipo de donación")]
        public int TipoDonacion { get; set; }

        [Required(ErrorMessage = "Seleccione una imagen.")]
        [Display(Name = "Foto")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Nombre de imagen demasiado extenso. Hasta 100 caracteres.")]
        /*[RegularExpression(@"^.*\.(jpg|JPG|jpeg|JPEG|png|PNG)$", ErrorMessage = "Debe ser de extensión .jpg, .jpeg o .png")]*/
        public string Foto { get; set; }
    }
}
