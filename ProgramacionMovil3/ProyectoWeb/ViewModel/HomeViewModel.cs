using Proyecto.BaseDatos;
using System.Collections.Generic;

namespace ProyectoWeb.ViewModel
{
    public class HomeViewModel
    {
        public Usuarios Usuario { get; set; }
        public List<Propuestas> Listado5PropuestasDestacadas { get; set; }
        public List<Propuestas> MisPropuestas { get; set; }
        public List<Propuestas> PropuestasGeneral { get; set; }
        public HomeViewModel()
        {
            Usuario = new Usuarios();
            MisPropuestas = new List<Propuestas>();
            PropuestasGeneral = new List<Propuestas>();
        }
    }
}