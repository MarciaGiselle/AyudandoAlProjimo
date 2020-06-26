using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoWeb.Utilidades
{
    public class UMensajes
    {

        public String MensajeTemporalDebeIniciarSesion()
        {
            return "Para continuar, por favor inicie sesión. O bien, registrese.  ";
        }

        public String MensajeTemporalInicioSesionCorrecto()
        {
            return "Ha iniciado sesión correctamente ! ";
        }

        public String MensajeExcedeElLimiteDePropuestasCreadas()
        {
            return "Excede el limite de propuestas creadas";
        }

        public String MensajePropuestasCreadas()
        {
            return "Su propuesta se ha creado exitosamente.";
        }

        public String MensajePropuestaFinalizada()
        {
            return "No es posible modificar la propuesta, ha sido finalizada.";
        }
        public String NoPoseeLosPermisos()
        {
            return "No posee los permisos para realizar esta acción.";
        }

        public String ErrorInternoEnElServidor()
        {
            return "Error interno en el servidor.";
        }

        public String MensajesAnimeseACrear()
        {
            return "Animese a crearla !";

        }

        public String YaPoseeUnaSesionIniciada()
        {
            return "Lo sentimos, ya posee una sesión iniciada !";

        }

        public String NoHayResultadosParaLaBusqueda()
        {
            return "Lo sentimos, no hay resultados para la búsqueda.";

        }
        public String ModificacionExitosa()
        {
            return "Se ha modificado correctamente su propuesta.";

        }

        public String NoEsPosibleActivar()
        {
            return "La propuesta no se puede activar. Excede el limite de propuestas activas.";

        }
        public String ActivacionCorrecta()
        {
            return "Se ha cambiado el estado de su propuesta.";

        }

        public String NoEsPosibleModificar()
        {
            return "La propuesta no se puede modificar.";
        }
    }
}
