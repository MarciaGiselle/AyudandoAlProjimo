using Proyecto.BaseDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.ModelView
{
    public class DenunciarPropuestaMV
    {
        public Denuncias Denuncia { get; set; }

        public Propuestas Propuesta { get; set; }

        public List<MotivoDenuncia> Motivos { get; set; }

        public DenunciarPropuestaMV()
        {
            Denuncia = new Denuncias();
        }
    }
}
