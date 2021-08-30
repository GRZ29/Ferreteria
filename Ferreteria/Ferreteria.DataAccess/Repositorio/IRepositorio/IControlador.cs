using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.DataAccess.Repositorio.IRepositorio
{
    public interface IControlador : IDisposable 
    {
        IClienteRepositorio Cliente { get; }
        IMaterialRepositorio Material { get; }
        IPrecioRepositorio Precio { get; }
        ITrabajoRepositorio Trabajo { get; }
        ILoginRepositorio Login { get; }
        void Guardar();
    }
}

