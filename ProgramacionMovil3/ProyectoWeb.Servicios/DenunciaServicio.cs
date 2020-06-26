using Proyecto.BaseDatos;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ProyectoWeb.Servicios
{
    public class DenunciaServicio
    {
         Entities db = new Entities();
        PropuestaServicio serviciopropuesta = new PropuestaServicio();
        
        public void AgregarDenuncia(Denuncias nuevoDenuncia)
        {
            nuevoDenuncia.FechaCreacion = DateTime.Now;
            db.Denuncias.Add(nuevoDenuncia);
            db.SaveChanges();
            }

        public List<MotivoDenuncia> ListaMotivos()
        {
            return db.MotivoDenuncia.ToList();
        }
        public List<Denuncias> AllDenuniaSinRevisar()
        {
            return db.Denuncias.Where(a => a.Estado == 0)
                .OrderBy(a => a.FechaCreacion)
                .ToList();
        }
        public void ModificarEstado(string Id,int num)
        {
            int id = Convert.ToInt32(Id);
            Denuncias miDenuncia = db.Denuncias.Find(id);
            if (num == 0) 
            {
                miDenuncia.Estado = num;
            }
            else
            {
                miDenuncia.Estado = num;
            }
            db.SaveChanges();
        }
        public List<Denuncias> todasDenunciasporIdProp(int idProp) 
        {
            List<Denuncias>miList = db.Denuncias.Where(d => d.IdPropuesta == idProp).ToList();
            if (miList == null)
            {
                return new List<Denuncias>();
            }
            return miList;
        }
        public void EvaluarSiMandaARevision(int idProp) 
        {
            if (this.todasDenunciasporIdProp(idProp).Count()<=4)
            {
                //que no haga nada
            }
            serviciopropuesta.CambiarARevision(idProp);
        }
    }
}
