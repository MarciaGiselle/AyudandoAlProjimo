using Proyecto.BaseDatos;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Web;

namespace ProyectoWeb.Servicios
{
	public class PerfilServicio
	{

		Entities ctx = new Entities();
		public string Registrar(Usuarios user)
		{
            string tokenResult;
			Usuarios miUsuario = new Usuarios();
			miUsuario.Email = user.Email;
			miUsuario.Password = user.Password;
			miUsuario.FechaNacimiento = user.FechaNacimiento;
			miUsuario.FechaCracion = DateTime.Today;
			miUsuario.Activo = false;
            miUsuario.Foto = null;
            miUsuario.TipoUsuario =1;
			miUsuario.Token = GenerarToken();
            tokenResult = miUsuario.Token;
            ctx.Usuarios.Add(miUsuario);


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

            //ctx.SaveChanges();
            return tokenResult;

        }

        private string GenerarToken() {
            string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            int longitud = 10;
            while (0 < longitud--)
            {
                res.Append(caracteres[rnd.Next(caracteres.Length)]);
            }
            return res.ToString();
        }

		public Usuarios ObtenerPorId(int id)
		{
			return ctx.Usuarios.Find(id);
		}
        public bool ObtenerPorToken(string token)
        {
            bool result = false;
            Usuarios usuario = ctx.Usuarios.FirstOrDefault(x => x.Token == token);
            if (usuario != null) {
                usuario.Activo = true;
                result = true;
                ctx.SaveChanges();
            };
            return result;
        }



        public void ModificarPerfil(Usuarios u)
        {

            Usuarios miUsuario = ObtenerPorId(u.IdUsuario);

           miUsuario.Nombre = u.Nombre;
           miUsuario.Apellido = u.Apellido;
            

            miUsuario.FechaNacimiento = u.FechaNacimiento;
            miUsuario.Foto = u.Foto;

            if (u.UserName == null)
            {
                string var1 = miUsuario.Nombre;
                string var2 = miUsuario.Apellido;
                int num = 0;
                string nombre = var1 + "." + var2;
                //trae repetidos del username
                List<Usuarios> miLista = ctx.Usuarios.Where(x => x.UserName.Contains(nombre)).ToList();
                if (miLista.Count() > 0)
                {

                    num = miLista.Count();
                    miUsuario.UserName = var1 + "." + var2 + "." + num;

                }

            }

            ctx.SaveChanges();
        }

       

		public List<Usuarios> ObtenerTodos()
		{
			return ctx.Usuarios.ToList();
		}

		public Usuarios Login(Usuarios user)
		{

		  return ctx.Usuarios.Where(x=>x.Email==user.Email&&x.Password==user.Password).FirstOrDefault();           
			
		}

		public void GrabarSession(Usuarios user)
		{
			
            
        }

		public void DestruirSession()
		{
			HttpContext.Current.Session["_usuarioLogueado"] = false;
			HttpContext.Current.Session["user"] = null;
			HttpContext.Current.Session["userID"] = null;
			HttpContext.Current.Session["_usuarioObj"] = null;
            HttpContext.Current.Session["_esAdmin"] = null;

        }

		public Boolean ValidarSession()
		{
            return (HttpContext.Current.Session["_usuarioLogueado"].Equals( false));
			
		}

		public int IdSession()
		{
			return Convert.ToInt32(HttpContext.Current.Session["userID"]);
		}
        public Boolean EsAdmin()
        {
            if (HttpContext.Current.Session["_esAdmin"].Equals(true))
            {
                return true;
            }
            return false;
        }



        public Boolean ValidarCamposLlenos(int id)
        {
            var IdUser = ctx.Usuarios.Find(id);

            if (IdUser.UserName == null)
            {
                return false;
            }

            return true;
        }

    }

   
	
}
