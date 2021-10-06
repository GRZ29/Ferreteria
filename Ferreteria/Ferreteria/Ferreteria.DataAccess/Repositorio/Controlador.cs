using Ferreteria.DataAccess.Data;
using Ferreteria.DataAccess.Repositorio.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.DataAccess.Repositorio
{
    public class Controlador : IControlador
    {
        public Controlador(ApplicationDbContext db)
        {
            _db = db;
            Cliente = new ClienteRepositorio(_db);
            Material = new MaterialRepositorio(_db);
            Precio = new PrecioRepositorio(_db);
            Trabajo = new TrabajoRepositorio(_db);

        }

        readonly ApplicationDbContext _db;

        public IClienteRepositorio Cliente { get; private set; }
        public IMaterialRepositorio Material { get; private set; }
        public IPrecioRepositorio Precio { get; private set; }
        public ITrabajoRepositorio Trabajo { get; private set; }
        public ILoginRepositorio Login { get; private set; }


        public void Dispose()
        {
            _db.Dispose();
        }

        public void Guardar()
        {
            _db.SaveChanges();
        }
    }
}
