using IKEA.BLL.DTOs.Departments;
using IKEA.DAL.Models.Departments;
using IKEA.DAL.Persistance.Repositories.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services.DepartmentServices
{
    public class DepartmentService:IDepartmentService
    {
        private IDepartment Repository;

        public DepartmentService(IDepartment _repository)
        {
            Repository = _repository;   
        }

        public int CreateDepartment(CreatedDepartmentDTO createdDepartmentDTO)
        {
            var department = new Department()
            {
                Name = createdDepartmentDTO.Name,
                Code = createdDepartmentDTO.Code,
                Description = createdDepartmentDTO.Description,
                C0reationDate = createdDepartmentDTO.C0reationDate,
                CreatedBy = 1,
                Createdon=DateTime.Now,
                LastModifiedBy = 1,
                LastModifiedon = DateTime.Now
            };
            return Repository.Add(department);
        }

        public bool DeleteDepartment(int id)
        {
            var department=Repository.GetById(id);
          

            if (department != null)
                return Repository.Delete(department)>0;
            else return false;
        }

        public IEnumerable<DepartmentDTO> GetAll()
        {
            var Departments = Repository.GetAll().Select(dept => new DepartmentDTO()
            {
                Id = dept.Id,
                Name = dept.Name,
                Code = dept.Code,
                C0reationDate = dept.C0reationDate

            });
            return Departments;
            //List<DepartmentDTO> departmentDtos = new();
            //foreach(var dept in Departments)
            //{
            //    DepartmentDTO departmentDTO = new DepartmentDTO()
            //    {
            //        Id = dept.Id,
            //        Name = dept.Name,
            //        Code= dept.Code,
            //        C0reationDate = dept.C0reationDate
            //    };
            //    departmentDtos.Add(departmentDTO);
            //}
            //return departmentDtos;
        }

        public DepartmentDetailsDTO? GetByDepartmentId(int id)
        {
            var Department=Repository.GetById(id);
            if (Department != null) 
            {
                return new DepartmentDetailsDTO()
                {
                    Id = Department.Id,
                    Name = Department.Name,
                    Code = Department.Code,
                    Description = Department.Description,
                    C0reationDate = Department.C0reationDate,
                    IsDeleted = Department.IsDeleted,
                    LastModifiedBy = Department.LastModifiedBy,
                    LastModifiedon = Department.LastModifiedon,
                    CreatedBy = Department.CreatedBy,
                    Createdon = Department.Createdon,
                };
            
            }

            return null;
        }

        public int UpdateDepartment(UpdatedDepartmentDTO updatedDepartmentDTO)
        {
            var department = new Department()
            {
                Id= updatedDepartmentDTO.Id,
                Name = updatedDepartmentDTO.Name,
                Code = updatedDepartmentDTO.Code,
                Description = updatedDepartmentDTO.Description,
                C0reationDate= updatedDepartmentDTO.C0reationDate,
                LastModifiedBy=1,
                LastModifiedon=DateTime.Now,
            };
            return Repository.Update(department);
        }
    }
}
