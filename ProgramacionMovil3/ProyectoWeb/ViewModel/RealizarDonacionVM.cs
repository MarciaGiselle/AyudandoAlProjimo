using Proyecto.BaseDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoWeb.ViewModel
{
    public class RealizarDonacionVM
    {
        public Propuestas Propuesta { get; set; }
        public PropuestasDonacionesMonetarias PropuestaMonetaria { get; set; }
        public PropuestasDonacionesInsumos PropuestaInsumo { get; set; }
        public PropuestasDonacionesHorasTrabajo PropuestaHoraTrabajo { get; set; }
        public DonacionesMonetarias DonacionMonetaria { get; set; }
        public DonacionesInsumos DonacionInsumo { get; set; }
        public DonacionesHorasTrabajo DonacionHoraTrabajo { get; set; }

        public List<DonacionesMonetarias> TodasDonacionesMonetarias { get; set; }

        public List<DonacionesInsumos> TodasDonacionesInsumos { get; set; }

        public List<DonacionesHorasTrabajo> TodasDonacionesHoras { get; set; }
        public RealizarDonacionVM()
        {
            PropuestaMonetaria = new PropuestasDonacionesMonetarias();
            PropuestaInsumo = new PropuestasDonacionesInsumos();
            PropuestaHoraTrabajo = new PropuestasDonacionesHorasTrabajo();
            DonacionMonetaria = new DonacionesMonetarias();
            DonacionInsumo = new DonacionesInsumos();
            DonacionHoraTrabajo = new DonacionesHorasTrabajo();
            TodasDonacionesMonetarias = new List<DonacionesMonetarias>();
            TodasDonacionesInsumos = new List<DonacionesInsumos>();
            TodasDonacionesHoras = new List<DonacionesHorasTrabajo>();
        }
    }
}