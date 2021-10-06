using Ferreteria.DataAccess.Data;
using Ferreteria.DataAccess.Repositorio.IRepositorio;
using Ferreteria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.DataAccess.Repositorio
{
    public class TrabajoRepositorio : Repositorio<Trabajo>, ITrabajoRepositorio
    {
        public TrabajoRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        readonly ApplicationDbContext _db;

        public void Actualizar(Trabajo trabajo)
        {
            var t = _db.Trabajos.FirstOrDefault(s => s.IdTrabajo == trabajo.IdTrabajo);

            if (t != null)
            {
                t.IdTrabajo = trabajo.IdTrabajo;
                t.Cliente = trabajo.Cliente;
                t.NombreTrabajo = trabajo.NombreTrabajo;
            }
        }
    }
}
