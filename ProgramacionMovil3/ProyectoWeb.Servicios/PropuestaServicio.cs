using Proyecto.BaseDatos;
using ProyectoWeb.Utilidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;

namespace ProyectoWeb.Servicios
{

    public class PropuestaServicio
    {
        Entities ctx = new Entities();
        public void Crear(Propuestas propuesta, int idUsuario)
        {
            propuesta.FechaCreacion = DateTime.Today;
            propuesta.Estado = (int)Estado.Activa;
            propuesta.IdUsuarioCreador = idUsuario;
            ctx.Propuestas.Add(propuesta);
            //Esto  muestras los errores del validate por BD
            try
            {
                ctx.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }
            }
        }

        public List<Propuestas> ObtenerPropuestasDeBusqueda(string texto, int id)
        {
            List <Propuestas> propuestas = 
                ctx.Propuestas.Include("Usuarios").Where
                (x=> (x.Nombre.Contains(texto) ||
                x.Usuarios.Nombre.Contains(texto) )
                && x.IdUsuarioCreador != id && x.Estado == (int)Estado.Activa
                && x.FechaFin >= DateTime.Today)
                .OrderBy(x => x.FechaFin)
                .ThenByDescending(x=> x.Valoracion)
                .ToList();
            return (propuestas.Count() > 0) ? propuestas : new List<Propuestas>();

        }

        //devuelve true si la botonera debe ser visible
        public bool SeDebeVerBotonera(Propuestas propuestas, int id)
        {
            return ((propuestas.IdUsuarioCreador != id 
                && propuestas.Estado == (int)Estado.Activa
                && propuestas.FechaFin >= DateTime.Today));
        }

        //esta vigente
        public bool EstaVigente(Propuestas propuestas)
        {
            return  (propuestas.FechaFin >= DateTime.Today);
        }

        public List<Propuestas> Obtener5PropuestasDestacadas()
        {
            var today = DateTime.Today;
            List<Propuestas> propuestas = ctx.Propuestas
                .Include("PropuestasValoraciones")
                .OrderByDescending(z => z.FechaCreacion)
                .OrderByDescending(z => z.Valoracion)
                .Where(x => x.Estado == 1 && x.FechaFin >= today)
                .Take(5)
                .ToList();
            return propuestas;
        }

        public List<Propuestas> ObtenerPropuestasMasValoradasDelResto(int id)
        {
            List<Propuestas> propuestas =
              ctx.Propuestas.Include("Usuarios").Where
              (x => x.IdUsuarioCreador != id &&
               x.Estado == (int)Estado.Activa &&
               x.FechaFin >= DateTime.Today)
              .OrderBy(x => x.FechaFin)
              .ThenByDescending(x => x.Valoracion)
              .Take(3)
              .ToList();
            return (propuestas.Count() > 0) ? propuestas : new List<Propuestas>();
        }

        //obtiene las propuestas de un usuario logueado las cuales pueden estar en estado de revision o estado
        //activo, y cuya fecha es mayor o igual a la del dia, es decir que siguen vigentes. 
        public List<Propuestas> ObtenerMisPropuestas(int idUsuario)
        {
            return ctx.Propuestas
                 .Where
                 (u => u.IdUsuarioCreador == idUsuario &&
                 (u.Estado == (int)Estado.Activa) &&
                 (u.FechaFin >= DateTime.Today))
                .ToList();
        }

        //return true si la propuesta se puede editar
        public bool EsEditable(Propuestas j, int idUsuario)
        {
            return ((j.FechaFin >= DateTime.Today ) 
                && ((j.Estado == (int)Estado.Activa) || (j.Estado == (int)Estado.EnRevision))
                && (j.IdUsuarioCreador == idUsuario));
            
        }


        //obtiene las propuestas de un usuario logueado con estado inactivo, es decir q fueron bloqueadas
        public List<Propuestas> ObtenerPropuestasInactivas(int idUsuario)
        {
            List<Propuestas> propuestas = ctx.Propuestas
                        .Where
                        (x => ((x.Usuarios.IdUsuario == idUsuario) &&
                        ((x.Estado == (int)Estado.Inactiva || x.Estado == (int)Estado.EnRevision) ||
                        (x.Estado == (int)Estado.Activa && x.FechaFin < DateTime.Today))))
                        .ToList();
            return (propuestas.Count() > 0) ? propuestas : new List<Propuestas>();
        }

        //obtiene las propuestas de un usuario logueado con estado inactivo, es decir q fueron bloqueadas
        public List<Propuestas> ObtenerPropuestasDenunciadas(int idUsuario)
        {
            List<Propuestas> propuestas = ctx.Propuestas
                        .Where(x => x.Usuarios.IdUsuario == idUsuario && x.Estado == (int)Estado.Denunciada)
                        .ToList();
            return (propuestas.Count() > 0) ? propuestas : new List<Propuestas>();
        }
      

        public List<Propuestas> obtenerPropuestasGeneral(int idUsuario)
        {
            return ctx.Propuestas
                .Where(x => x.Usuarios.IdUsuario != idUsuario && x.Estado == 1)
                .ToList();
        }
        public bool ValidarCantidadDePropuestasActivas(int idUsuario)
        {
           int cantidadPropuestas =  ctx.Propuestas.Where
                 (u => u.IdUsuarioCreador == idUsuario
                 && (u.Estado == (int)Estado.Activa)
                 && u.FechaFin >= DateTime.Today)
                .Select(o => o.IdPropuesta).Count();
            return (cantidadPropuestas < 3);
        }

        public void Eliminar(int id)
        {
            var j = ObtenerPorId(id);
            ctx.Propuestas.Remove(j);

        }

        public Boolean Modificar(Propuestas nueva, string otraProfesion )
        {
            Propuestas anterior;
            try
            {
                anterior = ObtenerPorId(nueva.IdPropuesta);
                //los campos a actualzar
                //seteo valor otra profesion

                anterior.Nombre = nueva.Nombre;
                anterior.FechaFin = nueva.FechaFin;
                anterior.Descripcion = nueva.Descripcion;
                if (nueva.Foto != null)
                {
                    anterior.Foto = nueva.Foto;
                }

                anterior.TelefonoContacto = nueva.TelefonoContacto;

                //actualizo referencias
                anterior.PropuestasReferencias.ElementAt(0).Nombre = nueva.PropuestasReferencias.ElementAt(0).Nombre;
                anterior.PropuestasReferencias.ElementAt(0).Telefono = nueva.PropuestasReferencias.ElementAt(0).Telefono;

                anterior.PropuestasReferencias.ElementAt(1).Nombre = nueva.PropuestasReferencias.ElementAt(1).Nombre;
                anterior.PropuestasReferencias.ElementAt(1).Telefono = nueva.PropuestasReferencias.ElementAt(1).Telefono;

                //anterior.PropuestasReferencias = nueva.PropuestasReferencias;

                if (nueva.TipoDonacion == (int)TipoDePropuesta.Monetaria)
                {
                    // anterior.PropuestasDonacionesMonetarias= nueva.PropuestasDonacionesMonetarias;
                    anterior.PropuestasDonacionesMonetarias.ElementAt(0).CBU = nueva.PropuestasDonacionesMonetarias.ElementAt(0).CBU;
                    anterior.PropuestasDonacionesMonetarias.ElementAt(0).Dinero = nueva.PropuestasDonacionesMonetarias.ElementAt(0).Dinero;
                }
                else if (nueva.TipoDonacion == (int)TipoDePropuesta.Insumos)
                {
                    int sizeAnterior = anterior.PropuestasDonacionesInsumos.Count;
                    int sizeNuevo = nueva.PropuestasDonacionesInsumos.Count;

                    int i = 0;
                    foreach (var item in nueva.PropuestasDonacionesInsumos)
                    {
                        if (i < sizeAnterior)
                        {
                            //actualizo los q tenga
                            item.Nombre = nueva.PropuestasDonacionesInsumos.ElementAt(i).Nombre;
                            item.Cantidad = nueva.PropuestasDonacionesInsumos.ElementAt(i).Cantidad;
                            i++;
                        }

                        else if (HayNuevosInsumos(sizeAnterior, sizeNuevo))
                        {
                            anterior.PropuestasDonacionesInsumos.Add(item);

                        }

                    }
                    //ver insumos
                }
                else
                {
                    if (otraProfesion != null && otraProfesion != "")
                    {
                        nueva.PropuestasDonacionesHorasTrabajo.ElementAt(0).Profesion = otraProfesion;
                    }
                    else
                    {
                        anterior.PropuestasDonacionesHorasTrabajo.ElementAt(0).Profesion = nueva.PropuestasDonacionesHorasTrabajo.ElementAt(0).Profesion;
                    }
                    anterior.PropuestasDonacionesHorasTrabajo.ElementAt(0).CantidadHoras = nueva.PropuestasDonacionesHorasTrabajo.ElementAt(0).CantidadHoras;
                }

                ctx.SaveChanges();
            }
            catch (NullReferenceException)
            {
                return false;

            }
            return false;


        }

        public bool HayNuevosInsumos(int anterior, int nueva)
        {
            return (anterior < nueva);
           
        }
        public List<Propuestas> ObtenerTodos(int id)
        {
 
            return ctx.Propuestas.Where(u=> u.IdUsuarioCreador == id).ToList();
        }

        public bool TieneValoracion(Propuestas prop, int idUsuario)
        {
            List<PropuestasValoraciones> propuestaValoracion = prop.PropuestasValoraciones.ToList();
            return propuestaValoracion.Where(x => x.IdUsuario == idUsuario && x.IdPropuesta == prop.IdPropuesta).Any();
        }

        public Propuestas ObtenerPorId(int id)
        {
            return ctx.Propuestas.Include("PropuestasDonacionesHorasTrabajo")
                .Include("PropuestasDonacionesMonetarias")
                .Include("PropuestasDonacionesInsumos")
                .Include("PropuestasReferencias")
                .FirstOrDefault(p => p.IdPropuesta == id);
        }
       /* public int getCantidadPropuestasActivas(int idUsuario)
        {
            return ctx.Propuestas.Count(p => p.Estado == 0 && p.IdUsuarioCreador == idUsuario);
        }*/

        public PropuestasDonacionesMonetarias BuscarMonetariaPorId(int id)
        {
            return ctx.PropuestasDonacionesMonetarias.Where(p => p.IdPropuesta == id).FirstOrDefault();
        }
        public List<PropuestasDonacionesInsumos> BuscarInsumoPorId(int id)
        {
            return ctx.PropuestasDonacionesInsumos.Where(p => p.IdPropuesta == id).ToList();
        }

        public Boolean CambiarEstado(int idPropuesta, int idUsuario)
        {
            try
            {
                Propuestas propuesta = ObtenerPorIdSinInclude(idPropuesta);
                if (propuesta != null && propuesta.IdUsuarioCreador == idUsuario)
                {
                    if (propuesta.Estado == (int)Estado.Activa)
                    {
                        //fecha ayer
                        //propuesta.FechaFin = DateTime.Today.AddDays(Double.Parse("-1"));
                        propuesta.Estado = (int)Estado.Inactiva;
                        ctx.SaveChanges();
                        return true;

                    }
                    else if (propuesta.Estado == (int)Estado.Inactiva)
                    {

                        if (this.ValidarCantidadDePropuestasActivas(idUsuario) && this.EstaVigente(propuesta))
                        {
                            propuesta.Estado = (int)Estado.Activa;
                           // propuesta.FechaFin = DateTime.Today.AddDays(Double.Parse("1"));
                            ctx.SaveChanges();
                            return true;
                        }
                    }
                    return false;
                }
            }
            catch (NullReferenceException)
            {
                return false;
            }
            return false;
        }

        public Propuestas ObtenerPorIdSinInclude(int idPropuesta)
        {
            return ctx.Propuestas.FirstOrDefault(c=> c.IdPropuesta == idPropuesta);
        }

        public List<PropuestasDonacionesHorasTrabajo> BuscarHorasPorId(int id)
        {

            return ctx.PropuestasDonacionesHorasTrabajo.Where(p => p.IdPropuesta == id).ToList();
        }

        public Propuestas ObtenerPorIdParaDetalle(int id)
        {
            Propuestas propuesta;
            try
            {
                propuesta = this.ObtenerPorId(id);
            
                 if (propuesta.TipoDonacion == (int)TipoDePropuesta.Monetaria)
                {
                    return ctx.Propuestas
                    .Include("PropuestasDonacionesMonetarias")
                    .Include("PropuestasReferencias")
                    .Include("PropuestasValoraciones")
                    .FirstOrDefault(p => p.IdPropuesta == id);
                }
                if (propuesta.TipoDonacion == (int)TipoDePropuesta.Insumos)
                {
                    return ctx.Propuestas
                    .Include("PropuestasDonacionesInsumos")
                    .Include("PropuestasReferencias")
                    .Include("PropuestasValoraciones")
                    .FirstOrDefault(p => p.IdPropuesta == id);
                }
                if (propuesta.TipoDonacion == (int)TipoDePropuesta.HorasDeTrabajo)
                {
                    return ctx.Propuestas
                    .Include("PropuestasDonacionesHorasTrabajo")
                    .Include("PropuestasReferencias")
                    .Include("PropuestasValoraciones")
                    .FirstOrDefault(p => p.IdPropuesta == id);
                }
            }
            catch (NullReferenceException)
            {
                return null;

            }
            return null;
        }
         
        public void CambiarARevision(int idProp)
        {
            Propuestas Propu = this.ObtenerPorIdSinInclude(idProp);
            Propu.Estado = (int)Estado.EnRevision;
            try
            {
                ctx.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }
            }

        }
        public void CambiarADenunciada(int Prop) 
        {
            Propuestas Propu = this.ObtenerPorIdSinInclude(Prop);
            Propu.Estado = (int)Estado.Denunciada;
            ctx.SaveChanges();
        }
           
           
        }




    }

