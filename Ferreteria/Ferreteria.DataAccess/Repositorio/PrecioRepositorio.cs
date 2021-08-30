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
    public class PrecioRepositorio : Repositorio<Precio>, IPrecioRepositorio
    {
        public PrecioRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        readonly ApplicationDbContext _db;

        public void Actualizar(Precio precio)
        {
            var p = _db.Precios.FirstOrDefault(s => s.IdPrecio == precio.IdPrecio);

            if (p != null)
            {
                p.IdPrecio = precio.IdPrecio;
                p.Trabajo = precio.Trabajo;
                p.Costo = precio.Costo;
            }
        }
    }
}
