using Ferreteria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.DataAccess.Repositorio.IRepositorio
{
    public interface IMaterialRepositorio : IRepositorio<Material>
    {
        void Actualizar(Material material);
    }
}
