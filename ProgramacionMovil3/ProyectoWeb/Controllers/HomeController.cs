using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Proyecto.BaseDatos;
using Proyecto.ModelView;
using ProyectoWeb.Servicios;
using ProyectoWeb.Utilidades;
using ProyectoWeb.ViewModel;
namespace ProyectoWeb.Controllers
{
    public class HomeController : Controller
    {
        PropuestaServicio propuestaServicio = new PropuestaServicio();
        ValoracionServicio valoracionServicio = new ValoracionServicio();
        PerfilServicio perfilServicio = new PerfilServicio();
        SessionHelper sessionHelper = new SessionHelper();
        UMensajes mensajes = new UMensajes();

        [HttpGet]
        public ActionResult Index(string busqueda)
        {
            ViewBag.SesionIniciada = false;
            VM_Home _Home = new VM_Home();
         
            if (sessionHelper.HaySesionIniciada())
            {
                ViewBag.NombreUsuario = (sessionHelper.getUserNameUsuarioEnSesion() != null) ? sessionHelper.getUserNameUsuarioEnSesion() : "USUARIO";
                ViewBag.SesionIniciada = true;
                ViewBag.Busqueda = busqueda;
                ViewBag.EsAdmin = sessionHelper.EsAdmin();

                _Home.PropuestasActivas = propuestaServicio.ObtenerMisPropuestas(sessionHelper.GetIdUsuarioEnSesion());
                TempData["NoHayPropuestasActivas"] = (_Home.PropuestasActivas.Count <= 0) ? true : false;
               
                _Home.PropuestasInactivas = propuestaServicio.ObtenerPropuestasInactivas(sessionHelper.GetIdUsuarioEnSesion());
                _Home.ResultadosDelaBusqueda = (busqueda != null && busqueda != "") ? propuestaServicio.ObtenerPropuestasDeBusqueda(busqueda, sessionHelper.GetIdUsuarioEnSesion()) : new List<Propuestas>();
                TempData["NoHayResultadosParaLaBusqueda"] = (busqueda != null && _Home.ResultadosDelaBusqueda.Count <= 0) ? true : false;
                _Home.PropuestasMasValoradas = propuestaServicio.ObtenerPropuestasMasValoradasDelResto(sessionHelper.GetIdUsuarioEnSesion());

            }
            else
            {
                TempData["DebeLoguearse"] = true;
                _Home.PropuestasMasValoradas = propuestaServicio.Obtener5PropuestasDestacadas();
            }

            ViewBag.NoHayResultadosParaLaBusqueda = NoHayResultados();
            ViewBag.YaHayUnaSesionIniciada = HayUnaSesionIniciada();
            ViewBag.NoHayPropuestasActivas = NoHayPropuestasActivas();
            ViewBag.EstaExcedido = EstaExcedido();
            ViewBag.NuevaPropuestaCreada = EsNuevaPropuesta();
            ViewBag.ModificacionExitosa = ModificacionExitosa();
            ViewBag.ModificacionInvalida = ModificacionNoDisponible();
            ViewBag.NoPoseeLosPermisos = NoPoseeLosPermisos();
            ViewBag.ErrorInternoEnElServidor = ErrorInternoEnElServidor();
            ViewBag.DebeLoguearse = DebeLoguearse();
            ViewBag.NoEsPosibleActivar = NoEsPosibleActivar();
            ViewBag.ActivacionCorrecta = ActivacionCorrecta();
            ViewBag.NoEsPosibleModificar = NoEsPosibleModificar();

            return View("Index", _Home);

        }

        

        [HttpPost]
        public void Valorar(int IdPropuesta, bool valoracion)
        {
            ViewBag.SesionIniciada = false;

            if (sessionHelper.HaySesionIniciada())
            {
                valoracionServicio.ValorarPropuesta(sessionHelper.GetIdUsuarioEnSesion(), IdPropuesta, valoracion);
            }
        }

          public ActionResult Error(int error = 0)
          {
            switch (error)
            {
                case 505:
                    ViewBag.Title = "Ocurrio un error inesperado";
                    ViewBag.Description = "Esto es muy vergonzoso, esperemos que no vuelva a pasar...";
                    break;

                case 404:
                    ViewBag.Title = "Página no encontrada";
                    ViewBag.Description = "La URL que está intentando ingresar no existe.";
                    break;

                default:
                    ViewBag.Title = "Página no encontrada";
                    ViewBag.Description = "Lo sentimos, algo salio muy mal :( ..";
                    break;
            }

           
            return View("Error");
        }

        [ActionName("acerca-de")]
        public ActionResult AcercaDe()
        {
            ViewBag.SesionIniciada = false;

            if (sessionHelper.HaySesionIniciada())
            {
            ViewBag.NombreUsuario = (sessionHelper.getUserNameUsuarioEnSesion() != null) ? sessionHelper.getUserNameUsuarioEnSesion() : "USUARIO";
            ViewBag.SesionIniciada = true;
            ViewBag.EsAdmin = sessionHelper.EsAdmin();
            return View();
            }
           TempData["DebeLoguearse"] = true;
            return RedirectToAction("Login", "Perfil");

        }

        private bool EstaExcedido()
        {
            if (TempData["estaExcedido"] != null)
            {

                ViewBag.MensajeError = mensajes.MensajeExcedeElLimiteDePropuestasCreadas();
            }
            return TempData["estaExcedido"] != null;
        }
        private bool EsNuevaPropuesta()
        {
            if (TempData["propuestaCreada"] != null)
            {

                ViewBag.MensajeExitoso = mensajes.MensajePropuestasCreadas();
            }
            return TempData["propuestaCreada"] != null;
        }
        private bool ModificacionNoDisponible()
        {
            if (TempData["modificacionInactiva"] != null)
            {
                ViewBag.MensajeError = mensajes.MensajePropuestaFinalizada();

            }
            return TempData["modificacionInactiva"] != null;
        }
        private bool NoPoseeLosPermisos()
        {
            if (TempData["NoPoseeLosPermisos"] != null)
            {
                ViewBag.MensajeError = mensajes.NoPoseeLosPermisos();
            }
            return (TempData["NoPoseeLosPermisos"] != null);
        }

        private bool ErrorInternoEnElServidor()
        {
            if (TempData["ErrorInternoEnElServidor"] != null)
            {
                ViewBag.MensajeError = mensajes.ErrorInternoEnElServidor();
            }
            return (TempData["ErrorInternoEnElServidor"] != null);
        }
        private bool InicioSesionCorrecto()
        {
            ViewBag.MensajeTemporal = mensajes.MensajeTemporalInicioSesionCorrecto();
            return TempData["InicioSesion"] != null;
        }
        private bool NoHayPropuestasActivas()
        {
            ViewBag.MensajeTemporal = mensajes.MensajesAnimeseACrear();
            return (TempData["NoHayPropuestasActivas"] != null) && (! TempData["NoHayPropuestasActivas"].Equals(false));
        }

        private bool HayUnaSesionIniciada()
        {
            ViewBag.MensajeError = mensajes.YaPoseeUnaSesionIniciada();
            return (TempData["HaySesionIniciada"] != null);
        }

        private bool NoHayResultados()
        {
            ViewBag.MensajeNoHayResultados = mensajes.NoHayResultadosParaLaBusqueda();
            return (TempData["NoHayResultadosParaLaBusqueda"] != null && !TempData["NoHayResultadosParaLaBusqueda"].Equals(false));
        }

        private bool ModificacionExitosa()
        {
            ViewBag.ModificacionExitosaMensaje = mensajes.ModificacionExitosa();
            return (TempData["ModificacionExitosa"] != null);
        }

        private bool DebeLoguearse()
        {
            ViewBag.InicioSesionCorrecto = false;
            ViewBag.MensajeTemporal = mensajes.MensajeTemporalDebeIniciarSesion();
            return TempData["DebeLoguearse"] != null;
        }

        private bool NoEsPosibleActivar()
        {
            ViewBag.NoEsPosibleActivarMensaje = mensajes.NoEsPosibleActivar();
            return TempData["NoEsPosibleActivar"] != null;
        }

        private bool ActivacionCorrecta()
        {
            ViewBag.ActivadoMensaje = mensajes.ActivacionCorrecta();
            return TempData["ActivacionCorrecta"] != null;
        }

        private bool NoEsPosibleModificar()
        {
            ViewBag.NoEsPosibleModificarMensaje = mensajes.NoEsPosibleModificar();
            return TempData["NoEsPosibleModificar"] != null;
        }
    }
}