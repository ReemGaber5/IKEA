using IKEA.DAL.Persistance.Data;
using IKEA.DAL.Persistance.Repositories.Departments;
using IKEA.DAL.Persistance.Repositories.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Persistance.UnitOfWork
{
    public class UOW : IUOW
    {
        private readonly ApplicationDbContext _context;
        public IDepartment department { get; }
        public IEmployeeRepository employeeRepository { get;}

        public UOW(ApplicationDbContext context)
        {
            _context = context;
            department = new DepartmentRepo(context);
            employeeRepository = new EmployeeRepository(context);   
            
        }

        public int complete()
        {
          return  _context.SaveChanges();  
        }
    }
}
