using Azure;
using IKEA.BLL.DTOs.Employees;
using IKEA.DAL.Common.Enums;
using IKEA.DAL.Models.Employees;
using IKEA.DAL.Persistance.Repositories.Employees;
using IKEA.DAL.Persistance.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services.EmployeeServices
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUOW _uow;

        public EmployeeService(IUOW uow)
        {
            _uow = uow;
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
                DeptId = createdEmployeeDTO.DeptId,
                CreatedBy = 1,
                Createdon = DateTime.Now,
                LastModifiedon = DateTime.Now,
                LastModifiedBy = 1
            };
            _uow.employeeRepository.Add(employee);
            return _uow.complete();
        }

        public bool DeleteEmployee(int id)
        {
            var Employee = _uow.employeeRepository.GetById(id);


            if (Employee != null)
                _uow.employeeRepository.Delete(Employee);

            var result = _uow.complete();
            if (result > 0)
                return true;
            else return false;
        }

        public IEnumerable<EmployeeDTO> GetAll(string search)
        {
            return _uow.employeeRepository.GetAllDepartment()
                .Where(E => !E.IsDeleted && (string.IsNullOrEmpty(search) || E.Name.ToLower().Contains(search.ToLower())))
                .Select(E => new EmployeeDTO()
                {
                    Id = E.Id,
                    Name = E.Name,
                    Department = E.Department?.Name ?? "N/A",
                    Age = E.Age,
                    salary = E.salary,
                    IsActive = E.IsActive,
                    Email = E.Email,
                    Gender = E.Gender,
                    EmployeeType = E.EmployeeType
                }).ToList();
        }


        public EmployeeDetailsDTO? GetByEmployeeId(int id)
        {
            var employee = _uow.employeeRepository.GetById(id);
            if (employee != null)
            {
                return new EmployeeDetailsDTO()
                {
                    Id = employee.Id,
                    DeptId = employee.DeptId,
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
            var existingEmployee = _uow.employeeRepository.GetById(updatedEmployeeDTO.Id);
            if (existingEmployee == null)
                return 0;
            existingEmployee.Name = updatedEmployeeDTO.Name;
            existingEmployee.Age = updatedEmployeeDTO.Age;
            existingEmployee.Address = updatedEmployeeDTO.Address;
            existingEmployee.IsActive = updatedEmployeeDTO.IsActive;
            existingEmployee.Email = updatedEmployeeDTO.Email;
            existingEmployee.salary = updatedEmployeeDTO.salary;
            existingEmployee.PhoneNUmber = updatedEmployeeDTO.PhoneNUmber;
            existingEmployee.HiringDate = updatedEmployeeDTO.HiringDate;
            existingEmployee.Gender = updatedEmployeeDTO.Gender;
            existingEmployee.EmployeeType = updatedEmployeeDTO.EmployeeType;
            existingEmployee.DeptId = updatedEmployeeDTO.DeptId;
            existingEmployee.LastModifiedon = DateTime.Now;
            existingEmployee.LastModifiedBy = 1;

            _uow.employeeRepository.Update(existingEmployee);
            return _uow.complete();



        }
    }
}
            

          
