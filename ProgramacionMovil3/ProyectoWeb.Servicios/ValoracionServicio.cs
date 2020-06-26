
using Proyecto.BaseDatos;
using System.Linq;

namespace ProyectoWeb.Servicios
{
    public class ValoracionServicio
    {
        Entities ctx = new Entities();

        public bool VerificarSiEstaValorado(int idUsuario, int idPropuesta) {
            bool result = false;
            PropuestasValoraciones propuestasValoraciones = ctx.PropuestasValoraciones.Where(x => x.IdUsuario == idUsuario && x.IdPropuesta == idPropuesta).FirstOrDefault();
            if (propuestasValoraciones != null) result = true;
            return result;
        }

        public void ValorarPropuesta(int idUsuario, int idPropuesta,bool valoracion)
        {            
            PropuestasValoraciones propuestasValoraciones = new PropuestasValoraciones();
            propuestasValoraciones.IdPropuesta = idPropuesta;
            propuestasValoraciones.IdUsuario = idUsuario;
            PropuestasValoraciones propuestasValoracionesBuscada = ctx.PropuestasValoraciones.Where(x => x.IdUsuario == idUsuario && x.IdPropuesta == idPropuesta).FirstOrDefault();
            bool usuarioYaValoro = propuestasValoracionesBuscada.Propuestas.PropuestasValoraciones.Where(y => y.IdUsuario == idUsuario && y.IdPropuesta == idPropuesta).Any();
            if (!usuarioYaValoro)
            {
                propuestasValoraciones.Valoracion = true;
                ctx.PropuestasValoraciones.Add(propuestasValoraciones);

            }
            int totalValoracionesPorPropuesta = propuestasValoracionesBuscada.Propuestas.PropuestasValoraciones.Where(y => y.IdPropuesta == idPropuesta).Count();
            int valoracionesBuenas = propuestasValoracionesBuscada.Propuestas.PropuestasValoraciones.Where(y => y.IdPropuesta == idPropuesta && y.Valoracion == true).Count();
            Propuestas propuestaAValorar = ctx.Propuestas.Where(x => x.IdPropuesta == idPropuesta).FirstOrDefault();
            valoracionesBuenas = valoracion ? (valoracionesBuenas + 1) : (valoracionesBuenas - 1);
            decimal porcentajeValoracion = ((decimal)valoracionesBuenas / (decimal)totalValoracionesPorPropuesta) * 100;
            propuestaAValorar.Valoracion = decimal.Round(porcentajeValoracion);
            ctx.SaveChanges();
        }
    }
}
