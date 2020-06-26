using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.BaseDatos
{
    public class UsuariosMetadatos
    {
        public int IdUsuario { get; set; }

        
        [StringLength(50, ErrorMessage = "cantidad maxima 50 carateres")]
        public string Nombre { get; set; }

        
        [StringLength(50, ErrorMessage = "cantidad maxima 50 carateres")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "ingrese una fecha valida")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime FechaNacimiento { get; set; }

        public string UserName { get; set; }

        [Required(ErrorMessage = "Ingrese un mail valido")]
        [EmailAddress]
        public string Email { get; set; }

        //[RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).{8,15}.+$")]
        [Required(ErrorMessage = "La contraseña debe tener una mayuscula, un numero y no ser menor a 8 caracteres")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

       

      
        public string Foto { get; set; }

        public int TipoUsuario { get; set; }

        public System.DateTime FechaCracion { get; set; }

        public bool Activo { get; set; }

        public string Token { get; set; }


    }


    public class CompareAttribute : ValidationAttribute
    {
        public CompareAttribute(object propertyToCompare)
        {

        }
    }
}
