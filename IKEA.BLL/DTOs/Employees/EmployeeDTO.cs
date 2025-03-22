using IKEA.DAL.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.DTOs.Employees
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public decimal salary { get; set; }
        public bool IsActive { get; set; }
        public string? Email { get; set; }
        public string Gender { get; set; }
        public string EmployeeType { get; set; }
    }
}
