using IKEA.DAL.Models.Departments;
using IKEA.DAL.Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Persistance.Repositories.Generics
{
    public interface IGenericRepository<T> where T:ModelBase
    {
        IEnumerable<T> GetAll(bool withnotrack = true);
        Task<T?> GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        IEnumerable<Employee> GetAllDepartment();
    }
}
