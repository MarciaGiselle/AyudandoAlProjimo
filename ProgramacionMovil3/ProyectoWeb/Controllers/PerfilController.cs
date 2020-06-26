using Proyecto.BaseDatos;
using ProyectoWeb.Servicios;
using ProyectoWeb.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoWeb.Controllers
{
    public class PerfilController : Controller
    {
        PerfilServicio servicio = new PerfilServicio();
        CorreoServicio correoServicio = new CorreoServicio();
        SessionHelper sessionHelper = new SessionHelper();
        UMensajes mensajes = new UMensajes();


        public ActionResult Crear()
        {
            ViewBag.SesionIniciada = false;

            if (sessionHelper.HaySesionIniciada())
            {
                ViewBag.NombreUsuario = (sessionHelper.getUserNameUsuarioEnSesion() != null) ? sessionHelper.getUserNameUsuarioEnSesion() : "USUARIO";
                ViewBag.SesionIniciada = true;
                ViewBag.EsAdmin = sessionHelper.EsAdmin();

            }
            ViewBag.DebeLoguearse = DebeLoguearse();
            ViewBag.HayRedireccion = Redireccion();
            return View();
        }

        [HttpPost]
        public ActionResult CrearPerfil(Usuarios usuario)
        {
            string email;
            string token;

            if (ModelState.IsValid)
            {
                email = usuario.Email;
                token = servicio.Registrar(usuario);
                correoServicio.enviarActivador(email, token);

                return RedirectToAction("../Home/Index");
            }
            TempData["NoRegistrado"]= "Usuario no registrado";
            ViewBag.NoRegistrado = "Usuario no registrado";
            return RedirectToAction("Crear","Perfil");

        }


        public ActionResult Editar()
        {

            ViewBag.SesionIniciada = false;
            if (sessionHelper.HaySesionIniciada())
            {
                ViewBag.NombreUsuario = (sessionHelper.getUserNameUsuarioEnSesion() != null) ? sessionHelper.getUserNameUsuarioEnSesion() : "USUARIO";
                ViewBag.SesionIniciada = true;
                ViewBag.EsAdmin = sessionHelper.EsAdmin();
                int id = sessionHelper.GetIdUsuarioEnSesion();
                Usuarios miUsuario = servicio.ObtenerPorId(id);
                return View(miUsuario);
            }

            ViewBag.DebeLoguearse = DebeLoguearse();
            ViewBag.HayRedireccion = Redireccion();
            return RedirectToAction("Login", "Perfil");
        }

        [HttpPost]
        public ActionResult EditarPerfil(Usuarios usuario)
        {
            

            ViewBag.SesionIniciada = false;

            if (!ModelState.IsValid)
            {
                int id = sessionHelper.GetIdUsuarioEnSesion();
                Usuarios miUsuario = usuario;
                miUsuario.IdUsuario = id;


                servicio.ModificarPerfil(miUsuario);
                return RedirectToAction("MiPerfil", "Perfil");
            }
            
            return View();

        }

           
        public ActionResult MiPerfil()
        {
            ViewBag.SesionIniciada = false;

            if (sessionHelper.HaySesionIniciada())
            {
                ViewBag.NombreUsuario = (sessionHelper.getUserNameUsuarioEnSesion() != null) ? sessionHelper.getUserNameUsuarioEnSesion() : "USUARIO";
                ViewBag.SesionIniciada = true;
                ViewBag.EsAdmin = sessionHelper.EsAdmin();
                int id = sessionHelper.GetIdUsuarioEnSesion();
                Usuarios miUsuario = servicio.ObtenerPorId(id);
                return View(miUsuario);
            }

            return RedirectToAction("Login", "Perfil");
        }

        public ActionResult Lista(int id)
        {
            if (sessionHelper.HaySesionIniciada() && sessionHelper.GetIdUsuarioEnSesion() == id)
            {
                return View(servicio.ObtenerTodos());
            }
            return RedirectToAction("Login", "Perfil");
        }


        public ActionResult Login()
        {
            ViewBag.SesionIniciada = false;
            if (sessionHelper.HaySesionIniciada())
            {
                ViewBag.NombreUsuario = (sessionHelper.getUserNameUsuarioEnSesion() != null) ? sessionHelper.getUserNameUsuarioEnSesion() : "USUARIO";
                ViewBag.SesionIniciada = true;
                ViewBag.EsAdmin = sessionHelper.EsAdmin();
                TempData["HaySesionIniciada"] = true;
                return RedirectToAction("Index", "Home");
            }
            else
            {
            TempData["DebeLoguearse"] = true;
            TempData["mensaje"] = "usuario y/o contraseña incorrectos";
            ViewBag.DebeLoguearse = DebeLoguearse();
             ViewBag.HayRedireccion = Redireccion();
              return View();

            }


            
        }

        public ActionResult IngresarLogin(Usuarios user)
        {
            Usuarios usuarios = servicio.Login(user);

            if (usuarios == null)
            {
                ViewBag.MensajeTemporalNoExisteUsuario = sessionHelper.MensajeTemporalNoExisteUsuario();
               
                return RedirectToAction("../Perfil/Login");
            }

            //  M.A: ESTO ANDA, NO BORRAR, AJUSTAR EN CASO DE INCIO DE SESION INCORRECTO
            sessionHelper.GuardarSesion(usuarios);
            //TempData["esAdmin"] = (sessionHelper.EsAdmin()) ? true : false;
            TempData["InicioSesion"] = true;
            String redireccion = Request["redireccion"];
            ViewBag.SesionIniciada = InicioSesionCorrecto();
            if (redireccion != null && redireccion != "")
            {
                TempData["Redireccion"] = true;
                return RedirectToAction(redireccion);
            }
            return RedirectToAction("Index", "Home");
        }
        private bool InicioSesionCorrecto()
        {
            ViewBag.MensajeTemporal = mensajes.MensajeTemporalInicioSesionCorrecto();
            return TempData["InicioSesion"] != null;
        }

        private bool Redireccion()
        {
            ViewBag.Redireccion = TempData["Redireccion"];
            return TempData["Redireccion"] != null;
        }

        private bool DebeLoguearse()
        {
            ViewBag.InicioSesionCorrecto = false;
            ViewBag.MensajeTemporal = mensajes.MensajeTemporalDebeIniciarSesion();
            return TempData["DebeLoguearse"] != null;
        }

        public ActionResult CerrarSession()
        {
            sessionHelper.CerrarSession();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ValidarUsuario(String token)
        {
            bool usuarioEsValido = servicio.ObtenerPorToken(token);
            //if(!usuarioEsValida) enviar error al login
            return RedirectToAction("../Perfil/Login");
        }

        

    }
}