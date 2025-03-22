using IKEA.BLL.DTOs.Employees;
using IKEA.DAL.Models.Employees;
using IKEA.DAL.Persistance.Repositories.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services.EmployeeServices
{
    public class EmployeeService:IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            
        }

        public int CreateEmployee(CreatedEmployeeDTO createdEmployeeDTO)
        {
            var employee = new Employee()
            {
                Name = createdEmployeeDTO.Name,
                Age = createdEmployeeDTO.Age,
                Address = createdEmployeeDTO.Address,
                IsActive = createdEmployeeDTO.IsActive,
                Email = createdEmployeeDTO.Email,
                salary = createdEmployeeDTO.salary,
                PhoneNUmber = createdEmployeeDTO.PhoneNUmber,
                HiringDate = createdEmployeeDTO.HiringDate,
                Gender = createdEmployeeDTO.Gender,
                EmployeeType = createdEmployeeDTO.EmployeeType,
                CreatedBy = 1,
                Createdon=DateTime.Now,
                LastModifiedon = DateTime.Now,
                LastModifiedBy=1
            };
            return _employeeRepository.Add(employee);
        }

        public bool DeleteEmployee(int id)
        {
            var Employee = _employeeRepository.GetById(id);


            if (Employee != null)
                return _employeeRepository.Delete(Employee) > 0;
            else return false;
        }

        public IEnumerable<EmployeeDTO> GetAll()
        {
           return _employeeRepository.GetAll().Where(E=>E.IsDeleted==false).Select(E=>new EmployeeDTO()
            {
                Id=E.Id,
                Name=E.Name,
                Age=E.Age,
                salary =E.salary,
                IsActive=E.IsActive,
                Email=E.Email,  
                Gender=nameof(E.Gender),
                EmployeeType=nameof(E.EmployeeType)

            }).ToList();

        }

        public EmployeeDetailsDTO? GetByEmployeeId(int id)
        {
            var employee= _employeeRepository.GetById(id);
            if (employee != null)
            {
                return new EmployeeDetailsDTO()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Age = employee.Age,
                    Address = employee.Address,
                    IsActive = employee.IsActive,
                    Email = employee.Email,
                    salary = employee.salary,
                    PhoneNUmber = employee.PhoneNUmber,
                    HiringDate = employee.HiringDate,
                    Gender = employee.Gender,
                    EmployeeType = employee.EmployeeType,
                    LastModifiedBy = employee.LastModifiedBy,
                    LastModifiedon = employee.LastModifiedon,
                    CreatedBy = employee.CreatedBy,
                    Createdon = employee.Createdon,
                };
            }
            return null;
        }

        public int UpdateEmployee(UpdatedEmployeeDTO updatedEmployeeDTO)
        {
            var employee = new Employee()
            {
                Id= updatedEmployeeDTO.Id,
                Name = updatedEmployeeDTO.Name,
                Age = updatedEmployeeDTO.Age,
                Address = updatedEmployeeDTO.Address,
                IsActive = updatedEmployeeDTO.IsActive,
                Email = updatedEmployeeDTO.Email,
                salary = updatedEmployeeDTO.salary,
                PhoneNUmber = updatedEmployeeDTO.PhoneNUmber,
                HiringDate = updatedEmployeeDTO.HiringDate,
                Gender = updatedEmployeeDTO.Gender,
                EmployeeType = updatedEmployeeDTO.EmployeeType,
                CreatedBy = 1,
                Createdon = DateTime.Now,
                LastModifiedon = DateTime.Now,
                LastModifiedBy = 1
            };
            return _employeeRepository.Update(employee);
        }
    }
}
