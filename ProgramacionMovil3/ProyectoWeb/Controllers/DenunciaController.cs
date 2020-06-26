using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto.BaseDatos;
using ProyectoWeb.Servicios;
using Proyecto.ModelView;
using ProyectoWeb.Utilidades;

namespace ProyectoWeb.Controllers
{

    public class DenunciaController : Controller
    {
        private DenunciaServicio ServicioDenuncia = new DenunciaServicio();
        private PropuestaServicio ServicioPropuesta = new PropuestaServicio();
        private PerfilServicio ServicioPerfil = new PerfilServicio();
        SessionHelper sessionHelper = new SessionHelper(); 

        // GET: Denuncia/Denunciarpropuesta/{id}
        [HttpGet]
        public ActionResult DenunciarPropuesta(int id)
        {
            ViewBag.SesionIniciada = false;
            if (sessionHelper.HaySesionIniciada())
            {
                ViewBag.NombreUsuario = (sessionHelper.getUserNameUsuarioEnSesion() != null) ? sessionHelper.getUserNameUsuarioEnSesion() : "USUARIO";
                ViewBag.Id = sessionHelper.GetIdUsuarioEnSesion();
                 ViewBag.SesionIniciada = true;
                ViewBag.EsAdmin = sessionHelper.EsAdmin();
                //
                DenunciarPropuestaMV modelo = new DenunciarPropuestaMV();
                modelo.Propuesta = ServicioPropuesta.ObtenerPorId(id);
                if (modelo.Propuesta != null)
                {
                    modelo.Motivos = ServicioDenuncia.ListaMotivos();
                    return View(modelo);
                }
                TempData["ErrorInternoEnElServidor"] = true; 
                    return Redirect("../../propuestas/home");
            }

            TempData["DebeLoguearse"] = true;
            TempData["Redireccion"] = "../Denuncia/Denunciarpropuesta/"+id+"";
            return RedirectToAction("Login", "Perfil");
        }
        
        [HttpPost]
        public ActionResult RegistrarDenuncia(Denuncias denuncia)
        {
            if (sessionHelper.HaySesionIniciada())
            {
                ServicioDenuncia.EvaluarSiMandaARevision(denuncia.IdPropuesta);
                ServicioDenuncia.AgregarDenuncia(denuncia);
                return Redirect("../Propuesta/Detalle/" + denuncia.IdPropuesta + "");
            }
            TempData["DebeLoguearse"] = true;
            TempData["Redireccion"] = "../Denuncia/Denunciarpropuesta/" + denuncia.IdDenuncia + "";
            return RedirectToAction("Login", "Perfil");
        }

        [HttpGet]
        public ActionResult Denuncias()
        {
            ViewBag.SesionIniciada = false;
            if (sessionHelper.HaySesionIniciada())
            {
                ViewBag.SesionIniciada = true;
                if (sessionHelper.EsAdmin())
                {
                    ViewBag.NombreUsuario = (sessionHelper.getUserNameUsuarioEnSesion() != null) ? sessionHelper.getUserNameUsuarioEnSesion() : "USUARIO";
                    ViewBag.EsAdmin = sessionHelper.EsAdmin();
                    return View(ServicioDenuncia.AllDenuniaSinRevisar());
                }
                //no es admin
                return Redirect("../propuestas/home");
            }
            TempData["DebeLoguearse"] = true;
            TempData["Redireccion"] = "../denuncias";
            return RedirectToAction("Login", "Perfil");
        }

        // estado de denuncia 0= sin revisar , 1=aceptar y 2=Desestimar
        //realizar un enum para los estados de denuncia la logica de 
        //modificar el estado de la denuncia esta en el servicio
        [HttpPost]
        public ActionResult Desestimar(FormCollection form)
        {
            ServicioDenuncia.ModificarEstado(form["IdDenuncia"], 2);
            return RedirectToAction("Denuncias");
        }
        [HttpPost]
        public ActionResult Aceptar(FormCollection form)
        {
            ServicioDenuncia.ModificarEstado(form["IdDenuncia"], 1);
            ServicioPropuesta.CambiarADenunciada(Convert.ToInt32(form["IdPropuestas"]));
            return RedirectToAction("Denuncias");
        }
    }
}