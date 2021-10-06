using Ferreteria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.DataAccess.Repositorio.IRepositorio
{
    public interface ITrabajoRepositorio : IRepositorio<Trabajo>
    {
        void Actualizar(Trabajo trabajo);
    }
}
