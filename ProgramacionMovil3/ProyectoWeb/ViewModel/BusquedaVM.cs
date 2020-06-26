using Proyecto.BaseDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoWeb.ViewModel
{
    public class BusquedaVM
    {
        public List<Propuestas> ListaResultadoBusqueda { get; set; }

        public BusquedaVM()
        {
            ListaResultadoBusqueda = new List<Propuestas>();
        }
    }
}