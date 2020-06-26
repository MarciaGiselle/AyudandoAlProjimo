using Proyecto.BaseDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoWeb.ViewModel
{
    public class DenunciarPropuestaVM
    {
        public Denuncias Denuncia { get; set; }

        public Propuestas Propuesta { get; set; }

        public List<MotivoDenuncia> Motivos { get; set; }

        public DenunciarPropuestaVM()
        {
            Denuncia = new Denuncias();
        }

    }
}