using IKEA.DAL.Models.Departments;
using IKEA.DAL.Persistance.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Persistance.Repositories.Generics
{
    public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase
    {
        private readonly ApplicationDbContext _Context;

        public GenericRepository(ApplicationDbContext context)
        {
            _Context = context;
        }

        public int Add(T entity)
        {
            _Context.Set<T>().Add(entity);
            return _Context.SaveChanges();
        }

        public int Delete(T entity)
        {
            _Context.Set<T>().Remove(entity);
            return _Context.SaveChanges();
        }

        public IEnumerable<T> GetAll(bool withnotrack = true)
        {
            if (withnotrack)
            {
                return _Context.Set<T>().AsNoTracking().ToList();
            }
            return _Context.Set<T>().ToList();
        }

        public T? GetById(int id)
        {

            var item = _Context.Set<T>().Find(id);
            return item;
        }

        public int Update(T entity)
        {
            _Context.Set<T>().Update(entity);
            return _Context.SaveChanges();
        }
    }
}
