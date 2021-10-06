using Ferreteria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.DataAccess.Repositorio.IRepositorio
{
    public interface IClienteRepositorio : IRepositorio<Cliente>
    {
        void Actualizar(Cliente cliente);
    }
}
