using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoWeb.Utilidades
{
    public class UDropDownList
    {
        public static IEnumerable<SelectListItem> GetProfesiones(object selectedValue)
        {
            return new List<SelectListItem>
        {
                                                 
            new SelectListItem{ Text="Plomero", Value = "1", Selected = "1" == selectedValue.ToString()},
            new SelectListItem{ Text="Electricista", Value = "2", Selected = "2" == selectedValue.ToString()},
            new SelectListItem{ Text="Abogado", Value = "3", Selected = "3" == selectedValue.ToString()},
            new SelectListItem{ Text="Contador", Value = "4", Selected = "4" == selectedValue.ToString()},
            new SelectListItem{ Text="Psicóloga", Value = "5", Selected = "5" == selectedValue.ToString()},
            new SelectListItem{ Text="Psicopedagoga", Value = "6", Selected = "6" == selectedValue.ToString()},
            new SelectListItem{ Text="Trabajador Social", Value = "7", Selected = "7" == selectedValue.ToString()},
            new SelectListItem{ Text="Cocinero", Value = "8", Selected = "8" == selectedValue.ToString()}, 
            new SelectListItem{ Text="Cocinero", Value = "9", Selected = "9" == selectedValue.ToString()},
            new SelectListItem{ Text="Otros", Value = "10", Selected = "10" == selectedValue.ToString()},

        };
        }
        public static String GetNombreSelecccionado(object selectValue)
        {
            string nombre;
            foreach (SelectListItem item in GetProfesiones(selectValue))
            {
                if (item.Value.ToString().Equals(selectValue.ToString()))
                {
                    nombre = item.Text;
                    return nombre;
                }

            }
            return null;
        }
    }
}