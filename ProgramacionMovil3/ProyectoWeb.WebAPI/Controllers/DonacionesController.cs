using Proyecto.BaseDatos;
using Proyecto.ModelView;
using ProyectoWeb.Servicios;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ProyectoWeb.WebAPI.Controllers
{
    [EnableCors(origins: "http://localhost:54725", headers: "*", methods: "*")]
    public class DonacionesController : ApiController
    {
        DonacionServicio Servicio = new DonacionServicio();
        // GET: api/Donaciones
        public Usuarios Get()
        {
            return Servicio.UsuarioConDonacionesPorId(2);
        }

        // GET: api/Donaciones/5
        public List<DonacionesMV> Get(int id)
        {
            var x = id;
            return Servicio.TodasDonacionesUsuario(x);
        }

        // POST: api/Donaciones
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Donaciones/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Donaciones/5
        public void Delete(int id)
        {
        }
    }
}
