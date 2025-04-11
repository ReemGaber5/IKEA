using IKEA.BLL.DTOs.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services.DepartmentServices
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDTO>> GetAll();
        Task<DepartmentDetailsDTO>? GetByDepartmentId(int id);
        Task<int>CreateDepartment(CreatedDepartmentDTO createdDepartmentDTO);
        Task<int> UpdateDepartment(UpdatedDepartmentDTO updatedDepartmentDTO);
        Task<bool> DeleteDepartment(int id);


    }
}
