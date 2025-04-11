using Azure;
using IKEA.BLL.Common.Services.Attachments;
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
using System.Transactions;

namespace IKEA.BLL.Services.EmployeeServices
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUOW _uow;
        private readonly IAttachmentService _attachmentService;

        public EmployeeService(IUOW uow, IAttachmentService attachmentService)
        {
            _uow = uow;
            _attachmentService = attachmentService;
        }

        public async Task<int> CreateEmployee(CreatedEmployeeDTO createdEmployeeDTO)
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

            if (createdEmployeeDTO.Image != null)
            {
                employee.ImageName= _attachmentService.UploadImage(createdEmployeeDTO.Image, "Images");

            }
            _uow.employeeRepository.Add(employee);
            return await _uow.complete();
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            var Employee =await _uow.employeeRepository.GetById(id);


            if (Employee != null)
            {
                if (Employee.ImageName != null)
                {
                    var filepath=Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files","Images",Employee.ImageName);
                    _attachmentService.DeleteImage(filepath);
                }
                 _uow.employeeRepository.Delete(Employee);
            }

            var result =await _uow.complete();
            if (result > 0)
                return true;
            else return false;
        }

        public async Task<IEnumerable<EmployeeDTO>> GetAll(string search)
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


        public async Task<EmployeeDetailsDTO>? GetByEmployeeId(int id)
        {
            var employee =await _uow.employeeRepository.GetById(id);
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
                    ImageName = employee.ImageName,
                };
            }
            return null;
        }

        public async Task<int> UpdateEmployee(UpdatedEmployeeDTO updatedEmployeeDTO)
        {
            var existingEmployee =await _uow.employeeRepository.GetById(updatedEmployeeDTO.Id);
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
            existingEmployee.ImageName = updatedEmployeeDTO.ImageName;

            if (updatedEmployeeDTO.Image != null)
            {
                if(existingEmployee.ImageName != null)
                {
                    var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", "Images", existingEmployee.ImageName);
                    _attachmentService.DeleteImage(filepath);
                }
                existingEmployee.ImageName = _attachmentService.UploadImage(updatedEmployeeDTO.Image, "Images");
            }
            _uow.employeeRepository.Update(existingEmployee);
            return await _uow.complete();
        }
    }
}
            

          
