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
    public class LoginRepositorio : Repositorio<Login>, ILoginRepositorio
    {
        public LoginRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        readonly ApplicationDbContext _db;

        public void Actualizar(Login login)
        {
            var c = _db.Login.FirstOrDefault(s => s.Id == login.Id);

            if (c == null)
                return;

            _db.Update(login);
        }
    }
}
