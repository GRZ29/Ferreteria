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
    public class ClienteRepositorio : Repositorio<Cliente>, IClienteRepositorio
    {
        public ClienteRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        readonly ApplicationDbContext _db;

        public void Actualizar(Cliente cliente)
        {
            var c = _db.Clientes.FirstOrDefault(s => s.IdCliente == cliente.IdCliente);
            if (c != null)
            {
                c.NombreCliente = cliente.NombreCliente;
                c.ApellidosCliente = cliente.ApellidosCliente;
                c.TelefonoCliente = cliente.TelefonoCliente;
                c.CorreoCliente = cliente.CorreoCliente;
            }
        } 
    }
}
