using Proyecto.BaseDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.ModelView
{
    public  class VM_Home
    {
        public List<Propuestas> PropuestasActivas { get; set; }
        public List<Propuestas> PropuestasInactivas { get; set; }
        public List<Propuestas> ResultadosDelaBusqueda { get; set; }
        public List<Propuestas> PropuestasMasValoradas { get; set; }
        

    }
}
