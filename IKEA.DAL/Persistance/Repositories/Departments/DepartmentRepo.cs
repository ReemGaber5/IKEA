using IKEA.DAL.Models.Departments;
using IKEA.DAL.Persistance.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Persistance.Repositories.Departments
{
    public class DepartmentRepo : IDepartment
    {
        private readonly ApplicationDbContext _Context;

        public DepartmentRepo(ApplicationDbContext context)
        {
            _Context = context;
        }

        public int Add(Department department)
        {
           _Context.Add(department);
            return _Context.SaveChanges();
        }

        public int Delete(Department department)
        {
            _Context.Remove(department);
            return _Context.SaveChanges();
        }

        public IEnumerable<Department> GetAll(bool withnotrack = true)
        {
            if (withnotrack)
            {
                return _Context.Departments.AsNoTracking().ToList();
            }
            return _Context.Departments.ToList();
        }

        public Department? GetById(int id)
        {
            
            var department = _Context.Departments.Find(id);
            return department;
        }

        public int Update(Department department)
        {
            _Context.Update(department);
            return _Context.SaveChanges();
        }
    }
}
