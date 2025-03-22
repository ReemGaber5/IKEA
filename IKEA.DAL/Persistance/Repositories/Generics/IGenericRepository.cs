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
        T? GetById(int id);
        int Add(T entity);
        int Update(T entity);
        int Delete(T entity);
    }
}
