using Proyecto.BaseDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace ProyectoWeb.Utilidades
{
    public class SessionHelper
    {
        private static HttpSessionState CurrentSession = HttpContext.Current.Session;

        public void GuardarSesion(Usuarios usuario)
        {
            CurrentSession["HaySesion"] = true;
            CurrentSession["UsuarioId"] = usuario.IdUsuario;
            CurrentSession["Username"] = usuario.UserName;
            CurrentSession["Nombre"] = usuario.Nombre;
            CurrentSession["EsAdmin"] = (usuario.IdUsuario == 1) ? true : false;
        }

        public int GetIdUsuarioEnSesion()
        {
            return (int)CurrentSession["UsuarioId"];
        }

        public String getNombreUsuarioEnSesion()
        {
           return this.HaySesionIniciada() ? (String)CurrentSession["Nombre"] : null;
          
        }

        public String getUserNameUsuarioEnSesion()
        {
            return this.HaySesionIniciada() ? (String)CurrentSession["Username"] :null;
        }

        public void CerrarSession()
        {
            CurrentSession.RemoveAll();
        }

        public bool HaySesionIniciada()
        {
            if (CurrentSession["HaySesion"] != null)
            {
                return (Boolean)CurrentSession["HaySesion"];
            }
            return false;
        }
        public bool EsAdmin()
        {
            if (CurrentSession["HaySesion"] != null)
            {
                return (Boolean)CurrentSession["EsAdmin"];
            }
            return false;

        }
        public String MensajeTemporalNoExisteUsuario()
        {
            return "Correo y contraseña invalidos !  ";
        }
    }
}