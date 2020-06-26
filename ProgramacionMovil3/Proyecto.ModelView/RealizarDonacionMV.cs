using Proyecto.BaseDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.ModelView
{
    public class RealizarDonacionMV
    {
        public Propuestas Propuesta { get; set; }
        public PropuestasDonacionesMonetarias PropuestaMonetaria { get; set; }
        public List<PropuestasDonacionesInsumos> ListaPropuestaInsumo { get; set; }
        public List<PropuestasDonacionesHorasTrabajo> ListaPropuestaHoraTrabajo { get; set; }
        public DonacionesMonetariasMetadatos DonacionMonetaria { get; set; }
        public DonacionesInsumos DonacionInsumo { get; set; }
        public DonacionesHorasTrabajoMetadatos DonacionHoraTrabajo { get; set; }

        public List<DonacionesMonetarias> TodasDonacionesMonetarias { get; set; }

        public List<DonacionesInsumosMetadatos> TodasDonacionesInsumos { get; set; }

        public List<DonacionesHorasTrabajo> TodasDonacionesHoras { get; set; }
      
        public RealizarDonacionMV()
        {
            PropuestaMonetaria = new PropuestasDonacionesMonetarias();
            ListaPropuestaInsumo = new List<PropuestasDonacionesInsumos>();
            ListaPropuestaHoraTrabajo = new List<PropuestasDonacionesHorasTrabajo>();
            DonacionMonetaria = new DonacionesMonetariasMetadatos();
            DonacionInsumo = new DonacionesInsumos();
            DonacionHoraTrabajo = new DonacionesHorasTrabajoMetadatos();
            TodasDonacionesMonetarias = new List<DonacionesMonetarias>();
            TodasDonacionesInsumos = new List<DonacionesInsumosMetadatos>();
            TodasDonacionesHoras = new List<DonacionesHorasTrabajo>();
            
        }
    }
}
