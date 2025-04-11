using IKEA.DAL.Models.Departments;
using IKEA.DAL.Models.Employees;
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

        public void Add(T entity)
        {
            _Context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _Context.Set<T>().Remove(entity);
        }

        public IEnumerable<T> GetAll(bool withnotrack = true)
        {
            if (withnotrack)
            {
                return _Context.Set<T>().AsNoTracking().ToList();
            }
            return _Context.Set<T>().ToList();
        }

        public async Task<T?> GetById(int id)
        {

            var item =await _Context.Set<T>().FindAsync(id);
            return item;
        }

        public void Update(T entity)
        {
            _Context.Set<T>().Update(entity);
        }

        public IEnumerable<Employee> GetAllDepartment()
        {
            return _Context.Employees
                .Include(e => e.Department)
                .ToList();
        }
    }
}
