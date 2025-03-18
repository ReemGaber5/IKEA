using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.DTOs.Departments
{
    public class DepartmentDetailsDTO
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime Createdon { get; set; }
        public int LastModifiedBy { get; set; }
        public DateTime LastModifiedon { get; set; }
        public string Name { get; set; } = null;
        public string Code { get; set; } = null;
        public string? Description { get; set; }
        public DateOnly C0reationDate { get; set; }
    }
}
