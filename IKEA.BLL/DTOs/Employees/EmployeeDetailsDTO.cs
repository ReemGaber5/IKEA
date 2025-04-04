using IKEA.DAL.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.DTOs.Employees
{
    public class EmployeeDetailsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public string? Address { get; set; }
        public decimal salary { get; set; }
        public bool IsActive { get; set; }
        public string? Email { get; set; }
        public string? PhoneNUmber { get; set; }
        public DateOnly HiringDate { get; set; }
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }

        #region Admin
        public int CreatedBy { get; set; }
        public DateTime Createdon { get; set; }
        public int LastModifiedBy { get; set; }
        public DateTime LastModifiedon { get; set; }

        public int? DeptId { get; set; }
        public string? Department { get; set; }
        #endregion
    }
}
