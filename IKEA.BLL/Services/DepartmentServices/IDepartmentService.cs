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
        IEnumerable<DepartmentDTO> GetAll();
        DepartmentDetailsDTO? GetByDepartmentId(int id);
        int CreateDepartment(CreatedDepartmentDTO createdDepartmentDTO);
        int UpdateDepartment(UpdatedDepartmentDTO updatedDepartmentDTO);
        bool DeleteDepartment(int id);


    }
}
