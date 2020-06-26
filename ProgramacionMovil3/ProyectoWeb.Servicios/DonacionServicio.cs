using Proyecto.BaseDatos;
using Proyecto.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace ProyectoWeb.Servicios
{
    public class DonacionServicio
    {
        Entities db = new Entities();
        PropuestaServicio SerProp = new PropuestaServicio();

        public RealizarDonacionMV ModelViewDonacionIDPropuesta(int id)
        {
            RealizarDonacionMV model = new RealizarDonacionMV();
            model.Propuesta = SerProp.ObtenerPorId(id);
            if (model.Propuesta == null)
            {
                return null;
            }
            if (model.Propuesta.TipoDonacion == 3)
            {
                model.Propuesta.PropuestasDonacionesHorasTrabajo.First().DonacionesHorasTrabajo = this.TodasDonacionesHoras(model.Propuesta.PropuestasDonacionesHorasTrabajo.First().IdPropuesta);
            }
            if (model.Propuesta.TipoDonacion == 2)
            {
                model.Propuesta.PropuestasDonacionesInsumos.Clear();
                model.Propuesta.PropuestasDonacionesInsumos = db.PropuestasDonacionesInsumos.Include("DonacionesInsumos").Where(a => a.IdPropuesta == 3).ToList();
            }
            int conut = model.Propuesta.PropuestasDonacionesInsumos.Count();
            for (int i = 0; i < conut; i++)
            {
                model.TodasDonacionesInsumos.Add(new DonacionesInsumosMetadatos());
            }
            if (model.Propuesta.TipoDonacion == 1)
            {
                model.Propuesta.PropuestasDonacionesMonetarias.First().DonacionesMonetarias.Clear();
                model.Propuesta.PropuestasDonacionesMonetarias.First().DonacionesMonetarias = this.DonacionesMonetariasPorIDPropMonet(model.Propuesta.PropuestasDonacionesMonetarias.First().IdPropuestaDonacionMonetaria);
            }

            return model;

        }
        public void GuardarDonacionmonetaria(RealizarDonacionMV model)
        {

            model.DonacionMonetaria.FechaCreacion = DateTime.Now;
            //id usuario hardcodeada
            
            DonacionesMonetarias miDona = new DonacionesMonetarias();
            miDona.ArchivoTransferencia = model.DonacionMonetaria.ArchivoTransferencia;
            miDona.Dinero = model.DonacionMonetaria.Dinero;
            miDona.IdUsuario = model.DonacionMonetaria.IdUsuario;
            miDona.IdPropuestaDonacionMonetaria = model.DonacionMonetaria.IdPropuestaDonacionMonetaria;
            miDona.FechaCreacion = model.DonacionMonetaria.FechaCreacion;
            db.DonacionesMonetarias.Add(miDona);

            db.SaveChanges();
        }
        public void GuardarDonacionInsumo(RealizarDonacionMV model)
        {
            foreach (DonacionesInsumosMetadatos d in model.TodasDonacionesInsumos)
            {
                DonacionesInsumos miDona = new DonacionesInsumos();
                if (d.Cantidad != 0)
                {
                    miDona.Cantidad = d.Cantidad;
                    miDona.IdPropuestaDonacionInsumo = d.IdPropuestaDonacionInsumo;
                    miDona.IdUsuario = d.IdUsuario;
                    

                    db.DonacionesInsumos.Add(miDona);
                }
                miDona = null;
            }
            db.SaveChanges();
        }
        public void GuardarDonacionHoras(RealizarDonacionMV model)
        {
            DonacionesHorasTrabajo miDona = new DonacionesHorasTrabajo();
            miDona.Cantidad = model.DonacionHoraTrabajo.Cantidad;
            miDona.IdPropuestaDonacionHorasTrabajo = model.DonacionHoraTrabajo.IdPropuestaDonacionHorasTrabajo;
            miDona.IdUsuario = model.DonacionHoraTrabajo.IdUsuario;

            db.DonacionesHorasTrabajo.Add(miDona);
            db.SaveChanges();
        }
        public String TipoDonacion(int id)
        {
            Propuestas miProp = db.Propuestas.Find(id);
            if (miProp.TipoDonacion == 1)
            {
                return "Monetario";
            }
            else if (miProp.TipoDonacion == 2)
            {
                return "Insumo";
            }
            return "Hora";
        }
        public List<DonacionesMonetarias> DonacionesMonetariasPorIDPropMonet(int id)
        {
            List<DonacionesMonetarias> miLista = db.DonacionesMonetarias.Where(d => d.IdPropuestaDonacionMonetaria == id)
                .ToList();
            if (miLista.Count == 0)
            {
                return new List<DonacionesMonetarias>();
            }
            return miLista;
        }
        public List<DonacionesHorasTrabajo> TodasDonacionesHoras(int Id)
        {
            List<DonacionesHorasTrabajo> list = db.DonacionesHorasTrabajo.Where(d => d.IdPropuestaDonacionHorasTrabajo == Id).ToList();
            return list;
        }
        public List<DonacionesInsumos> TodasDonacionesInsumoIDPropInsumo(int id)
        {
            return db.DonacionesInsumos.Where(x => x.IdPropuestaDonacionInsumo == id).ToList();
        }

        public Usuarios UsuarioConDonacionesPorId(int id)
        {
            return db.Usuarios.FirstOrDefault(m => m.IdUsuario == id);
        }

        public List<DonacionesMV> TodasDonacionesUsuario(int ID)
        {
            //model a enviar
            List<DonacionesMV> miDonaciones = new List<DonacionesMV>();
            //donaciones del usuario
            List<DonacionesMonetarias> misDonacionesM = db.DonacionesMonetarias.Include("PropuestasDonacionesMonetarias.Propuestas").Where(d => d.IdUsuario == ID)
                .OrderBy(d => d.FechaCreacion).ToList();
            List<DonacionesHorasTrabajo> misDonacionesT = db.DonacionesHorasTrabajo.Include("PropuestasDonacionesHorasTrabajo.Propuestas").Where(d => d.IdUsuario == ID)
                .OrderBy(d => d.IdDonacionHorasTrabajo).ToList();
            List<DonacionesInsumos> misDonacionesI = db.DonacionesInsumos.Include("PropuestasDonacionesInsumos.Propuestas").Where(d => d.IdUsuario == ID)
                .OrderBy(d => d.IdDonacionInsumo).ToList();
            // ingresando las donciones monetarias

            // tengo q validar q este llena y hacer uno por cada lista
            // monetaria
            if (misDonacionesM.Count() > 0)
            {
                foreach (DonacionesMonetarias dm in misDonacionesM)
                {
                    DonacionesMV modeloBase = new DonacionesMV();
                    modeloBase.Fecha = dm.FechaCreacion;
                    modeloBase.Nombre = dm.PropuestasDonacionesMonetarias.Propuestas.Nombre;
                    modeloBase.TipoDonacion = "Monetaria";
                    // estado un if
                    if (dm.PropuestasDonacionesMonetarias.Propuestas.Estado.Equals(1))
                    {
                        modeloBase.Estado = "Activa";
                    }
                    else if (dm.PropuestasDonacionesMonetarias.Propuestas.Estado.Equals(0))
                    {
                        modeloBase.Estado = "Denunciada";
                    }
                    else
                    {
                        modeloBase.Estado = "Finalizada";
                    }
                    // tottalrecaudado
                    List<DonacionesMonetarias> Donacionesmonto = db.DonacionesMonetarias.Where(d => d.IdPropuestaDonacionMonetaria == dm.IdPropuestaDonacionMonetaria)
                        .ToList();
                    double suma = 0.0;
                    foreach (DonacionesMonetarias donmonto in Donacionesmonto)
                    {
                        suma += Convert.ToDouble(donmonto.Dinero);
                    }
                    modeloBase.TotalRecaudado = suma;
                    ///
                    modeloBase.MiDonacion = Convert.ToDouble(dm.Dinero);
                    modeloBase.IdPropuesta = dm.PropuestasDonacionesMonetarias.IdPropuesta;
                    // agregarlo a la lista
                    miDonaciones.Add(modeloBase);
                    modeloBase = null;

                }
            }

            // trabajo horas
            if (misDonacionesT.Count() > 0)
            {
                foreach (DonacionesHorasTrabajo dt in misDonacionesT)
                {
                    DonacionesMV modeloBase = new DonacionesMV();
                    modeloBase.Fecha = null;
                    modeloBase.Nombre = dt.PropuestasDonacionesHorasTrabajo.Propuestas.Nombre;
                    modeloBase.TipoDonacion = "Horas de Trabajo";
                    // estado un if
                    if (dt.PropuestasDonacionesHorasTrabajo.Propuestas.Estado.Equals(1))
                    {
                        modeloBase.Estado = "Activa";
                    }
                    else if (dt.PropuestasDonacionesHorasTrabajo.Propuestas.Estado.Equals(0))
                    {
                        modeloBase.Estado = "Denunciada";
                    }
                    else
                    {
                        modeloBase.Estado = "Finalizada";
                    }
                    // tottalrecaudado
                    List<DonacionesHorasTrabajo> DonacionesHoras = db.DonacionesHorasTrabajo.Where(d => d.IdPropuestaDonacionHorasTrabajo == dt.IdPropuestaDonacionHorasTrabajo)
                        .ToList();
                    double suma = 0.0;
                    foreach (DonacionesHorasTrabajo donmonto in DonacionesHoras)
                    {
                        suma += Convert.ToDouble(donmonto.Cantidad);
                    }
                    modeloBase.TotalRecaudado = suma;
                    ///
                    modeloBase.MiDonacion = Convert.ToDouble(dt.Cantidad);
                    modeloBase.IdPropuesta = dt.PropuestasDonacionesHorasTrabajo.IdPropuesta;
                    // agregarlo a la lista
                    miDonaciones.Add(modeloBase);
                    modeloBase = null;

                }
            }

            // insumos
            if (misDonacionesI.Count() > 0)
            {
                foreach (DonacionesInsumos di in misDonacionesI)
                {
                    DonacionesMV modeloBase = new DonacionesMV();
                    modeloBase.Fecha = null;
                    modeloBase.Nombre = di.PropuestasDonacionesInsumos.Propuestas.Nombre;
                    modeloBase.TipoDonacion = "Insumos";
                    // estado un if
                    if (di.PropuestasDonacionesInsumos.Propuestas.Estado.Equals(1))
                    {
                        modeloBase.Estado = "Activa";
                    }
                    else if (di.PropuestasDonacionesInsumos.Propuestas.Estado.Equals(0))
                    {
                        modeloBase.Estado = "Finalizada";
                    }
                    else
                    {
                        modeloBase.Estado = "En Revision";
                    }
                    // tottalrecaudado
                    List<DonacionesInsumos> DonacionesInsumo = db.DonacionesInsumos.Where(d => d.IdPropuestaDonacionInsumo == di.IdPropuestaDonacionInsumo)
                        .ToList();
                    double suma = 0.0;
                    foreach (DonacionesInsumos donmonto in DonacionesInsumo)
                    {
                        suma += Convert.ToDouble(donmonto.Cantidad);
                    }
                    modeloBase.TotalRecaudado = suma;
                    ///
                    modeloBase.MiDonacion = Convert.ToDouble(di.Cantidad);
                    modeloBase.IdPropuesta = di.PropuestasDonacionesInsumos.IdPropuesta;
                    // agregarlo a la lista
                    miDonaciones.Add(modeloBase);
                    modeloBase = null;

                }
            }
            ///



            return miDonaciones;
        }
    }
}
