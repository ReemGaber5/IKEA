using IKEA.BLL.DTOs.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services.EmployeeServices
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDTO>> GetAll(string search);
        Task<EmployeeDetailsDTO>? GetByEmployeeId(int id);
        Task<int> CreateEmployee(CreatedEmployeeDTO createdEmployeeDTO);
        Task<int> UpdateEmployee(UpdatedEmployeeDTO updatedEmployeeDTO);
        Task<bool> DeleteEmployee(int id);

    }
}
