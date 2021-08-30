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
    public class MaterialRepositorio : Repositorio<Material>, IMaterialRepositorio
    {
        public MaterialRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        readonly ApplicationDbContext _db;

        public void Actualizar(Material material)
        {
            var m = _db.Materiales.FirstOrDefault(s => s.IdMaterial == material.IdMaterial);

            if ( m != null)
            {
                m.IdMaterial = material.IdMaterial;
                m.NombreMaterial = material.NombreMaterial;
                m.CostoMaterial = material.CostoMaterial;
            }
        }
    }
}
