using Ferreteria.DataAccess.Data;
using Ferreteria.DataAccess.Repositorio.IRepositorio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.DataAccess.Repositorio
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        public Repositorio(ApplicationDbContext db)
        {
            _db = db;
            dbSet = _db.Set<T>();
        }

        readonly ApplicationDbContext _db;
        DbSet<T> dbSet;

        public void Agregar(T entidad)
        {
            dbSet.Add(entidad);
        }

        public T Buscar(int id)
        {
            return dbSet.Find(id);
        }

        public T Buscar(Expression<Func<T, bool>> filtro = null, string propiedades = null)
        {
            IQueryable<T> query = dbSet;

            if (filtro != null)
            {
                query = query.Where(filtro);
            }

            if (!string.IsNullOrEmpty(propiedades))
            {
                foreach (var propiedad in propiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(propiedad);
                }
            }

            return query.FirstOrDefault();
        }

        public IEnumerable<T> Listar(Expression<Func<T, bool>> filtro = null, Func<IQueryable<T>, IOrderedQueryable<T>> ordenamiento = null, string propiedades = null)
        {
            IQueryable<T> query = dbSet;

            if (filtro != null)
            {
                query = query.Where(filtro);
            }

            if (!string.IsNullOrEmpty(propiedades))
            {
                foreach (var propiedad in propiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(propiedad);
                }
            }

            if (ordenamiento != null)
            {
                return ordenamiento(query).ToList();
            }

            return query.ToList();
        }

        public void Remover(int id)
        {
            T entidad = dbSet.Find(id);
            Remover(entidad);
        }

        public void Remover(T entidad)
        {
            dbSet.Remove(entidad);
        }

        public void RemoverRango(IEnumerable<T> entidades)
        {
            dbSet.RemoveRange(entidades);
        }
    }
}
