using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.DTOs.Departments
{
    public class CreatedDepartmentDTO
    {
        [Required(ErrorMessage = "Name Is Requried !!")]

        public string Name { get; set; } = null;
        [Required (ErrorMessage ="Code Is Requried !!")]
        public string Code { get; set; } = null;
        public string? Description { get; set; }
        [Display (Name = "Date Of Craetion")]
        public DateOnly C0reationDate { get; set; }
    }
}
