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
        IEnumerable<EmployeeDTO> GetAll(string search);
        EmployeeDetailsDTO? GetByEmployeeId(int id);
        int CreateEmployee(CreatedEmployeeDTO createdEmployeeDTO);
        int UpdateEmployee(UpdatedEmployeeDTO updatedEmployeeDTO);
        bool DeleteEmployee(int id);

    }
}
